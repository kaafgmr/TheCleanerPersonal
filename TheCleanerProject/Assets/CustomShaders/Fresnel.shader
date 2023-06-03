Shader "TheCleaner/FresnelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _ColorFresnel("",Color) = (0,0,0,0)
		_Intensidad("",Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex VShader
            #pragma fragment FShader

            #include "UnityCG.cginc"

            struct VSInput
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct VSOutput
            {
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
				float3 normal : NORMAL;
				float4 cameraDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _Mask;
            float4 _ColorFresnel;
			float _Intensidad;
			
            VSOutput VShader(VSInput i)
            {
                VSOutput o;
                
                float4 positionO = i.position;
                
                float4 positionW = mul(UNITY_MATRIX_M, positionO);
				
                float3 normalW = mul(UNITY_MATRIX_M, i.normal);
                
                float4 positionC = mul(UNITY_MATRIX_V, positionW);
				
                float3 normalC = mul(UNITY_MATRIX_V, normalW);
				
				o.cameraDir = positionC;
				
                float4 positionS = mul(UNITY_MATRIX_P, positionC);
                
                o.position = positionS;
				
				o.normal = normalC;
                o.uv = i.uv;
                
                return o;
            }

            float4 FShader(VSOutput i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);
				
				float colorIntensity = 1- dot(i.normal, -normalize(i.cameraDir));

                return lerp(texColor,  texColor + _ColorFresnel * colorIntensity,_Intensidad);
            }
            
            ENDCG
        }
    }
}