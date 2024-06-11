Shader "Custom/Wireframe" {
	Properties {
		[PowerSlider(3.0)] _WireframeVal ("Wireframe width", Range(0, 0.5)) = 0.005
		_FrontColor ("Front color", Vector) = (1,1,1,1)
		_BackColor ("Back color", Vector) = (1,1,1,1)
		_FaceColor ("Face color", Vector) = (0.5,0.5,1,0.5)
		[Toggle] _RemoveDiag ("Remove diagonals?", Float) = 0
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
}