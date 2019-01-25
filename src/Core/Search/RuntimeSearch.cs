using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using System.Reflection;

namespace BTDebug.RuntimeSearch {
	public class RuntimeSearch : MonoBehaviour {

    [SerializeField]
    private InputField searchInput;

    [SerializeField]
    private Dropdown searchType;

    void Start() {
      searchInput.onEndEdit.AddListener(delegate {
        Search(searchInput.text, searchType.captionText.text);
      });
    }

    public static void Search(string searchValue, string type) {
      Debug.Log($"Search triggered with value '{searchValue}' and '{type}'");
      if (type == "Object Name") {

      } else if (type == "Component") {
        Type systemType = Type.GetType(searchValue);
        if (systemType != null) {
          UnityEngine.Object[] types = GameObject.FindObjectsOfType(systemType);
          Debug.Log($"[BTDebug Search] '{types.Length}' items found");
        } else {
          Debug.Log($"[BTDebug Search] Unknown type of '{searchValue}'. Please use a correct component type.");
        }
      } else {
        Debug.Log("[BTDebug Search] Unknown search type of '{type}'");
      }
    }
  } 
}