Shader "Custom/PortalShader"
{
    Properties
    {
        _InactiveColor("Inactive Color", Color) = (1,1,1,1)
        _Radius("Radius", Range(0.0, 1.0)) = 0.5
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        CULL Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };

            struct interpolators
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float2 uvs : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _InactiveColor;
            float _Radius;
            int displayMask;

            interpolators vert(appdata v)
            {
                interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uvs = v.uv0;
                return o;
            }

            fixed4 frag(interpolators i) : SV_TARGET
            {
                float2 uvCentered = i.uvs * 2 - 1;
                float rDistance = length(uvCentered);
                if (rDistance >= _Radius)
                    clip(-1);
                float2 uv = i.screenPos.xy / i.screenPos.w;
                fixed4 portalCol = tex2D(_MainTex, uv);
                return portalCol * displayMask + _InactiveColor * (1 - displayMask);
            }
            ENDCG
        }
    }
    FallBack "Off"
}
