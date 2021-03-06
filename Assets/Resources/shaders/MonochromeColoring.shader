﻿Shader "Dreyever/MonochromeColoring"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0,0,0,1)
	}
	SubShader
	{
		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent"
		}

		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata_img v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb = _Color;
				col.a *= _Color.a;
				return col;
			}
			ENDCG
		}
	}
}
