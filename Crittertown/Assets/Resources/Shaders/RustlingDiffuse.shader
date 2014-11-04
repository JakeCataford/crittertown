Shader "Custom/Rustling Diffuse" {
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_Amount ("Amount", Range(0,.2)) = 0.1
		_Scale ("Scale", Range(0.1,10)) = 1.0
		_TimeScale ("Time Scale", Range(0,10)) = 1.0
		_NormalInfluence ("Normal Influence", Range(0.0,1)) = 0.5
	}
	SubShader
	{
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		struct Input
		{
			float2 uv_MainTex;
		};

		half4 _Direction;
		float _Amount;
		float _TimeScale;
		float _Scale;
		sampler2D _MainTex;
		half4 _Color;
		float _NormalInfluence;

		void vert(inout appdata_full v)
		{
			float4 offs = ((float4 (normalize(v.normal), 0) + v.color) * sin (_Time.y * _TimeScale + (v.vertex.y * _Scale) + (v.vertex.x * _Scale) - (v.vertex.z * _Scale)) * _Amount) * v.color.a;
			v.vertex += lerp(offs, float4(v.normal, 0) * length(offs),  _NormalInfluence);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;
			o.Alpha = c.a * _Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
