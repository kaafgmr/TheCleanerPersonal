Shader "TheCleaner/Base HLSL Shader Lit"
{
    Properties
    {
       [Header(UniversalRP Default Shader code)]
       [Space(20)]
       _TintColor("TintColor", color) = (1,1,1,1)
       _MainTex("Texture", 2D) = "white" {}

       [Toggle]_AlphaTest("Alpha Test", float) = 0
       _Alpha("AlphaClip", Range(0,1)) = 0.5
       [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull Mode", Float) = 1
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
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex VShader
            #pragma fragment FShader

            //include fog
            #pragma multi_compile_fog           

            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma shader_feature _ALPHATEST_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _TintColor;
                sampler2D _MainTex;
                float4 _MainTex_ST;
                float   _Alpha;
            CBUFFER_END

            struct VSInput
            {
                float4 position : POSITION;
                float2 uv     : TEXCOORD0;
                float3 normal : NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VSOutput
            {
                float4 position      : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float fogCoord : TEXCOORD1;
                float3 normal      : NORMAL;

                float4 shadowCoord : TEXCOORD2;

                //if shader need a view direction, use below code in shader 
                //float3 WorldSpaceViewDirection : TEXCOORD3;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            VSOutput VShader(VSInput i)
            {
                VSOutput o;
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_TRANSFER_INSTANCE_ID(i, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.position = TransformObjectToHClip(i.position.xyz);

                o.uv = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw; ;
                o.normal = normalize(mul(i.normal, (float3x3)UNITY_MATRIX_I_M));

                o.fogCoord = ComputeFogFactor(o.position.z);

                VertexPositionInputs positionInput = GetVertexPositionInputs(i.position.xyz);
                o.shadowCoord = GetShadowCoord(positionInput);

                //view direction
                //o.WorldSpaceViewDirection = _WorldSpaceCameraPos.xyz - mul(GetObjectToWorldMatrix(), float4(v.vertex.xyz, 1.0)).xyz;

                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                float4 col = tex2D(_MainTex, i.uv) * _TintColor;


                //below texture sampling code does not use in material inspector             
                // float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);


                Light mainLight = GetMainLight(i.shadowCoord);


                //Lighting Calculate(Lambert)              
                float NdotL = saturate(dot(normalize(_MainLightPosition.xyz), i.normal));
                float3 ambient = SampleSH(i.normal);

                // half receiveshadow = MainLightRealtimeShadow(i.shadowCoord);
                // col.rgb *= NdotL * _MainLightColor.rgb * receiveshadow + ambient;

                col.rgb *= NdotL * _MainLightColor.rgb * mainLight.shadowAttenuation + ambient;

                #if _ALPHATEST_ON
                clip(col.a - _Alpha);
                #endif

                //apply fog
                col.rgb = MixFog(col.rgb, i.fogCoord);


                return col;
            }

            ENDHLSL
        }
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}