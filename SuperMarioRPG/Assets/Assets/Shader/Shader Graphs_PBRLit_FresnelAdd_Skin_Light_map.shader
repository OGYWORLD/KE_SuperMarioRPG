Shader "Shader Graphs/PBRLit_FresnelAdd_Skin_Light_map" {
	Properties {
		[NoScaleOffset] _MainTex ("AlbedMap", 2D) = "white" {}
		[NoScaleOffset] [Normal] Texture2D_2A45DA31 ("NormalMap", 2D) = "bump" {}
		Vector1_7388E1D8 ("NormalMapStrength", Range(0, 4)) = 1
		[NoScaleOffset] Texture2D_7923EF37 ("MultiMap(Smoothness,AO,Brank,Metalic)", 2D) = "white" {}
		_Color ("Albed", Vector) = (1,1,1,0)
		Vector1_B40A056E ("Metalic", Range(0, 1)) = 0
		Vector1_2F04771C ("Smoothness", Range(0, 1)) = 0.5
		Vector1_BA802B87 ("AmbientOcculusion", Range(0, 1)) = 1
		[NoScaleOffset] FresnelMask ("FresnelMask", 2D) = "white" {}
		Vector1_E173C7DF ("FrenelColorAngle", Range(0, 360)) = 0
		Color_7A81B365 ("FresnelColorA", Vector) = (1,0,0,1)
		Vector1_FE42F863 ("ThreshouldA", Range(0, 1)) = 0
		Color_F87EAD4 ("FresnelColorB", Vector) = (0.1881378,1,0,1)
		Vector1_4AAD1857 ("ThreshouldB", Range(0, 1)) = 1
		Vector1_7D6E59DF ("FresnelPower", Float) = 1
		Vector1_EF7CA402 ("FresnelOpacity", Range(0, 1)) = 1
		Vector1_8624AC6D ("ThreshouldLow", Range(0, 1)) = 0
		Vector1_B2A63013 ("ThreshouldHigh", Range(0, 1)) = 1
		Vector1_A45EA858 ("AlphaClipThreshold", Range(0, 1)) = 0.1
		Color_SpecialEmission ("SpecialEmission", Vector) = (0,0,0,0)
		Color_CA156002 ("SkinColor", Vector) = (0,0,0,1)
		Color_8BA909FE ("AddPersonalLight", Vector) = (1,1,1,0)
		Vector1_57D59F3B ("PersonalLightIntensity", Float) = 0
		Vector1_E02B1CF1 ("PersonalLightShadingRate", Range(0, 1)) = 0
		Vector4_EAEB1544 ("ChangeSpecularColor", Vector) = (0,0,0,0)
		Vector1_A29536A7 ("ChangeSpecularColor_Range", Range(0, 1)) = 1
		Vector1_353DD1EF ("SmoothMapRate", Range(0, 1)) = 1
		[NoScaleOffset] Texture2D_f7e7271bc5f84bf087336a9d5b61fd50 ("EmissionTex", 2D) = "white" {}
		[HDR] Color_d16b6973ed1d4b849c6e05ee4b55a0bf ("EmissionColor", Vector) = (0,0,0,0)
		[HideInInspector] _WorkflowMode ("_WorkflowMode", Float) = 1
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 1
		[HideInInspector] _ReceiveShadows ("_ReceiveShadows", Float) = 1
		[HideInInspector] _Surface ("_Surface", Float) = 0
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 1
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("_ZWrite", Float) = 1
		[HideInInspector] _ZWriteControl ("_ZWriteControl", Float) = 0
		[HideInInspector] _ZTest ("_ZTest", Float) = 4
		[HideInInspector] _Cull ("_Cull", Float) = 2
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}