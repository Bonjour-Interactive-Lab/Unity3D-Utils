Shader "Hidden/TimerViewer"
{
    Properties
    {
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            uniform float _NormalTime;
            uniform float _Orientation;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                float x      = step(i.uv.x, _NormalTime);
                float floory = floor((1.0 - i.uv.y) * 3);
                float y      = step(floory, _Orientation) * (1.0 - step(floory + 1, _Orientation));
                float c      = x * y;
                float4 rgba = float4(c, c, c, 1);
                return rgba;
            }
            ENDCG
        }
    }
}
