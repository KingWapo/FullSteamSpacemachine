Shader "Example/Rim" {
    Properties {
      _MainCol ("Color", Color) = (1,1,1,1)
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _BaseGlow("Glow Multiplier", Range(0.0,3.0)) = 2.0
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
      CGPROGRAM
      #pragma surface surf Lambert alpha
      struct Input {
          float3 viewDir;
      };
      float3 _MainCol;
      float4 _RimColor;
      float _RimPower;
      float _BaseGlow;
      
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _MainCol.rgb;
          o.Alpha = 0f;
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission =  _RimColor.rgb * _BaseGlow *(pow (rim, _RimPower));
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }