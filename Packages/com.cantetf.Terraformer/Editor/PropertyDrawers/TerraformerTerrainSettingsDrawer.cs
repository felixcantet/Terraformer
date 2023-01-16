using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Terraformer.Editor
{
    [CustomPropertyDrawer(typeof(TerraformerTerrainSettings))]
    public class TerraformerTerrainSettingsDrawer : PropertyDrawer
    {
        private bool foldoutOpen = false;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var foldout = new Foldout();
            foldout.text = "Terraformer Terrain Settings";
            foldout.Add(new PropertyField(property.FindPropertyRelative("_heightmapResolution")));
            foldout.Add(new PropertyField(property.FindPropertyRelative("_splatmapResolution")));
            container.Add(foldout);
            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            foldoutOpen = EditorGUI.Foldout(position, foldoutOpen, "Terraformer Terrain Settings");
            if(foldoutOpen)
            {
                EditorGUI.indentLevel++;
                var heightMapRes = property.FindPropertyRelative("_heightmapResolution");
                var splatMapRes = property.FindPropertyRelative("_splatmapResolution");
                var prevHeightmapRes = heightMapRes.enumValueIndex;
                var prevSplatmapRes = splatMapRes.enumValueIndex;
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(heightMapRes);
                EditorGUILayout.PropertyField(splatMapRes);
                if (EditorGUI.EndChangeCheck())
                {
                    Debug.Log($"Value Changed from {prevHeightmapRes} to {heightMapRes.enumValueIndex}");
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}