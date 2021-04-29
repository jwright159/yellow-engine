Shader "Portal"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = ComputeScreenPos(o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv.xy / i.uv.w);
            }
            ENDHLSL
        }
    }
}
