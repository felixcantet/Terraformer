using System;
using System.Collections.Generic;
using Terraformer.Extensions;
using UnityEngine;

namespace Terraformer
{
    public interface IRectResolver
    {
        /// <summary>
        /// Combine two rect by using the minimum and maximum of the two rects
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Rect CombineRects(Rect a, Rect b)
        {
            var min = Vector2.Min(a.min, b.min);
            var max = Vector2.Max(a.max, b.max);
            
            return new Rect(min, max - min);
        }
        
        /// <summary>
        /// Combine all rects in the list
        /// </summary>
        /// <param name="rects">List a rects to combine</param>
        /// <returns>A combination of all rects in the list</returns>
        /// <exception cref="Exception">Throw if Rect List is empty</exception>
        public Rect CombineRects(List<Rect> rects)
        {
            if(rects.Count == 0)
                throw new Exception("Cannot combine an empty list of rects");

            var rect = rects[0];
            for(int i = 1; i < rects.Count; i++)
            {
                rect = CombineRects(rect, rects[i]);
            }

            return rect;
        }

        public Vector3[] GetStampVertex(TerraformerStamp stamp)
        {
            var res = new Vector3[4];
            var transform = stamp.TransformWrapper;
            float xOffset = stamp.TransformWrapper.Scale.x * 0.5f;
            float zOffset = stamp.TransformWrapper.Scale.z * 0.5f;
            
            Quaternion rotation = transform.Rotation;
            
            // Apply rotation to the vertices and offset them by stamp position to get world space vertices
            res[0] = rotation * new Vector3(-xOffset, 0, -zOffset) + transform.Position;
            res[1] = rotation * new Vector3(-xOffset, 0, zOffset) + transform.Position;
            res[2] = rotation * new Vector3(xOffset, 0, zOffset) + transform.Position;
            res[3] = rotation * new Vector3(xOffset, 0, -zOffset) + transform.Position;

            return res;
        }

        public Rect GetStampWorldSpaceRect(TerraformerStamp stamp)
        {
            var vertices = GetStampVertex(stamp);

            Vector2 min = Vector3.positiveInfinity;
            Vector2 max = Vector3.negativeInfinity;
            foreach (var vertex in vertices)
            {
                min = Vector2.Min(min, vertex.XZ());
                max = Vector2.Max(max, vertex.XZ());
            }

            return new Rect(min, max - min);
        }

        /// <summary>
        /// Compute Rect of stamps including children
        /// Usefull for computing update rect when stamps are parented
        /// </summary>
        /// <param name="stamp">The stamp to compute Rect for</param>
        /// <returns>A combination of all rects</returns>
        public Rect GetStampWorldSpaceRectIncludingChilds(TerraformerStamp stamp)
        {
            var rects = new List<Rect>();
            
            foreach (var item in stamp.GetComponentsInChildren<TerraformerStamp>())
            {
                rects.Add(GetStampWorldSpaceRect(item));
            }

            return CombineRects(rects);
        }
        
    }
}