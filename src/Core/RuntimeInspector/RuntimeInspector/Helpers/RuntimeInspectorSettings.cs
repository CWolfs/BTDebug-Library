using UnityEngine;

namespace RuntimeInspectorNamespace
{
	[CreateAssetMenu( fileName = "Inspector Settings", menuName = "RuntimeInspector/Settings", order = 111 )]
	public class RuntimeInspectorSettings : ScriptableObject
	{
		[SerializeField]
		private InspectorField[] m_standardDrawers;
		public InspectorField[] StandardDrawers { get { return m_standardDrawers; } }

		[SerializeField]
		private InspectorField[] m_referenceDrawers;
		public InspectorField[] ReferenceDrawers { get { return m_referenceDrawers; } }

		// Hard coding these so workaround a serialisation bug with asset bundles and the type 'VariableSet'
		[SerializeField]
    private VariableSet[] m_hiddenVariables = new VariableSet[]{
			new VariableSet("Object", new string[] { "hideFlags", "useGUILayout", "runInEditMode" }),
			new VariableSet("Renderer", new string[] { "material", "materials" }),
			new VariableSet("MeshFilter", new string[] { "mesh" }),
			new VariableSet("Transform", new string[] { "right", "up", "forward", "rotationOrder", "parentInternal", "hierarchyCapacity", "drivenByObject", "drivenProperties" }),
			new VariableSet("Component", new string[] { "name", "tag" }),
			new VariableSet("Collider", new string[] { "material" }),
			new VariableSet("CanvasRenderer", new string[] { "*" })
		};
		public VariableSet[] HiddenVariables { get { return m_hiddenVariables; } }

		[SerializeField]
		private VariableSet[] m_exposedVariables = new VariableSet[0];
		public VariableSet[] ExposedVariables { get { return m_exposedVariables; } }
	}
}