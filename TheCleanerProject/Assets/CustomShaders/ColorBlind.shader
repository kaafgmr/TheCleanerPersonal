Shader "TheCleaner/ColorBlind"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _R ("_R", Color) = (1, 0, 0, 1)
        _G ("_G", Color) = (0, 1, 0, 1)
        _B ("_B", Color) = (0, 0, 1, 1)
    }
    SubShader
    {
        Name  "URPDefault"
        Tags {"RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" "Queue" = "Geometry"}
        LOD 300
        Cull[_Cull]

        Pass
        {
            HLSLPROGRAM
            
            #pragma vertex VShader
            #pragma fragment FShader

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct VSInput
            {
                float4 position : POSITION;
                float2 uv     : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct VSOutput
            {
                float4 position      : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float3 normal      : NORMAL;
            };

            sampler2D _MainTex;
            float4 _R;
            float4 _G;
            float4 _B;

            VSOutput VShader(VSInput i)
            {
                VSOutput o;

                float4 positionO = i.position;
                float4 positionW = mul(UNITY_MATRIX_M, positionO);
                float4 positionC = mul(UNITY_MATRIX_V, positionW);
                float4 positionS = mul(UNITY_MATRIX_P, positionC);

                o.position = positionS;
                o.uv = i.uv;
                o.normal = i.normal;

                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
                float4 screenColor = tex2D(_MainTex, i.uv);

                return float4(
                    screenColor.r * _R.r + screenColor.g * _R.g + screenColor.b * _R.b,
                    screenColor.r * _G.r + screenColor.g * _G.g + screenColor.b * _G.b,
                    screenColor.r * _B.r + screenColor.g * _B.g + screenColor.b * _B.b,
                    1
                );
            }

            ENDHLSL
        }
    }
}