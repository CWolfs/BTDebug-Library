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

    void Start() {
      searchInput.onEndEdit.AddListener(delegate {
        List<GameObject> results = Search(searchInput.text, searchType.captionText.text);
        DisplayResults(results);
      });
    }

    private void DisplayResults(List<GameObject> results) {
      foreach (Transform child in viewList.transform) {
          GameObject.Destroy(child.gameObject);
      }

      foreach (GameObject go in results) {
        Debug.Log($"[Result] {go.GetGameObjectPath()}");
        GameObject instantiatedItem = GameObject.Instantiate(listItemPrefab, viewList.transform, false);
        instantiatedItem.GetComponentInChildren<Text>().text = go.name;
      }
    }

    public List<GameObject> Search(string searchValue, string type) {
      List<GameObject> results = new List<GameObject>();
      Debug.Log($"Search triggered with value '{searchValue}' and '{type}'");

      if (type == "Object Name") {
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in rootGameObjects) {
          if (go.name.Contains(searchValue)) results.Add(go);
          results.AddRange(go.FindAllContainsRecursive(searchValue));
        }
        Debug.Log($"[BTDebug Search] '{results.Count}' items found");
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
  } 
}