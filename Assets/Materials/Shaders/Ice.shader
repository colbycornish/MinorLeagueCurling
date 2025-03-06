Shader "Custom/Ice"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RampTex("Ramp", 2D) = "white" {}
		_DistortMap("Distort Map", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_EdgeThickness("Silouette Dropoff Rate", float) = 1.0
        _DistortStrength("Distort Strength", Range(-1,1)) = 1.0
        _MaxEdgeAlpha("Max Edge Fresnel Alpha",float) = 0.5
    }
    SubShader
    {
        // Grab the screen behind the object into _BackgroundTexture
        GrabPass
        {
            "_BackgroundTexture"
        }

        // Background distortion
        Pass
        {
            Tags
            {
                "Queue" = "Transparent"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _BackgroundTexture;
            sampler2D _DistortMap;
            float4 _DistortMap_ST;
            float     _DistortStrength;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float3 texCoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float3 texCoord : TEXCOORD0;
                float4 grabPos : TEXCOORD1;
            };

            vertexOutput vert(vertexInput input)
            {
                 vertexOutput output;

                // convert input to world space
                output.pos = UnityObjectToClipPos(input.vertex);
                // use ComputeGrabScreenPos function from UnityCG.cginc
                // to get the correct texture coordinate
                output.grabPos = ComputeGrabScreenPos(output.pos);
                output.texCoord=input.texCoord;
                // distort based on bump map
                return output;
            }

            float4 frag(vertexOutput input) : SV_Target
            {
                float bump=tex2D(_DistortMap,TRANSFORM_TEX(input.texCoord,_DistortMap)).r-0.5;
                input.grabPos.x+=bump*_DistortStrength;
                input.grabPos.y+=bump*_DistortStrength;
                return tex2Dproj(_BackgroundTexture, input.grabPos);
            }
            ENDCG
        }

        // Shadow pass
		Pass
    	{
            Tags 
			{
				"LightMode" = "ShadowCaster"
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
    	}
	}

}