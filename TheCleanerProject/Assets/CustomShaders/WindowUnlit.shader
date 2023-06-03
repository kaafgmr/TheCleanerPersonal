Shader "TheCleaner/WindowUnlit"
{
    Properties
    {
       [NoScaleOffset] _CleanTexture("Clean Texture", 2D) = "white" {}
       [NoScaleOffset] _DirtyTexture("Dirty Texture", 2D) = "black" {}
        _Progress("_Progress", Range(0,1)) = 0
    }
    SubShader
    {
        Name  "URPDefault"
        Tags {"RenderPipeline" = "UniversalRenderPipeline" "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
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
            };

            struct VSOutput
            {
                float4 position      : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            sampler2D _CleanTexture;
            sampler2D _DirtyTexture;
            float _Progress;

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
                float4 dirtyColor = tex2D(_DirtyTexture, i.uv);
                float4 cleanColor = tex2D(_CleanTexture, i.uv);

                float4 texColor = lerp(dirtyColor, cleanColor, _Progress);

                return texColor;
            }

            ENDHLSL
        }
    }
}