using UnityEngine;

namespace Terraformer
{
    public struct TransformWrapper
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public TransformWrapper(Transform transform)
        {
            this.Position = transform.position;
            this.Rotation = transform.rotation;
            this.Scale = transform.localScale;
        }
        
        
    }
}