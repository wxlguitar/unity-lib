Shader "LOL/NoLitColorModVF(cull front)" 
{
	Properties
	{
		_ModColor ("Modulate Color", Color) = (0.5, 0.5, 0.5, 0.5)	
		_MainTex ("Base", 2D) = "black" {}
	}

	//BindChannels
	//{		
	//	Bind "Vertex", vertex
	//	Bind "TexCoord", texcoord0
	//}
		
	// ---- Fragment program cards
	SubShader
	{
		Tags { "Queue"="Geometry" "RenderType"="Opaque" "IgnoreProjector"="True" }

		ColorMask RGB
		Cull Front 
		Lighting Off 
		ZWrite On 
		ZTest LEqual
		AlphaTest Off
		Blend SrcAlpha OneMinusSrcAlpha		
		Fog { Mode Off } 
		//Stencil
		//{
		//	Comp Always
		//}

		Pass
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			//#pragma multi_compile_particles
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;	
			fixed4 _ModColor;	
			
			struct appdata_t
			{
				float4 vertex : POSITION;						
				half2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;				
				half2 texcoord : TEXCOORD0;
			};	
			
			half4 _MainTex_ST;		

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);				
				//o.texcoord = v.texcoord;	
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);			
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 texColor = tex2D(_MainTex, i.texcoord);		
				return texColor * _ModColor * 2.0;
			}
			ENDCG 
		}
	} 
		
	// ---- Single texture cards (does not do color tint)
	SubShader
	{
		Pass
		{
			SetTexture [_MainTex]
			{
				combine texture * primary
			}
		}
	}
}
