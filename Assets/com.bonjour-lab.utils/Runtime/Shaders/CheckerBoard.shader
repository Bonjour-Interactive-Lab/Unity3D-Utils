Shader "Custom/CheckerBoard"
{
    //Simple checkerboard shader
    //This checker implements its ownb utils function to work outside the ./Utils/ folder
    Properties
    {
        [Header(Mat properties)]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        [Header(checker board)]
        _ColsRows("Column Rows", Vector) = (0, 0, 0, 1)
        _ColorOdd("Odd color", Color) = (0, 0, 0, 1)
        _ColorEven("Even color", Color) = (1, 1, 1, 1)
        _CrossLength("Cross length", Range(0, 1)) = 0.1
        _CrossThickness("Cross thickness", Range(0, 1)) = 0.05
        [Toggle]
        _UseGradient("Use gradient for index", Float) = 1
    }
    SubShader
    {

        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float2 _ColsRows;
        fixed4 _ColorOdd;
        fixed4 _ColorEven;
        float _CrossLength;
        float _CrossThickness;
        float _UseGradient;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        //Rect sdf for cross shape
        float rectangleSDF(float2 st, float2 thickness){
            //remap st coordinate from 0.0 to 1.0 to -1.0, 1.0
            st = st * 2.0 - 1.0;
            float edgeX = abs(st.x / thickness.x);
            float edgeY = abs(st.y / thickness.y);
            return max(edgeX, edgeY);
        }

        //Union operator for cross shape (made with two rectangle)
        float opUnite(float d1, float d2){
            return (d1 < d2) ? d1 : d2;
        }

        //fill operator for cross color
        float fill(float x, float size){
            return 1.0 - step(size, x);
        }

        //HSV to RGB convert fct to use hue as index mapper
        float3 hsv2rgb(in float3 hsb) {
            float3 rgb = clamp(abs(fmod(hsb.x * 6. + float3(0., 4., 2.), 
                                    6.) - 3.) - 1.,
                            0.,
                            1.);
            #ifdef HSV2RGB_SMOOTH
            rgb = rgb*rgb*(3. - 2. * rgb);
            #endif
            return hsb.z * lerp(float3(1., 1., 1.), rgb, hsb.y);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //create the checker board
            float2 nuv  = IN.uv_MainTex * _ColsRows;
            float2 fuv  = frac(nuv);
            float2 iuv  = floor(nuv);
            float index = iuv.x + iuv.y * _ColsRows.x;

            //draw cross
            float rectUP    = rectangleSDF(fuv, float2(_CrossThickness, _CrossLength));
            float rectLeft  = rectangleSDF(fuv, float2(_CrossLength, _CrossThickness));
            float crossSDF  = opUnite(rectUP, rectLeft);
            float fillCross = fill(crossSDF, 1.0);

            float3 crossColor   = (_UseGradient == 1) ? hsv2rgb(float3(index / (_ColsRows.x * _ColsRows.y), 1.0, 0.5)) * fillCross : float3(fillCross, fillCross, fillCross);
            fixed4 checker      = ((iuv.x + iuv.y) % 2 == 0) ? _ColorOdd: _ColorEven;

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = (c.rgb * checker) * (1.0 - fillCross) + crossColor;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
