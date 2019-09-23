// Unity Phong Shader Example - Ricardo Bustamante
Shader "Unlit/Phong"
{
    Properties
    {
        [Header(Material Properties)]
        ks("Specular Material", Color) = (1,1,1,1)
        kd("Diffuse Material", Color) = (1,1,1,1)
        ka("Ambient Material", Color) = (1,1,1,1)
        a("Shininess", float) = 10
        
        [Header(Light 0 Properties)]
        is0("Light 0 Specular", Color) = (1,1,1,1)
        id0("Light 0 Diffuse", Color) = (1,1,1,1)
        l0("Light 0 Position", Vector) = (1,1,1)
        
        [Header(Light 1 Properties)]
        is1("Light 1 Specular", Color) = (1,1,1,1)
        id1("Light 1 Diffuse", Color) = (1,1,1,1)
        l1("Light 1 Position", Vector) = (1,1,1)
        
        [Header(Ambient Properties)]
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
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }
            
            // Material Properties
            fixed4 ks;
            fixed4 kd;
            fixed4 ka;
            float a;
            
            // Light 0 Properties
            fixed4 is0;
            fixed4 id0;
            fixed4 l0;
            
            // Light 1 Properties
            fixed4 is1;
            fixed4 id1;
            fixed4 l1;
            
            // Ambient Properties
            fixed4 ia;
            
            fixed4 light(fixed4 kd, fixed4 id, fixed4 N, fixed4 L)
            {
                return kd * id * ();
            }
            
            fixed4 specularLight(fixed4 ks, fixed4 is, fixed4 V, fixed4 R)
            {
                return (0,0,0,0);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = ia*ka + diffuseLight(kd, id0, i.normal, l0) + diffuseLight(kd, id1, i.normal, l1); 
                return col;
            }
            ENDCG
        }
    }
}
