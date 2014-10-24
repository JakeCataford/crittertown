Shader "Custom/SimpleOutlineShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_OutlineColor ("Outline Color", Color) = (1,1,1,0.5)
		_OutlineWidth ("Outline Width", range(1.0,2.0)) = 1.08
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			Cull front
			Fog { Mode off }
			
			CGPROGRAM  		
			#pragma vertex vert
      		#pragma fragment frag
      		#pragma target 3.0
      		
      		#include "UnityCG.cginc"
			
			half4 _OutlineColor;
			float _OutlineWidth;
			
			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata_base v) {
				v.vertex.x *= _OutlineWidth;
				v.vertex.y *= _OutlineWidth;
				v.vertex.z *= _OutlineWidth;
				
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;
				return o; 
			} 
			
			half4 frag(v2f i) : COLOR { 
				return _OutlineColor;
			}
			
			ENDCG
		}
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color;
			o.Alpha = c.a;
		}
		ENDCG	
	} 
	FallBack "Diffuse"
}
