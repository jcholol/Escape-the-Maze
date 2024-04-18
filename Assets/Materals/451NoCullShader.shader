Shader "Unlit/451NoCullShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecTex ("Second Texture", 2D) = "white" {}
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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float3 vertexWC : TEXCOORD3;
            };

            // Existing light properties (Light 1, 2, 3)
            float4 LightPosition1;
            fixed4 LightColor1;
            float LightNear1;
            float LightFar1;

            float4 LightPosition2;
            fixed4 LightColor2;
            float LightNear2;
            float LightFar2;

            float4 LightPosition3;
            fixed4 LightColor3;
            float LightNear3;
            float LightFar3;
            float4 LightDirection3;
            float LightSpotAngle3;
            bool _SpotlightActive3;

            // New light properties (Light 4, 5, 6)
            float4 LightPosition4;
            fixed4 LightColor4;
            float LightNear4;
            float LightFar4;

            float4 LightPosition5;
            fixed4 LightColor5;
            float LightNear5;
            float LightFar5;

            float4 LightPosition6;
            fixed4 LightColor6;
            float LightNear6;
            float LightFar6;

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecTex;
            float4 _SecTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv1 = TRANSFORM_TEX(v.uv1, _SecTex);
                o.vertexWC = mul(UNITY_MATRIX_M, v.vertex);
                float3 p = v.vertex + 10 * v.normal;
                p = mul(UNITY_MATRIX_M, float4(p,1));  
                o.normal = normalize(p - o.vertexWC); 
                return o;
            }
float ComputeDiffuse(v2f i, float4 lightPos, float lightNear, float lightFar, float4 lightDir, float spotAngle, int lightIndex) 
{
    if (lightIndex == 3 && _SpotlightActive3 == 0) 
    {
        return 0; // No contribution from this light
    }
    float3 l = lightPos - i.vertexWC;
    float d = length(l);
    l = l / d;
    float strength = 1;

    float ndotl = clamp(dot(i.normal, l), 0, 1);
    if (d > lightNear) 
    {
        if (d < lightFar) 
        {
            float range = lightFar - lightNear;
            float n = d - lightNear;
            strength = smoothstep(0, 1, 1.0 - (n*n) / (range*range));
        }
        else 
        {
            strength = 0;
        }
    }

    if (lightIndex == 3 && _SpotlightActive3) 
    {
        float ndotdir = dot(-l, lightDir.xyz);
        float spotCosine = cos(spotAngle * 0.5 * 3.14159 / 180.0);
        if (ndotdir > spotCosine) 
        {
            strength *= smoothstep(spotCosine, 1.0, ndotdir);
        }
        else 
        {
            strength = 0;
        }
    }
    return ndotl * strength;
}


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col1 = tex2D(_MainTex, i.uv);
                fixed4 col2 = tex2D(_SecTex, i.uv1);

                // Compute diffuse for each light
                float diff1 = ComputeDiffuse(i, LightPosition1, LightNear1, LightFar1, float4(0,0,0,0), 0, 1);
                float diff2 = ComputeDiffuse(i, LightPosition2, LightNear2, LightFar2, float4(0,0,0,0), 0, 2);
                float diff3 = ComputeDiffuse(i, LightPosition3, LightNear3, LightFar3, LightDirection3, LightSpotAngle3, 3);
                float diff4 = ComputeDiffuse(i, LightPosition4, LightNear4, LightFar4, float4(0,0,0,0), 0, 4);
                float diff5 = ComputeDiffuse(i, LightPosition5, LightNear5, LightFar5, float4(0,0,0,0), 0, 5);
                float diff6 = ComputeDiffuse(i, LightPosition6, LightNear6, LightFar6, float4(0,0,0,0), 0, 6);

                // Combine colors from all lights
                return (col1*.2 + col2*.8) * (diff1 * LightColor1 + diff2 * LightColor2 + diff3 * LightColor3 + diff4 * LightColor4 + diff5 * LightColor5 + diff6 * LightColor6);
            }
            ENDCG
        }
    }
}
