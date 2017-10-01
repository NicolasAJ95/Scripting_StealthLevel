Shader "Unlit/BlurEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

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
	float4 _MainTex_TexelSize;
	float _BlurOffset;
	float _VFXIntensity;
	sampler2D _Mask;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		float mask = tex2D(_Mask, i.uv).r;

		fixed4 centerTexel = tex2D(_MainTex, i.uv);
		fixed4 rightTexel = tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x, 0.0));
		fixed4 uprightTexel = tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y));
		fixed4 upTexel = tex2D(_MainTex, i.uv + float2(0.0, _MainTex_TexelSize.y));
		fixed4 upleftTexel = tex2D(_MainTex, i.uv + float2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y));
		fixed4 leftTexel = tex2D(_MainTex, i.uv + float2(-_MainTex_TexelSize.x, 0.0));
		fixed4 downleftTexel = tex2D(_MainTex, i.uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y));
		fixed4 downTexel = tex2D(_MainTex, i.uv + float2(0.0, -_MainTex_TexelSize.y));
		fixed4 downrightTexel = tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y));

		fixed4 blur = (centerTexel + rightTexel + uprightTexel + upTexel + upleftTexel + leftTexel + downleftTexel + downTexel + downrightTexel)/9;
		fixed4 finalBlur = lerp(centerTexel, blur, _BlurOffset);

		return lerp (centerTexel, lerp (blur, centerTexel, mask), _VFXIntensity);
		//return mask;
	}
		ENDCG
	}
	
}
}