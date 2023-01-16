using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;

namespace Terraformer.Editor
{
    [CustomEditor(typeof(TerraformerProcessor))]
    public class TerraformerTerrainCustomInspector : UnityEditor.Editor
    {
        public bool expandedState = true;
        private VisualElement foldoutContainer;
        private Box box;
        public override VisualElement CreateInspectorGUI()
        {
            
            var container = new VisualElement();
            foldoutContainer = new VisualElement()
            {
                style = { marginLeft = 20}
            };
            box = new Box()
            {
                style =
                {
                    backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.0f),
                    borderBottomLeftRadius = 5.0f,
                    borderBottomRightRadius = 5.0f,
                    borderTopLeftRadius = 5.0f,
                    borderTopRightRadius = 5.0f,
                    borderBottomColor = Color.black,
                    borderLeftColor = Color.black,
                    borderRightColor = Color.black,
                    borderTopColor = Color.black,
                    marginTop = 5,
                    paddingBottom = 5,
                    paddingTop = 5,
                    paddingRight = 5,
                    paddingLeft = 5
                }
            };
            container.Add(box);

            StyleCursor GetCursor()
            {
                var cursor = new Cursor();
                var field = typeof(Cursor).GetProperty("defaultCursorId",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                field.SetValue(cursor, MouseCursor.Link);
                return new StyleCursor((Cursor)cursor);
            }
            
            var label = new Label("Terraformer Terrain Settings")
            {
                style = { fontSize = 20, unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold), cursor =  GetCursor(), paddingBottom = 15, unityTextAlign = TextAnchor.MiddleCenter}
            };
            label.RegisterCallback((ClickEvent e) =>
            {
                this.expandedState = !this.expandedState;
                OnFoldoutToggle(expandedState);
            });
            
            
            var heightmapRes = new EnumField("Heightmap Resolution", (target as TerraformerProcessor).TerrainSettings.HeightmapResolution);
            heightmapRes.RegisterValueChangedCallback(x =>
            {
                var newVal = (HeightmapResolution)x.newValue ;
                (target as TerraformerProcessor).TerrainSettings.SetHeightmapResolution(newVal);
            });
            
            var splatmapRes = new EnumField("Splatmap Resolution", (target as TerraformerProcessor).TerrainSettings.SplatmapResolution);
            splatmapRes.RegisterValueChangedCallback(x =>
            {
                var newVal = (SplatmapResolution)x.newValue ;
                (target as TerraformerProcessor).TerrainSettings.SetSplatmapResolution(newVal);
            });
            
            foldoutContainer.Add(heightmapRes);
            foldoutContainer.Add(splatmapRes);
            
            box.Add(label);
            OnFoldoutToggle(expandedState);
            //
            // var foldout = new Foldout()
            // {
            //     text = "Terraformer Terrain Settings",
            //     style = { fontSize = 25, unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold) }
            // };
            // foldout.viewDataKey = "foldout_state";
            // foldout.Add(new EnumField("Heightmap Resolution", (target as TerraformerTerrain).TerrainSettings.HeightmapResolution)
            // {
            //     style = { fontSize = 12, unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Normal) }
            // });
            // box.Add(foldout);
            // var label = new Label("Terraformer Terrain Settings");

            return container;
        }

        private void OnFoldoutToggle(bool expandState)
        {
            if (expandState)
            {
                box.Add(foldoutContainer);   
                
            }
            else
            {
                if(box.Contains(foldoutContainer))
                    box.Remove(foldoutContainer);   
            }
        }
    }
}