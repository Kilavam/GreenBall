using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScenePathAttribute))]
public class ScenePathDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);

        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var newScene = EditorGUI.ObjectField(position, oldScene, typeof(SceneAsset), true);
        property.stringValue = AssetDatabase.GetAssetPath(newScene);
        
        EditorGUI.EndProperty();
    }
}
