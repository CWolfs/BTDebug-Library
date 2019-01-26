using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Reflection;

using BTDebug.Utils;

namespace BTDebug.RuntimeSearch {
	public class RuntimeSearch : MonoBehaviour {

    [SerializeField]
    private InputField searchInput;

    [SerializeField]
    private Dropdown searchType;

    [SerializeField]
    private GameObject viewList;

    [SerializeField]
    private GameObject listItemPrefab;

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
    }

    private void DisplayResults(List<GameObject> results) {
      foreach (Transform child in viewList.transform) {
          GameObject.Destroy(child.gameObject);
      }

      foreach (GameObject go in results) {
        // Debug.Log($"[Result] {go.GetGameObjectPath()}");
        GameObject instantiatedItem = GameObject.Instantiate(listItemPrefab, viewList.transform, false);
        instantiatedItem.transform.Find("Name").GetComponent<Text>().text = go.name;
        instantiatedItem.transform.Find("Path").GetComponent<Text>().text = go.GetGameObjectPath();
      }
    }

    public List<GameObject> Search(string searchValue, string type) {
      Reset();

      List<GameObject> results = new List<GameObject>();
      Debug.Log($"Search triggered with value '{searchValue}' and '{type}'");

      if (type == "Object Name") {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++) {
          Scene scene = SceneManager.GetSceneAt(i);
          SearchScene(scene, results, searchValue);
        }

        Scene dontDestroyScene = GetDontDestroyOnLoadScene();
        SearchScene(dontDestroyScene, results, searchValue);
      } else if (type == "Component") {
        Type systemType = ReflectionUtils.GetAssemblyType(searchValue);

        if (systemType != null) {
          UnityEngine.Object[] typeObjects = GameObject.FindObjectsOfType(systemType);
          foreach (var typeObject in typeObjects) {
            results.Add(((Component)typeObject).gameObject);
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
        if (go.name.Contains(searchValue)) results.Add(go);
        results.AddRange(go.FindAllContainsRecursive(searchValue));
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
    }
  } 
}