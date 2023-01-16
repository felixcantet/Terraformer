Shader "Unlit/HeightStamp"
{
	Properties
	{
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"
			#include "BlendMode.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 screenPos: TEXCOORD1;
				float3 worldPos: TEXCOORD2;
			};

			// Textures
			sampler2D _Stamp;
			sampler2D _HeightMap;
			sampler2D _Mask;
			// Stamp Transform Info
			float3 _StampPosition;
			float3 _StampScale;

			// Terrain Info
			float3 _TerrainSize;
			float3 _TerrainPosition;

			// Rendering Info
			int _BlendMode;
			
			
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.screenPos = ComputeScreenPos(v.vertex);
				return o;
			}
			
			float frag (v2f i) : SV_Target
			{
				float2 screenPos = i.screenPos.xy / i.screenPos.w;
				float stampHeight = tex2D(_Stamp, i.uv).r / 2.0; // In GPU,heightmap value is clamped between 0 and 0.5
				float currentHeight = tex2D(_HeightMap, screenPos).r;
				float mask = tex2D(_Mask, i.uv);
				return Blend(stampHeight, currentHeight, mask, _BlendMode);
			}
			ENDCG
		}
	}
}