Shader "Custom/ClippingMask" {
 
 
    Properties {
      _MainTex ("Base (RGB)", 3D) = "white" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float3 worldPos;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      void surf (Input IN, inout SurfaceOutput o) {
          clip (IN.worldPos.y + 2.5);
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb*5;
      }
      ENDCG
    }
    Fallback "Diffuse"
}