Shader "Custom/VertexLayered" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LayerOne ("Layer One (Vertex R)", 2D) = "white" {}
		_LOneIntensity ("Intensity", range(0.0,1.0)) = 1.0
		_LayerTwo ("Layer Two (Vertex G)", 2D) = "white" {}
		_LTwoIntensity("Intensity", range(0.0,1.0)) = 1.0
		_LayerThree ("Layer Three (Vertex B)", 2D) = "white" {}
		_LThreeIntensity ("Intensity", range(0.0,1.0)) = 1.0
		_VertexColorOverlay ("Vertex Color Overlay", range(0.0,1.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		sampler2D _LayerOne;
		sampler2D _LayerTwo;
		sampler2D _LayerThree;
		float _VertexColorOverlay;
		float _LOneIntensity;
		float _LTwoIntensity;
		float _LThreeIntensity;

		struct Input {
			float2 uv_MainTex;
			float2 uv_LayerOne;
			float2 uv_LayerTwo;
			float2 uv_LayerThree;
			float4 vertexColor;
		};
		
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
	        o.vertexColor = v.color;
	    }

		void surf (Input IN, inout SurfaceOutput o) {
			half4 base = tex2D (_MainTex, IN.uv_MainTex);
			half4 oone = tex2D (_LayerOne, IN.uv_LayerOne);
			half4 two = tex2D (_LayerTwo, IN.uv_LayerTwo);
			half4 three = tex2D (_LayerThree, IN.uv_LayerThree);
			
			o.Albedo = base.rgb;
			o.Albedo = lerp(o.Albedo, oone, IN.vertexColor.r * _LOneIntensity * oone.a);
			o.Albedo = lerp(o.Albedo, two, IN.vertexColor.g * _LTwoIntensity * two.a);
			o.Albedo = lerp(o.Albedo, three, IN.vertexColor.b * _LThreeIntensity * three.a);
			
			o.Albedo += IN.vertexColor * _VertexColorOverlay;
			
			o.Alpha = base.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
