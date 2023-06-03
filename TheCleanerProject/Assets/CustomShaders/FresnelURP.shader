Shader "TheCleaner/FresnelURP"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ColorFresnel("FresnelColor",Color) = (255,255,0,255)
        _Intensidad("Intensity",Range(0,1)) = 1
    }
    SubShader
    {
        Name  "URPDefault"
        Tags {"RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" "Queue" = "Geometry"}
        LOD 300
        //Cull[_Cull]

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
                float4 cameraDir : TEXCOORD1;
                float4 fresnelIntensity : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _ColorFresnel;
            float _Intensidad;

            VSOutput VShader(VSInput i)
            {
                VSOutput o;

                float4 positionO = i.position;

                float4 positionW = mul(UNITY_MATRIX_M, positionO);

                float4 normalW = float4(mul(UNITY_MATRIX_M, i.normal).xyz, 0);

                float4 positionC = mul(UNITY_MATRIX_V, positionW);

                float4 normalC = float4(mul(UNITY_MATRIX_V, normalW).xyz, 0);

                o.cameraDir = positionC;

                float4 positionS = mul(UNITY_MATRIX_P, positionC);

                o.position = positionS;

                o.normal = normalC;

                o.fresnelIntensity = 1 - dot(o.normal, -normalize(o.cameraDir.xy));

                o.uv = i.uv;

                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);

                return lerp(texColor,  texColor + _ColorFresnel * pow(i.fresnelIntensity, 50),_Intensidad);
            }

            ENDHLSL
        }
    }
}