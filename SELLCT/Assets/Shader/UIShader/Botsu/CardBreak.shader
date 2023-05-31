Shader "Custom/CardBreak"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Threshold("Threshold", Range(0.0, 1.0)) = 0.5
        _EdgeWidth("Edge Width", Range(0.0, 0.1)) = 0.02
        _BumpMap("Bump Map", 2D) = "bump" {}
        _BumpScale("Bump Scale", Range(0.0, 1.0)) = 0.1
    }

        SubShader{  Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _Threshold;
            float _EdgeWidth;
            sampler2D _BumpMap;
            float _BumpScale;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            float4 frag (v2f i) : SV_Target {
                float4 color = tex2D(_MainTex, i.uv);
 
                // calculate the bump value based on the bump map
                float4 bump = tex2D(_BumpMap, i.uv);
                float bumpValue = dot(bump.rgb, float3(0.299, 0.587, 0.114));
                bumpValue *= _BumpScale;
 
                // calculate the distance from the top edge of the card
                float distance = i.uv.y - _Threshold;
                distance += bumpValue;
                distance = saturate(distance);
 
                // apply the edge effect
                float edge = smoothstep(_Threshold - _EdgeWidth, _Threshold, distance);
                color.rgb = lerp(color.rgb, float3(0, 0, 0), edge);
 
                return color;
            }
            ENDCG
            }
        }
    FallBack "Diffuse"
}
