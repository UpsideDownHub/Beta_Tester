﻿// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/LightShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		[PerRendererData] _MainTex("Albedo (RGB)", 2D) = "white" {}
	_MyNormalMap("My Normal map", 2D) = "white" {}
	_EmissiveMap("Emissive map", 2D) = "white" {}
	[Toggle(EMISSIVE_TEXTURE)] _EnabledEmissive("Use Emissive?", Float) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200
		Cull Off

		CGPROGRAM
#pragma shader_feature EMISSIVE_TEXTURE
#pragma surface surf Standard fullforwardshadows alpha:fade
#pragma target 3.0

		sampler2D _MainTex;
	sampler2D _MyNormalMap;
	sampler2D _EmissiveMap;

	struct Input {
		float2 uv_MainTex;
	};

	fixed4 _Color;

	UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_MyNormalMap, IN.uv_MainTex));
#if EMISSIVE_TEXTURE
		o.Emission = tex2D(_EmissiveMap, IN.uv_MainTex);
#endif

		// Hack for outline: Make black pixels always black
		if (length(c.rgb)<0.001)
		{
			o.Normal = fixed3(0,0,-1);
			o.Albedo = fixed3(0,0,0);
		}
	}
	ENDCG
	}
		FallBack "Diffuse"
}
