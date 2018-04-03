Shader "Custom/Planet Shader" {
	Properties {
		_Top ("Top", Color) = (1,1,1,1)
        _Bot("Bot", Color) = (1,1,1,1)
        _Border("Border", Color) = (1,1,1,1)
        _Blend("Blend", Range(0,1)) = 0.5
        _Cut("Cut", Range(0,1)) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv;
            float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
        half _Blend;
        half _Cut;
        fixed4 _Top;
        fixed4 _Bot;
        fixed4 _Border;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
            float3 up = float3(0, 1, 0);
            half d = dot(IN.worldNormal, up);
            half t = (d + 1) / 2;
            half b = (t - (_Cut - _Blend / 2)) / max(1e-10, _Blend);
                        
            _Border.a = (1 - ((b * 2) - 1) * ((b * 2) - 1)) * _Border.a;
            fixed4 color = ((1 - b) * _Bot + b * _Top) * (1 - _Border.a) + (_Border.a * _Border);
            color = lerp(_Top, color, step(t, _Cut + _Blend / 2)); // This is an IF
            color = lerp(_Bot, color, step(_Cut - _Blend / 2, t)); // An other one
            o.Albedo = color.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Top.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
