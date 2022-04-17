using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonjour
{
    public class TextureGenerator : MonoBehaviour
    {
        [Header("Output params")]
        public Vector2Int resolution;
        public float scale = 1f;
        [HideInInspector] public RenderTexture output;
        public RenderTextureFormat format;

        [Header("Shader parameters")]
        protected Material material;

        [Header("Memory Params")]
        public bool isAutoDisable = true;

        [Header("Debug")]
        public bool showOutput;
        [Range(0, 1f)] public float debugScale = 0.5f;
        [HideInInspector] public int debugWidth;
        [HideInInspector] public int debugHeight;
        public bool logBufferSize;

        public virtual void Init(){
            InitBuffer();
            InitMaterial();
            if(logBufferSize) Debug.Log($"{output.name} has been init at {output.width}×{output.height}");
        }

        protected virtual void InitBuffer(){
            output                      = new RenderTexture(Mathf.FloorToInt(resolution.x * scale), Mathf.FloorToInt(resolution.y * scale), 0, format);
            output.filterMode           = FilterMode.Trilinear;
            output.wrapMode             = TextureWrapMode.Repeat;
            output.useMipMap            = false;
            output.antiAliasing         = 1;
            output.anisoLevel           = 1;
            output.enableRandomWrite    = true;
            output.name                 = $"{this.transform.name}_{this.GetType().Name}";
            output.Create();
        }

       protected virtual void InitMaterial(){
            //here set the based params for the shader
        }

        public virtual void DrawDebugTexture(Vector2 position){
            if(output != null){
                debugWidth                  = Mathf.RoundToInt(output.width * debugScale);
                debugHeight                 = Mathf.RoundToInt(output.height * debugScale);
                GUI.DrawTexture(new Rect(position.x, position.y,
                    debugWidth, debugHeight),
                    output);
            }
        }

        protected virtual void OnDisable() {
            if(isAutoDisable){
                output?.Release();
                output = null;
            }
        }
    }   
}