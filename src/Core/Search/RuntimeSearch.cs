using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Reflection;

using BTDebug.Utils;

using RuntimeInspectorNamespace;

namespace BTDebug.RuntimeSearch {
	public class RuntimeSearch : MonoBehaviour {

    [SerializeField]
    private InputField searchInput;

    [SerializeField]
    private Dropdown searchType;
    
    [SerializeField]
    private ScrollRect scrollView;

    [SerializeField]
    private GameObject viewList;

    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
		private RuntimeHierarchy connectedHierarchy;
		public RuntimeHierarchy ConnectedHierarchy {
			get { return connectedHierarchy; }
			set { connectedHierarchy = value; }
		}

    [SerializeField]
    private List<string> ignoreObjectNames = new List<string>{
      "ColorPicker(Clone)",
      "ObjectReferencePicker(Clone)",
      "RuntimeHierarchyPool",
      "RuntimeInspectorPool",
      "BTDebugInspector"
    };

    private SearchItem selectedItem;

    void Start() {
      searchInput.onEndEdit.AddListener(delegate {
        List<GameObject> results = Search(searchInput.text, searchType.captionText.text);
        DisplayResults(results);
      });
    }

    public void SetSelectedItem(SearchItem item) {
      if (selectedItem != null) selectedItem.Deselect();
      selectedItem = item;
      selectedItem.Select();
      connectedHierarchy.OnPreSelect(selectedItem.SearchObject.transform);
      connectedHierarchy.Select(selectedItem.SearchObject.transform);
    }

    private void DisplayResults(List<GameObject> results) {
      foreach (Transform child in viewList.transform) {
          GameObject.Destroy(child.gameObject);
      }

      foreach (GameObject go in results) {
        GameObject instantiatedItem = GameObject.Instantiate(listItemPrefab, viewList.transform, false);
        SearchItem searchItem = instantiatedItem.GetComponent<SearchItem>();
        searchItem.SetSearchObject(go);
        instantiatedItem.transform.Find("Name").GetComponent<Text>().text = go.name;
        instantiatedItem.transform.Find("Path").GetComponent<Text>().text = go.GetGameObjectPath();
      }
    }

    public List<GameObject> Search(string searchValue, string type) {
      Reset();
      List<GameObject> results = new List<GameObject>();
      Debug.Log($"Search triggered with value '{searchValue}' and '{type}'");
      
      if (searchValue == "") return results;

      if (type == "Object Name") {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++) {
          Scene scene = SceneManager.GetSceneAt(i);
          SearchScene(scene, results, searchValue);
        }

        Scene dontDestroyScene = GetDontDestroyOnLoadScene();
        SearchScene(dontDestroyScene, results, searchValue);
      } else if (type == "Component") {
        Type systemType = ReflectionUtils.GetTypeByName(searchValue);

        if (systemType != null) {
          Debug.Log($"Type is {systemType}");
          GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
          for (int i = 0; i < allObjects.Length; i++) {
            GameObject go = allObjects[i];
            if (ContainsPath(ignoreObjectNames, go.GetGameObjectPath())) continue;

            if (go.GetComponent(systemType) != null) {
              results.Add(go);
            }
          }
          Debug.Log($"[BTDebug Search] '{results.Count}' items found");
        } else {
          Debug.Log($"[BTDebug Search] Unknown type of '{searchValue}'. Please use a correct component type.");
        }
      } else {
        Debug.Log("[BTDebug Search] Unknown search type of '{type}'");
      }

      return results;
    }

    private void SearchScene(Scene scene, List<GameObject> results, string searchValue) {
      GameObject[] rootGameObjects = scene.GetRootGameObjects();
      foreach (GameObject go in rootGameObjects) {
        if (ContainsPath(ignoreObjectNames, go.GetGameObjectPath())) continue;

        if (go.name.Contains(searchValue)) results.Add(go);
        results.AddRange(go.FindAllContainsRecursiveIgnoreCase(searchValue));
      }
    }

    public Scene GetDontDestroyOnLoadScene() {
      GameObject temp = null;
      try {
        temp = new GameObject();
        DontDestroyOnLoad(temp);
        Scene dontDestroyOnLoad = temp.scene;
        DestroyImmediate(temp);
        temp = null;

        return dontDestroyOnLoad;
      } finally {
        if (temp != null) DestroyImmediate(temp);
      }
    }

    public void Reset() {
      if (selectedItem != null) selectedItem.Deselect();
      selectedItem = null;
      scrollView.verticalNormalizedPosition = 1;
    }

    public bool ContainsPath(List<string> data, string path) {
      foreach (string d in data) {
        if (path.Contains(d)) return true;
      }
      return false;
    }
  } 
}