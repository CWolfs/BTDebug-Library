using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTDebug.Highlighter {
	public class GameObjectHighlighter : MonoBehaviour {

    private Shader highlightShader;
    private Material highlightMaterial;
    
    private float endThreshold = 20f/255f;
    private float interval = 10f/255f;
    private float intervalCount = 0f;

    private Dictionary<Renderer, Material> materialCache = new Dictionary<Renderer, Material>();

    void Start() {
      highlightShader = Shader.Find("Hidden/BT-MenuUIPost");
      highlightMaterial = new Material(highlightShader);
      this.name = "Highlighter";

      Highlight();
    }
    
    private void Highlight() {
      Renderer[] renderers = this.transform.parent.gameObject.GetComponentsInChildren<Renderer>();

      foreach (Renderer r in renderers) {
        materialCache[r] = r.sharedMaterial;
        r.material = highlightMaterial;
      }
    }

    private void Reset() {
      Renderer[] renderers = this.transform.parent.gameObject.GetComponentsInChildren<Renderer>();

      foreach (Renderer r in renderers) {
        r.sharedMaterial = materialCache[r];
      }

      GameObject.Destroy(this.gameObject);
    }

    void Update() {
      intervalCount += Time.deltaTime;

      if (intervalCount > 2) Reset();
    }

  }
}