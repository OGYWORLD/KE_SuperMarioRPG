Shader "Shader Graphs/PBRLit_FresnelAdd" {
	Properties {
		[NoScaleOffset] _MainTex ("AlbedMap", 2D) = "white" {}
		[NoScaleOffset] [Normal] Texture2D_2A45DA31 ("NormalMap", 2D) = "bump" {}
		Vector1_7388E1D8 ("NormalMapStrength", Range(0, 4)) = 1
		[NoScaleOffset] Texture2D_7923EF37 ("MultiMap(Smoothness,AO,Brank,Metalic)", 2D) = "white" {}
		Color_37CAC7DB ("Albed", Vector) = (1,1,1,0)
		Vector1_B40A056E ("Metalic", Range(0, 1)) = 0
		Vector1_2F04771C ("Smoothness", Range(0, 1)) = 0.5
		Vector1_BA802B87 ("AmbientOcculusion", Range(0, 1)) = 1
		Vector1_E173C7DF ("FrenelColorAngle", Range(0, 360)) = 0
		Color_7A81B365 ("FresnelColorA", Vector) = (1,0,0,1)
		Vector1_FE42F863 ("ThreshouldA", Range(0, 1)) = 0
		Color_F87EAD4 ("FresnelColorB", Vector) = (0.1881378,1,0,1)
		Vector1_4AAD1857 ("ThreshouldB", Range(0, 1)) = 1
		Vector1_7D6E59DF ("FresnelPower", Float) = 1
		Vector1_EF7CA402 ("FresnelOpacity", Range(0, 1)) = 1
		Vector1_8624AC6D ("ThreshouldLow", Range(0, 1)) = 0
		Vector1_B2A63013 ("ThreshouldHigh", Range(0, 1)) = 1
		Vector1_A45EA858 ("AlphaClipThreshold", Range(0, 1)) = 0.5
		[ToggleUI] Boolean_869666A1 ("AlphaClipping", Float) = 0
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
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}