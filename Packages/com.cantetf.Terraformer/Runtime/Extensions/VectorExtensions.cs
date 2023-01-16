using UnityEngine;

namespace Terraformer.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 XZ(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
    }
}