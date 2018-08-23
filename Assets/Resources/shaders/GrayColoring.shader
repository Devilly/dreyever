// Used reference for algorithms: http://www.tannerhelland.com/3643/grayscale-image-algorithm-vb6/.

Shader "Dreyever/GrayColoring"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Type("Type", Int) = 1
		_NumberOfShades("Number of shades of gray", Range(1, 255)) = 10
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
			int _Type;
			int _NumberOfShades;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				// averaging
				if(_Type == 1) {
					
					col.rgb = (col.r + col.g + col.b) / 3;
				}
				// correcting
				else if(_Type == 2) {
					col.rgb = col.r * .3 + col.g * .59 + col.b * .11;
				}
				// desaturation
				else if(_Type == 3) {
					col.rgb = (max(max(col.r, col.g), col.b) + min(min(col.r, col.g), col.b)) / 2;
				}
				// decomposition, maximum value
				else if(_Type == 4) {
					col.rgb = max(max(col.r, col.g), col.b);
				}
				// decomposition, minimum value
				else if(_Type == 5) {
					col.rgb = min(min(col.r, col.g), col.b);
				}
				// single color channel, red
				else if(_Type == 6) {
					col.rgb = col.r;
				}
				// single color channel, green
				else if(_Type == 7) {
					col.rgb = col.g;
				}
				// single color channel, red
				else if(_Type == 8) {
					col.rgb = col.b;
				}
				// user chosen number of shades of gray
				else if(_Type == 9) {
					float shadeSize = 255 / _NumberOfShades;
					float average = (col.r + col.g + col.b) / 3 * 255;
					col.rgb = round(average / shadeSize) * shadeSize / 255;
				}
				
				return col;
			}
			ENDCG
		}
	}
}
