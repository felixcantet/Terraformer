using System;
using UnityEngine;

namespace Terraformer
{
    [System.Serializable]
    public class TerraformerTerrainSettings
    {
        [SerializeField] private HeightmapResolution _heightmapResolution = HeightmapResolution._513x513;
        [SerializeField] private SplatmapResolution _splatmapResolution = SplatmapResolution._512x512;
        [SerializeField] private TerraformerProcessor terraformerProcessor;
        
        public TerraformerTerrainSettings(TerraformerProcessor terraformerProcessor)
        {
            this.terraformerProcessor = terraformerProcessor;
            SetHeightmapResolution(_heightmapResolution);
            SetSplatmapResolution(_splatmapResolution);
        }
        
        public void SetHeightmapResolution(HeightmapResolution newResolution)
        {
            if (this._heightmapResolution != newResolution)
            {
                HeightmapResolution previous = _heightmapResolution;
                this._heightmapResolution = newResolution;
                terraformerProcessor.OnHeightmapResolutionChanged(_heightmapResolution, newResolution);
            }
        }

        public void SetSplatmapResolution(SplatmapResolution newResolution)
        {
            if (this._splatmapResolution != newResolution)
            {
                SplatmapResolution previous = _splatmapResolution;
                this._splatmapResolution = newResolution;
                terraformerProcessor.OnSplatmapResolutionChanged(_splatmapResolution, newResolution);
            }
        }

        public int IntHeightmapResolution => (int)_heightmapResolution;
        public int IntSplatmapResolution => (int)_heightmapResolution;
        
        public HeightmapResolution HeightmapResolution => _heightmapResolution;
        public SplatmapResolution SplatmapResolution => _splatmapResolution;
    }
}