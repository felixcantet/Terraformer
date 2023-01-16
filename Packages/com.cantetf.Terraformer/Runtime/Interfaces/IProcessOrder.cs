using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Terraformer
{
    public interface IProcessOrder
    {
        /// <summary>
        /// Return an ordered list of all stamps that should be processed.
        /// The list is ordered by scene hierarchy position 
        ///  - 1
        ///      - 2
        ///        - 3
        ///  - 4
        ///      -5 
        /// </summary>
        /// <param name="stampsToProcess">The prefiltered stamp. Only stamps existing in this list are allowed to be processed</param>
        /// <returns></returns>
        public List<TerraformerStamp> ProcessOrder(List<TerraformerStamp> stampsToProcess)
        {
            // TODO : Optimize all of this !
            // First get all root objects transforms in scene
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects().Select(x => x.transform);
            // Order them by descending order 
            var orderedRoots = roots.OrderByDescending(x => x.transform.GetSiblingIndex()).ToList();

            var stack = new Stack<Transform>();
            // Add to the stack (result in ascending order)
            foreach (var item in orderedRoots)
            {
                stack.Push(item);
            }

            
            var orderedStamps = new List<TerraformerStamp>();
            // While our stack is not empty
            while (stack.Count > 0)
            {
                // get the first element of the stack and remove it
                var transform = stack.Pop();
                // Add it to the ordered list if contains a stamp that is also contains in the stampsToProcess list
                if (transform.TryGetComponent<TerraformerStamp>(out var stamp))
                {
                    if(stampsToProcess.Contains(stamp))
                        orderedStamps.Add(stamp);   
                }
                // Add all it's children to the stack (in ascending order)
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    stack.Push(transform.GetChild(i));
                }
            }
            // Return the ordered list
            return orderedStamps;
        }
    }
}