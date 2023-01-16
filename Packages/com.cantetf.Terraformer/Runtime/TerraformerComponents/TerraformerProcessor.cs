using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Zenject;

namespace Terraformer
{
    [ExecuteAlways][DefaultExecutionOrder(-9900)]
    public class TerraformerProcessor : MonoBehaviour
    {
        [SerializeField] private TerraformerTerrainSettings _terrainSettings;
        
        public TerraformerTerrainSettings TerrainSettings => _terrainSettings;
        
        /// <summary>
        /// Callback trigger on HeightmapResolution Changed
        /// First parameter is the previous resolution
        /// Second parameter is the new resolution
        /// </summary>
        public Action<HeightmapResolution, HeightmapResolution> OnHeightmapResolutionChanged;
        /// <summary>
        /// Callback trigger on SplatmapResolution Changed
        /// First parameter is the previous resolution
        /// Second parameter is the new resolution
        /// </summary>
        public Action<SplatmapResolution, SplatmapResolution> OnSplatmapResolutionChanged;
        
        /// <summary>
        /// Because we use the attribute [ExecuteAlways] this method is called on every reload of the scene (code compile, scene open, etc...)
        /// So we can use this method for dependency injection entry point 
        /// </summary>
        public void OnEnable()
        {
            if (_terrainSettings == null)
            {
                _terrainSettings = new TerraformerTerrainSettings(this);
            }
            StaticContext.Container.Bind<TerraformerProcessor>().FromInstance(this);
            StaticContext.Container.Bind<IRectResolver>().To<TerraformerRectResolver>().FromInstance(new TerraformerRectResolver());
            StaticContext.Container.Bind<IProcessOrder>().To<TerraformerStampProcessOrder>()
                .FromInstance(new TerraformerStampProcessOrder());
        }

        /// <summary>
        /// Update all terrains in the scene in the given update rect
        /// </summary>
        /// <param name="updateRect">World Space Update rect</param>
        public void ProcessTerrain(Rect updateRect)
        {
            
        }
    }
}