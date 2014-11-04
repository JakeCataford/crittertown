Shader "Custom/SolidColorShader" {
	Properties {
		_SolidColor ("Color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		ZTest Off
		Blend One OneMinusSrcAlpha
		Fog { Mode off }
		
		CGPROGRAM
      	#pragma surface surf Lambert
      	
      	float4 _SolidColor;
      	
      	struct Input {
          float4 color : COLOR;
      	};
      	
      	void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _SolidColor.rgb;
          o.Alpha = _SolidColor.a;
      	}
      	ENDCG
	} 
	FallBack "Unlit"
}
