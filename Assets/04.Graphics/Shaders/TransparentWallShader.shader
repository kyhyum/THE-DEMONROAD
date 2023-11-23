Shader "Universal Render Pipeline/TransparentShaderURP"
{
    Properties
    {
        _Color("Main Color", Color) = (1, 1, 1, 0.5)
        [HDR] _BaseMap("Base (RGB)", 2D) = "white" { }
    }

        SubShader
    {
        Tags {"Queue" = "Overlay" }
        LOD 100

        Pass
        {
            Name "Overlay"
            Tags {"LightMode" = "Overlay"}

            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 _Color;
            sampler2D _BaseMap;

            struct Input
            {
                float2 uv_MainTex;
            };

            fixed4 frag(Input IN) : COLOR
            {
                fixed4 c = tex2D(_BaseMap, IN.uv_MainTex) * _Color;
                return c;
            }
            ENDCG
        }
    }

        Fallback "Diffuse"
}