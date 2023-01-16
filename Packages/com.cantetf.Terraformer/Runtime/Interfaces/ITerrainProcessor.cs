using System.Collections.Generic;
using UnityEngine;

namespace Terraformer
{
    /// <summary>
    /// Interfaces defining prototype for processing terrain data.
    /// </summary>
    public interface ITerraformerProcessor
    {
        public Dictionary<string, RenderTexture> ProcessorResources { get; set; }
        public Material ProcessorMaterial { get; set; }
        public void SetupMaterial();
        public void ProcessStamp();
    }
}