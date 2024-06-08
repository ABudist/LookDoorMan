Shader "Unlit/CutScreenGeometry"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BorderMin ("Border min", Vector) = (0, 0, 0)
        _BorderMax ("Border max", Vector) = (0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"  "Queue" = "Transparent"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BorderMin;
            float4 _BorderMax;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 screenPosition = (i.screenPos.xy/i.screenPos.w);

                if(screenPosition.x < _BorderMin.x || screenPosition.y < _BorderMin.y || screenPosition.x > _BorderMax.x || screenPosition.y > _BorderMax.y)
                {
                    col.a = 0;
                }
                
                return col;
            }
            ENDCG
        }
    }
}
