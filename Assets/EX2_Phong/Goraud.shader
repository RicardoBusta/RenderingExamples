// Unity Goraud Shader Example - Ricardo Bustamante
Shader "Unlit/Goraud"
{
    Properties
    {
        [Header(Material Properties)]
        ks("Specular Material", Color) = (1,1,1,1)
        kd("Diffuse Material", Color) = (1,1,1,1)
        ka("Ambient Material", Color) = (1,1,1,1)
        a("Shininess", float) = 10
        
        [Header(Light 0 Properties)]
        l0value ("Light 0 Value", Range (0, 1)) = 1
        is0("Light 0 Specular", Color) = (1,1,1,1)
        id0("Light 0 Diffuse", Color) = (1,1,1,1)
        l0("Light 0 Position", Vector) = (1,1,1)
        
        [Header(Light 1 Properties)]
        l1value ("Light 1 Value", Range (0, 1)) = 1
        is1("Light 1 Specular", Color) = (1,1,1,1)
        id1("Light 1 Diffuse", Color) = (1,1,1,1)
        l1("Light 1 Position", Vector) = (1,1,1)
        
        [Header(Ambient Properties)]
        aValue ("Ambient Value", Range (0, 1)) = 1
        ia("Ambient Light", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags { "LightMode" = "ForwardBase" } 
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            // Material Properties
            fixed3 ks;
            fixed3 kd;
            fixed3 ka;
            float a;
            
            // Light 0 Properties
            float l0value;
            fixed3 is0;
            fixed3 id0;
            fixed3 l0;
            
            // Light 1 Properties
            float l1value;
            fixed3 is1;
            fixed3 id1;
            fixed3 l1;
            
            // Ambient Properties
            float aValue;
            fixed3 ia;
            
            fixed4 light(fixed3 id, fixed3 is, fixed3 N, fixed3 V, fixed3 vpos, fixed3 lpos)
            {
                float3 L = normalize(lpos-vpos); // light source direction
                float3 diffuse = kd * id * (dot(N,L));
                
                float3 R = normalize((2*dot(L, N))*N-L); // reflection vector
                
                // Clamp is important to avoid color subtraction
                float3 specular = clamp(ks * is * (pow(dot(R, V),a)), 0, 1);
                return fixed4(diffuse + specular, 1);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 N = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
                float3 vpos = UnityObjectToClipPos(v.vertex).xyz;
                float3 V = normalize(_WorldSpaceCameraPos - vpos);
                
                o.color = fixed4(ia*ka*aValue, 1) + // ambient
                          light(id0, is0, N, V, vpos, l0)*l0value + // light 0
                          light(id1, is1, N, V, vpos, l1)*l1value; // light 1
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            { 
                fixed4 col = i.color;
                return col;
            }
            ENDCG
        }
    }
}
