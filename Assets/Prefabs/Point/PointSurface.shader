Shader "Custom/PointSurface"
{
    Properties {
		_Smoothness ("Smoothness", Range(0,1)) = 0.5
	}

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        // ConfigureSurface is the name of the method used to configure the shader
        #pragma surface ConfigureSurface Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
        };

        float _Smoothness;
    
        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface) {
            surface.Albedo.rg = ( input.worldPos.xy + 1.0 ) / 2.0;
            surface.Smoothness = _Smoothness;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
