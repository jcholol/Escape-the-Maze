Shader "Unlit/451NoCullShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecTex ("Second Texture", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1) // Emission color property
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.5 // Blend factor for combining textures

        // Matrix elements for animated transformation
        _Matrix00 ("Matrix00", Float) = 1.0
        _Matrix01 ("Matrix01", Float) = 0.0
        _Matrix02 ("Matrix02", Float) = 0.0
        _Matrix10 ("Matrix10", Float) = 0.0
        _Matrix11 ("Matrix11", Float) = 1.0
        _Matrix12 ("Matrix12", Float) = 0.0
        _Matrix20 ("Matrix20", Float) = 0.0
        _Matrix21 ("Matrix21", Float) = 0.0
        _Matrix22 ("Matrix22", Float) = 1.0

        // Matrix elements for static transformation
        _StaticMatrix00 ("Static Matrix00", Float) = 1.0
        _StaticMatrix01 ("Static Matrix01", Float) = 0.0
        _StaticMatrix02 ("Static Matrix02", Float) = 0.0
        _StaticMatrix10 ("Static Matrix10", Float) = 0.0
        _StaticMatrix11 ("Static Matrix11", Float) = 1.0
        _StaticMatrix12 ("Static Matrix12", Float) = 0.0
        _StaticMatrix20 ("Static Matrix20", Float) = 0.0
        _StaticMatrix21 ("Static Matrix21", Float) = 0.0
        _StaticMatrix22 ("Static Matrix22", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

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
                float2 uvSec : TEXCOORD1; // Second UV coordinate for the second texture
            };

            sampler2D _MainTex;
            sampler2D _SecTex;
            fixed4 _EmissionColor; // Emission color variable
            float _BlendFactor; // Blend factor for texture blending

            // Animated matrix elements
            float _Matrix00;
            float _Matrix01;
            float _Matrix02;
            float _Matrix10;
            float _Matrix11;
            float _Matrix12;
            float _Matrix20;
            float _Matrix21;
            float _Matrix22;

            // Static matrix elements
            float _StaticMatrix00;
            float _StaticMatrix01;
            float _StaticMatrix02;
            float _StaticMatrix10;
            float _StaticMatrix11;
            float _StaticMatrix12;
            float _StaticMatrix20;
            float _StaticMatrix21;
            float _StaticMatrix22;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Construct the animated matrix
                float3x3 animatedMatrix = float3x3(
                    _Matrix00, _Matrix01, _Matrix02,
                    _Matrix10, _Matrix11, _Matrix12,
                    _Matrix20, _Matrix21, _Matrix22
                );

                // Construct the static matrix
                float3x3 staticMatrix = float3x3(
                    _StaticMatrix00, _StaticMatrix01, _StaticMatrix02,
                    _StaticMatrix10, _StaticMatrix11, _StaticMatrix12,
                    _StaticMatrix20, _StaticMatrix21, _StaticMatrix22
                );

                // Apply the animated matrix to the first texture's UVs
                float3 uvTransformed = mul(animatedMatrix, float3(v.uv, 1));
                o.uv = uvTransformed.xy;

                // Apply the static matrix to the second texture's UVs
                float3 uvSecTransformed = mul(staticMatrix, float3(v.uv, 1));
                o.uvSec = uvSecTransformed.xy;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the first texture and apply the matrix transformation
                fixed4 col = tex2D(_MainTex, i.uv);

                // Sample the second texture with the static transformation
                fixed4 colSec = tex2D(_SecTex, i.uvSec);

                // Blend the two textures based on the blend factor
                fixed4 finalColor = lerp(col, colSec, _BlendFactor);

                // Add emission color to the fragment color
                finalColor += _EmissionColor;

                return finalColor;
            }
            ENDCG
        }
    }
}
