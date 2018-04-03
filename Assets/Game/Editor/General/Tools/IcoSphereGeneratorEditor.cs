using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(IcoSphereGenerator))]
public class IcoSphereGeneratorEditor : Editor
{   
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IcoSphereGenerator generator = (IcoSphereGenerator)target;
        if (GUILayout.Button("Build Object"))
        {
            generator.GenerateMesh();
        }
    }
}
