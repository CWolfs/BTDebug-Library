using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using RuntimeInspectorNamespace;

namespace BTDebug.RuntimeSearch {
	public class SearchItem : MonoBehaviour {
    [SerializeField]
    private RuntimeSearch runtimeSearch;

    [SerializeField]
    private GameObject searchObject;
    public GameObject SearchObject {
      get { return searchObject; }
      set { searchObject = value; }
    }

    private PointerEventListener clickListener;
    private Image background;

    void Start() {
      Initialise();
    }

    private void Initialise() {
      runtimeSearch = this.GetComponentInParent<RuntimeSearch>();
      clickListener = GetComponent<PointerEventListener>();
      background = clickListener.GetComponent<Image>();
			clickListener.PointerClick += ( eventData ) => OnClicked();
    }

    public void SetSearchObject(GameObject searchObject) {
      this.SearchObject = searchObject;
    }

    public void OnClicked() {
      runtimeSearch.SetSelectedItem(this);
		}

    public void Select() {
      Color newColour = background.color;
      newColour.a = 1f;
      background.color = newColour;
    }

    public void Deselect() {
      Color newColour = background.color;
      newColour.a = 0f;
      background.color = newColour;
    }
	}
}