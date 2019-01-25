using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
      Debug.Log($"Search triggered with value {searchValue} and {type}");
    }
  } 
}