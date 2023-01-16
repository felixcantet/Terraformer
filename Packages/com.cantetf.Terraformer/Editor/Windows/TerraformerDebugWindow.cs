using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using Zenject;

namespace Terraformer
{
    public class TerraformerDebugWindow : EditorWindow 
    {
        [Inject] private IProcessOrder _processOrder;
        
        [MenuItem("Tools/Terraformer/Debug Window")]
        public static void Init()
        {
            EditorWindow.GetWindow<TerraformerDebugWindow>().Show();
        }

        private void OnEnable()
        {
            StaticContext.Container.Inject(this);
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
            root.Add(new Button(() =>
            {
                var processOrder = this._processOrder.ProcessOrder(FindObjectsOfType<TerraformerStamp>().ToList());
                foreach (var item in processOrder)
                {
                    item.processOrder = processOrder.IndexOf(item);
                }
            })
            {
                text = "Debug Process Order"
            });
        }
    }
}