// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Rainbow shader with lots of adjustable properties!

Shader "_Shaders/Rainbow" {

	Properties {
		_HueAlgorithm("Hue Algorithm Number (1-4)", Range(1, 4)) = 1
		_LineFrequency("Line Frequency", Range(0, 100)) = 20
		_Saturation("Saturation", Range(0.0, 1.0)) = 0.8
		_Luminosity("Luminosity", Range(0.0, 1.0)) = 0.5
		_Spread("Spread", Range(0.5, 10.0)) = 3.8
		_Speed("Speed", Range(-10.0, 10.0)) = 2.4
		_TimeOffset("TimeOffset", Range(0.0, 6.28318531)) = 0.0
	}

	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Shared/ShaderTools.cginc"

			fixed _HueAlgorithm;
			fixed _LineFrequency;
			fixed _Saturation;
			fixed _Luminosity;
			half _Spread;
			half _Speed;
			half _TimeOffset;

			struct vertexInput {
				float4 vertex : POSITION;
				float4 texcoord0 : TEXCOORD0;
			};

			struct fragmentInput {
				float4 position : SV_POSITION;
				float4 texcoord0 : TEXCOORD0;
				fixed3 localPosition : TEXCOORD1;
			};

			fragmentInput vert(vertexInput i) {
				fragmentInput o;
				o.position = UnityObjectToClipPos(i.vertex);
				o.texcoord0 = i.texcoord0;
				o.localPosition = i.vertex.xyz; +fixed3(0.5, 0.5, 0.5);
				return o;
			}

			fixed4 frag(fragmentInput i) : SV_TARGET {
				fixed2 lPos = i.localPosition / _Spread;
				half time = _Time.y * _Speed / _Spread;
				half timeWithOffset = time + _TimeOffset;
				fixed sine = sin(timeWithOffset);
				fixed cosine = cos(timeWithOffset);
				fixed hue = (-lPos.y) * _LineFrequency;

				fixed2 cir =	(sin(lPos.x + timeWithOffset) * sin(lPos.y + timeWithOffset) / 25.0) + 
								(lPos.x * sin(timeWithOffset) + lPos.y * sin(timeWithOffset) / 25.0);

				// Set the hue based on the chosen algorithm
				if (_HueAlgorithm < 2)			hue = (sqrt(abs(cir.x * cir.y) * 15.0) / 12.0);
				else if (_HueAlgorithm < 3)		hue = (sqrt(abs(cir.x + cir.y)) * 10.0);
				else if (_HueAlgorithm < 4)		hue = cir.x / timeWithOffset + cir.y * timeWithOffset;
				else							hue = cir.x * timeWithOffset + cir.y / timeWithOffset;

				hue *= _LineFrequency;
				hue += time;
				while (hue < 0.0) hue += 1.0;
				while (hue > 1.0) hue -= 1.0;

				fixed4 hsl = fixed4(hue, _Saturation, _Luminosity, 1.0);
				return HSLtoRGB(hsl);
			}

			ENDCG
		}
	}
	
	FallBack "Diffuse"
}