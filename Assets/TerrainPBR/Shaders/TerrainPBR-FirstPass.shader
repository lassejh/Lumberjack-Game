Shader "Nature/Terrain/Terrain PBR" 
{
	Properties 
	{
		[HideInInspector] _Control ("Control (RGBA)", 2D) = "red" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {}
		[HideInInspector] _MainTex ("BaseMap (RGB)", 2D) = "white" {}
		[HideInInspector] _Color ("Main Color", Color) = (1,1,1,1)
		_BaseColormap ("Colormap (RGB) Metalness (A)", 2D) = "white" {}
		_BaseNormalMap ("Normalmap (RGB) Smoothness (A)", 2D) = "white" {}
		_BasemapDistance ("BaseMap Distance", Float) = 1000
		_FadeLength ("Fade Lenght", Float) = 100
	}
	
	SubShader 
	{
		Tags 
		{
			"SplatCount" = "4"
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}
		
        CGINCLUDE
	    #define _GLOSSYENV 1
	    #define UNITY_SETUP_BRDF_INPUT SpecularSetup
        ENDCG
		
		CGPROGRAM
		#pragma target 3.0
		#include "UnityPBSLighting.cginc"
		#pragma surface surf Standard vertex:vert
		#pragma exclude_renderers gles

		struct Input 
		{
			float2 uv_Control;
			float2 uv_Splat0;
			float2 uv_Splat1;
			float2 uv_Splat2;
			float2 uv_Splat3;
			float3 worldPos;
		};

		void vert (inout appdata_full v)
		{
			v.tangent.xyz = cross(v.normal, float3(0,0,1));
			v.tangent.w = -1;
		}

		sampler2D _BaseColormap, _BaseNormalMap;
		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
		sampler2D _Normal0,_Normal1,_Normal2,_Normal3;
		float _BasemapDistance, _FadeLength;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float d = distance(IN.worldPos.xyz, _WorldSpaceCameraPos.xyz);
			float fadeout = saturate((_BasemapDistance - d ) / _FadeLength);
			
			// Base maps
			fixed4 splat_control = tex2D(_Control, IN.uv_Control);

			fixed4 baseColor = tex2D(_BaseColormap, IN.uv_Control);
			fixed4 baseNormal = tex2D(_BaseNormalMap, IN.uv_Control);
			fixed baseMetalness = baseColor.a;
			fixed baseSmoothness = baseNormal.a;
			fixed3 baseSpecular = lerp(float3(0.04,0.04,0.04), baseColor.rgb, baseMetalness);
						
			// Albedo
			fixed4 splatColor;
			splatColor  = splat_control.r * tex2D (_Splat0, IN.uv_Splat0);
			splatColor += splat_control.g * tex2D (_Splat1, IN.uv_Splat1);
			splatColor += splat_control.b * tex2D (_Splat2, IN.uv_Splat2);
			splatColor += splat_control.a * tex2D (_Splat3, IN.uv_Splat3);

			// Normal
			fixed4 splatNormal;
			splatNormal  = splat_control.r * tex2D(_Normal0, IN.uv_Splat0);
			splatNormal += splat_control.g * tex2D(_Normal1, IN.uv_Splat1);
			splatNormal += splat_control.b * tex2D(_Normal2, IN.uv_Splat2);
			splatNormal += splat_control.a * tex2D(_Normal3, IN.uv_Splat3);

			// Specular
			float splatMetalness = min(splatColor.a, 1.0);
			float splatSmoothness = min(splatNormal.a, 1.0);
			fixed3 splatSpecular = lerp(float3(0.04, 0.04, 0.04), splatColor.rgb, splatMetalness);
			splatColor.rgb = lerp(float3(0.0, 0.0, 0.0), splatColor.rgb, 1.0-splatMetalness);
			baseColor.rgb = lerp(float3(0.0, 0.0, 0.0), baseColor.rgb, 1.0-baseMetalness);

			// Lerp to flat normal
			fixed splatSum = dot(splat_control, fixed4(1,1,1,1));
			fixed4 flatNormal = fixed4(0.5,0.5,1,0.5);
			
			// Lerp albedo to black for metal surfaces
			splatNormal.rgb = lerp(flatNormal, splatNormal.rgb, splatSum);
			baseNormal.rgb = lerp(flatNormal, baseNormal.rgb, splatSum);

			o.Albedo = lerp(baseColor.rgb*splatSum, splatColor.rgb, fadeout);
			o.Normal = lerp(baseNormal.rgb * 2 - 1, splatNormal.rgb * 2 - 1, fadeout);
			o.Specular = lerp(baseSpecular.rgb*splatSum, splatSpecular.rgb, fadeout);
			o.Smoothness = lerp(baseSmoothness*splatSum, splatSmoothness, fadeout);
		}
		ENDCG
	} 
	
	Dependency "AddPassShader" = "Hidden/Nature/Terrain/Terrain PBR-AddPass"
	Dependency "BaseMapShader" = "Hidden/Nature/Terrain/Terrain PBR-Far"
	Dependency "Details0"      = "Hidden/TerrainEngine/Details/Vertexlit"
	Dependency "Details1"      = "Hidden/TerrainEngine/Details/WavingDoublePass"
	Dependency "Details2"      = "Hidden/TerrainEngine/Details/BillboardWavingDoublePass"
	Dependency "Tree0"         = "Hidden/TerrainEngine/BillboardTree"

	FallBack "Nature/Terrain/Diffuse"
}
