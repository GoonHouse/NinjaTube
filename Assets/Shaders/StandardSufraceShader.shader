Shader "Custom/StandardSufraceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("Extrusion Amount", Range(-100,100)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};
		float _Amount;
		void vert (inout appdata_full v, out Input IN) {
			UNITY_INITIALIZE_OUTPUT(Input, IN);
			//float distanceSquared = v.vertex.x * v.vertex.x + v.vertex.z * v.vertex.z;
			float distanceSquared = IN.worldPos.y*IN.worldPos.y;
			v.vertex.z += 5*_Amount*sin(distanceSquared*_SinTime.x/10);
			float z = v.vertex.z;
			float x = v.vertex.x;
			float om = sin(distanceSquared*_SinTime.x/50) * _SinTime.x;
			v.vertex.z = x*sin(om)+z*cos(om);
			v.vertex.x = x*cos(om)-z*sin(om);
			}

		sampler2D _MainTex;

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}