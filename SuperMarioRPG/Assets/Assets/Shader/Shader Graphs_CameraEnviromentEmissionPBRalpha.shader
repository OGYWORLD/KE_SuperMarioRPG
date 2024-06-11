Shader "Shader Graphs/CameraEnviromentEmissionPBRalpha" {
	Properties {
		[NoScaleOffset] Texture2D_72456657 ("Albed", 2D) = "white" {}
		Color_12D45797 ("AlbedColor", Vector) = (1,1,1,1)
		[NoScaleOffset] [Normal] Texture2D_5B0DF029 ("Normal", 2D) = "bump" {}
		Vector1_7B79C9F4 ("NormalStrength", Float) = 0
		[NoScaleOffset] Texture2D_149BFCDF ("CameraEnviroment", 2D) = "black" {}
		Vector1_3A62148A ("CameraEnviromentSize", Float) = 1
		[NoScaleOffset] Texture2D_E6F92EDB ("CameraEnvMask", 2D) = "white" {}
		Vector1_83FAEC45 ("Metalic", Range(0, 1)) = 0
		Vector1_6C6DE689 ("Smoothness", Range(0, 1)) = 0.5
		[NoScaleOffset] Texture2D_8D977005 ("Occurusion", 2D) = "white" {}
		[ToggleUI] Boolean_CD575E20 ("AlphaClipping", Float) = 0
		Vector1_D7FBD576 ("AlphaClipThreshold", Range(0, 1)) = 0.5
		Vector1_1C2E824E ("emissionThreshold", Range(0, 1)) = 0.5
		[NoScaleOffset] Texture2D_EF17A62A ("Emission", 2D) = "black" {}
		[HDR] Color_A9739CCC ("EmissionColor", Vector) = (0,0,0,0)
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