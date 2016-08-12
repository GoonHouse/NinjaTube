Shader "Custom/LitWarpShader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                //o.vertex = UnityObjectToClipPos(v.vertex);
                float4 vertex = mul(UNITY_MATRIX_MV, v.vertex);
                   
                float distanceSquared = vertex.x * vertex.x + vertex.z * vertex.z;
                vertex.y += 5*sin(distanceSquared*_SinTime.x/1000);
                float y = vertex.y;
                float x = vertex.x;
                float om = sin(distanceSquared*_SinTime.x/5000) * _SinTime.x;
                vertex.y = x*sin(om)+y*cos(om);
                vertex.x = x*cos(om)-y*sin(om);
                   
                o.vertex = mul(UNITY_MATRIX_P, vertex);


                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0;

                // the only difference from previous shader:
                // in addition to the diffuse lighting from the main light,
                // add illumination from ambient or light probes
                // ShadeSH9 function from UnityCG.cginc evaluates it,
                // using world space normal
                o.diff.rgb += ShadeSH9(half4(worldNormal,1));
                return o;
            }
            
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.diff;
                return col;
            }
            ENDCG
        }
    }
}
