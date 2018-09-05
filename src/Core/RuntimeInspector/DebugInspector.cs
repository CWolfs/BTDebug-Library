using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace {
	public class BTDebugInspector : MonoBehaviour {
      public ColorPicker colourPickerPrefab;
      public DraggedReferenceItem draggedReferenceItemPrefab;
      public ObjectReferencePicker objectReferencePickerPrefab;

      void Awake() {
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        MonoBehaviour.DontDestroyOnLoad(this.gameObject);
      }
  }
}