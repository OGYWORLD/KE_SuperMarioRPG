Shader "Shader Graphs/flag2" {
	Properties {
		Radial_Push ("Radial Push", Float) = 0
		Sine_Amplitude ("Sine Amplitude", Float) = 0
		Sine_frequency ("Sine frequency", Float) = 0
		Sine_speed ("Sine speed", Float) = 0
		[NoScaleOffset] main_tex ("main_tex", 2D) = "white" {}
		smoothness ("smoothness", Range(0, 1)) = 0.5
		[NoScaleOffset] [Normal] NormalMap ("NormalMap", 2D) = "bump" {}
		NormalPower ("NormalPower", Float) = 0
		[HDR] Specular_Color ("Specular Color", Vector) = (0.1981132,0.1981132,0.1981132,0)
		normal_speed ("normal_speed", Vector) = (0,0,0,0)
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}