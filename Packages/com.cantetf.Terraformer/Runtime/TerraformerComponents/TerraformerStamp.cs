using System;
using UnityEngine;
using Zenject;

namespace Terraformer
{
    [ExecuteAlways]
    public class TerraformerStamp : MonoBehaviour
    {
        [Inject] private TerraformerProcessor processor;
        [Inject] private IRectResolver _rectResolver;
        [SerializeField] public int processOrder;
        private TransformWrapper transformWrapper;

        public TransformWrapper TransformWrapper => transformWrapper;

        [SerializeField] private Texture heightMap;

        public Texture HeightMap => heightMap;

        #region MonoBehaviour

        private void OnEnable()
        {
            StaticContext.Container.Inject(this);
        }

        #endregion
        
        #region public methods

        public void SetPosition(Vector3 newPosition, bool updateTerrain = true)
        {
            Rect previousRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            transformWrapper.Position = newPosition;
            transform.position = newPosition;
            Rect newRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            if (updateTerrain)
            {
                var combinedRect = _rectResolver.CombineRects(previousRect, newRect);
                processor.ProcessTerrain(combinedRect);
            }
        }

        public void SetRotation(Quaternion newRotation, bool updateTerrain = true)
        {
            Rect previousRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            transformWrapper.Rotation = newRotation;
            transform.rotation = newRotation;
            Rect newRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            if (updateTerrain)
            {
                var combinedRect = _rectResolver.CombineRects(previousRect, newRect);
                processor.ProcessTerrain(combinedRect);
            }
        }

        public void SetScale(Vector3 newScale, bool updateTerrain = true)
        {
            Rect previousRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            transformWrapper.Scale = newScale;
            
            Rect newRect = _rectResolver.GetStampWorldSpaceRectIncludingChilds(this);
            if (updateTerrain)
            {
                var combinedRect = _rectResolver.CombineRects(previousRect, newRect);
                processor.ProcessTerrain(combinedRect);
            }
        }
        
        #endregion
    }
}