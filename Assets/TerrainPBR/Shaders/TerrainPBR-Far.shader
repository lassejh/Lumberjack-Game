Shader "Hidden/Nature/Terrain/Terrain PBR-Far"
{
	Properties 
	{
		[HideInInspector] _MainTex ("Base (RGB)", 2D) = "white" {}
		[HideInInspector] _BaseColormap ("Colormap (RGB) Metalness (A)", 2D) = "white" {}
		[HideInInspector] _BaseNormalMap ("Normalmap (RGB) Smoothness (A)", 2D) = "white" {}
	}
	
	SubShader 
	{
		Tags 
		{ 
			"RenderType"="Opaque" 
		}
		LOD 200
		
        CGINCLUDE
	    #define _GLOSSYENV 1
	    #define UNITY_SETUP_BRDF_INPUT SpecularSetup
        ENDCG
		
		CGPROGRAM
		#pragma target 3.0
		#include "UnityPBSLighting.cginc"
		#pragma surface surf Standard vertex:vert
		#pragma exclude_renderers gles

		void vert (inout appdata_full v)
		{
			v.tangent.xyz = cross(v.normal, float3(0,0,1));
			v.tangent.w = -1;
		}

		struct Input 
		{
			float2 uv_Control;
			float2 uv_Splat0;
			float2 uv_Splat1;
			float2 uv_Splat2;
			float2 uv_Splat3;
		};
		
		sampler2D _BaseColormap, _BaseNormalMap;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Base maps
			fixed4 baseColor = tex2D(_BaseColormap, IN.uv_Control);
			fixed4 baseNormal = tex2D(_BaseNormalMap, IN.uv_Control);
			
			// Specular
			fixed metalness = baseColor.a;
			fixed smoothness = baseNormal.a;
			fixed3 specular = lerp(float3(0.04,0.04,0.04), baseColor.rgb, metalness);
			
			// Lerp albedo to black for metal surfaces
			baseColor.rgb = lerp(float3(0.0, 0.0, 0.0), baseColor.rgb, 1.0-metalness);
		
			o.Albedo = baseColor;
			o.Normal = baseNormal.rgb * 2 - 1;
			o.Specular = specular;
			o.Smoothness = smoothness;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
