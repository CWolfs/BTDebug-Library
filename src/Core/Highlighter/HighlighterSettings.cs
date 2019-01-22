using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTDebug.Highlighter {
	public class HighlighterSettings : MonoBehaviour {

    [SerializeField]
    private GameObject highlighterPrefab;

    public GameObject HighlighterPrefab {
      get { return highlighterPrefab; }
    }

    [SerializeField]
    private GameObject gameObjectHighlighterPrefab;

    public GameObject GameObjectHighlighterPrefab {
      get { return gameObjectHighlighterPrefab; }
    }

  }
}