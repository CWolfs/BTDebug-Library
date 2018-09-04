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

		[SerializeField]
    private VariableSet[] m_hiddenVariables = new VariableSet[0];
		public VariableSet[] HiddenVariables { get { return m_hiddenVariables; } }

		[SerializeField]
		private VariableSet[] m_exposedVariables = new VariableSet[0];
		public VariableSet[] ExposedVariables { get { return m_exposedVariables; } }
	}
}