Shader "Custom/Outline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Colour ("Colour", color) = (0.5, 0.5, 0.5, 1)
		_OutlineColour ("Outline colour", color) = (0, 0, 0, 1)
		_OutlineWidth ("Outline width", Range(1.0, 5.0)) = 1.01
	}

	SubShader
	{
		Tags {"Queue" = "Transparent+1"}
		CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float4 pos : POSITION;
			float4 colour : COLOR;
			float3 normal : NORMAL;
		};

		float4 _OutlineColour;
		float _OutlineWidth;

		v2f vert(appdata v)
		{
			v2f output;
			v.vertex.xyz *= _OutlineWidth;
			output.pos = UnityObjectToClipPos(v.vertex);
			output.colour = _OutlineColour;
			return output;
		}
		ENDCG

		pass
		{
			ZWrite Off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				half4 frag (v2f i) : COLOR
				{
					return i.colour;
				}
			ENDCG
		}

		pass
		{
			ZWrite On
			Material
			{
				Diffuse[_Colour]
				Ambient[_Colour]
			}
			Lighting On
			SetTexture[_MainTex]
			{
				ConstantColor[_Colour]
			}
			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
}
