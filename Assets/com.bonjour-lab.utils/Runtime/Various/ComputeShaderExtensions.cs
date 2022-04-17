using UnityEngine;

namespace Bonjour.Utils{
   static class ComputeShaderExtensions
    {
        // Execute a compute shader with specifying a minimum number of thread count not by a thread GROUP count.
        public static void DispatchThreads(ref ComputeShader compute, ref int kernel, int resolution)
        {
            uint x, y, z;
            compute.GetKernelThreadGroupSizes(kernel, out x, out y, out z);
            
            // Debug.Log($"Try dispatch kernel with {x}×{y}×{z}");
            
            int threadGroupSizeX = Mathf.CeilToInt(resolution / x);
            int threadGroupSizeY = Mathf.CeilToInt(resolution / y);
            int threadGroupSizeZ = Mathf.CeilToInt(resolution / z);

            compute.Dispatch(kernel, threadGroupSizeX, threadGroupSizeY, threadGroupSizeZ);
        }
    } 
}
