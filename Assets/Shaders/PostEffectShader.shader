Shader "Custom/PostEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Cull Off 
		ZWrite Off 
		ZTest Always

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	sampler2D _RenderTexture;
	float _VFXIntensity;
	float4 _WhiteReplacement;
	float4 _BlackReplacement;


	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
	fixed4 rendTex = tex2D(_RenderTexture, i.uv);
	// sample the texture
	fixed4 col = tex2D(_MainTex, i.uv);
	fixed grayTint = (col.r + col.g + col.b) / 3.0f;
	//fixed4 grayTint = (gray, gray, gray, 1);

	//fixed4 grayTintLerp = lerp(fixed4(grayTint, grayTint, grayTint, 1) * lerp(_WhiteReplacement, _BlackReplacement, grayTint) * 0.5, col, 1-_VFXIntensity);
	fixed4 grayTintLerp = lerp(col * (lerp(_WhiteReplacement, _BlackReplacement, 1-grayTint)) * 0.5, col, 1-_VFXIntensity);

	return  grayTintLerp ;

	}
		ENDCG
	}
	}
}