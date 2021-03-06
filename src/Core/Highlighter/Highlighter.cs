using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTDebug.Highlighter {
	public class Highlighter : MonoBehaviour {

    private Image highlightImage;
    private Color highlightColour;
    
    private float endThreshold = 1f/255f;
    private float interval = 2f;

    void Start() {
      highlightImage = this.GetComponent<Image>();
      highlightColour = highlightImage.color;
      this.name = "Highlighter";
    }

    void Update() {
      highlightColour.a -= (interval * Time.deltaTime);
      if (highlightColour.a <= endThreshold) {
        GameObject.Destroy(this.gameObject);
      } else {
        highlightImage.color = highlightColour;
      }
    }

  }
}