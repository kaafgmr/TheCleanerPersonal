Shader "TheCleaner/Base HLSL Shader"
{
    Properties
    {
       _MainTex("Texture", 2D) = "white" {}
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

            VSOutput VShader(VSInput i)
            {
                VSOutput o;

                float4 positionO = i.position;
                float4 positionW = mul(UNITY_MATRIX_M, positionO);
                float4 positionC = mul(UNITY_MATRIX_V, positionW);
                float4 positionS = mul(UNITY_MATRIX_P, positionC);

                o.position = positionS;
                o.uv = i.uv;

                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
               float4 texColor = tex2D(_MainTex, i.uv);


                return texColor;
            }

            ENDHLSL
        }
    }
}