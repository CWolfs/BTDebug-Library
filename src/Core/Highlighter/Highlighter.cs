using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTDebug.Highlighter {
	public class Highlighter : MonoBehaviour {

    private Image highlightImage;
    private Color highlightColour;
    
    private float endThreshold = 20f/255f;
    private float interval = 10f/255f;

    void Start() {
      highlightImage = this.GetComponent<Image>();
      highlightColour = highlightImage.color;
      this.name = "Highlighter";
    }

    void Update() {
      Debug.Log($"[Highlighter] Updating alpha from '{highlightColour.a}' to");
      highlightColour.a -= interval;
      Debug.Log($"[Highlighter] '{highlightColour.a}");
      if (highlightColour.a <= endThreshold) {
        GameObject.Destroy(this.gameObject);
      } else {
        highlightImage.color = highlightColour;
      }
    }

  }
}