// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XRay/XRayVision"
{
	Properties
	{	
		_EdgeColor("Rim color", Color) = (1,1,1,1)
	}

	SubShader
	{

		Stencil
		{
			Ref 1
			Comp GEqual
			Pass Keep
			ZFail Replace
		} 
			ZWrite On
			ZTest Always
			Blend One One

		Tags
		{

			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"XRay" = "ColoredOutline"
		}


		Pass
		{

			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float _VFXIntensity;
			float4 _EdgeColor;
			
			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				// object space vertex
				// object space normal
			};

			struct v2f
			{				
				float4 vertex : SV_POSITION;
				float3 worldSpaceVertex : TEXCOORD0;
				float3 worldSpaceNormal : TEXCOORD1;
				// screen space vertex
				// + 2 other elements
			};
			
			v2f vert (appdata v)
			{			
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldSpaceNormal = (mul(unity_ObjectToWorld, float4(v.normal, 0.0)));
				o.worldSpaceVertex = (mul(unity_ObjectToWorld, v.vertex));

				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				float NdotV;

				float3 N = normalize(i.worldSpaceNormal);
				float3 V = normalize(_WorldSpaceCameraPos - i.worldSpaceVertex.xyz);

				NdotV = saturate(1 - dot(N,V) * 0.5f);

				float4 rim = _EdgeColor * NdotV;
				return rim * _VFXIntensity;

			}

			ENDCG
		}
	}
}
