Shader "Custom/Pixelation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // This must exist!
        _PixelResolution ("Pixel Resolution", Vector) = (320, 180, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        Pass
        {
            Name "PixelationPass"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _PixelResolution;

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
                float2 pixelSize = 1.0 / _PixelResolution.xy;
                float2 uv = floor(i.uv / pixelSize) * pixelSize;
                return tex2D(_MainTex, uv);
            }
            ENDHLSL
        }
    }
}