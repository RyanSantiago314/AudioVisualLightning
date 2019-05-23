
//Adapted from Example 5.3 in The CG Tutorial by Fernando & Kilgard
Shader "CM163/LightningParticles"
{
    Properties
    {
        _EmissiveColor("Emissive Color", Color) = (1, 1, 1, 1)
        _Emissiveness("Emissiveness", Range(0,10)) = 0
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Opaque" }
        LOD 100

        Blend One One
        ZWrite off
        Pass {
            Tags { "LightMode" = "ForwardAdd" } //Important! In Unity, point lights are calculated in the the ForwardAdd pass
            Blend One One //Turn on additive blending if you have more than one point light
          
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
           
            uniform float4 _LightColor0; //From UnityCG
    
            uniform float4 _EmissiveColor;
            uniform float _Emissiveness;
            uniform float _Opacity;
            sampler _MainTex;       
          
            struct appdata
            {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                    float4 vertex : SV_POSITION;
                    float3 normal : NORMAL;       
                    float3 vertexInWorldCoords : TEXCOORD1;
                    float2 uv: TEXCOORD0;
            };

 
           v2f vert(appdata v)
           { 
                v2f o;
                o.vertexInWorldCoords = mul(unity_ObjectToWorld, v.vertex); //Vertex position in WORLD coords
                o.normal = v.normal; //Normal 
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex); 
                
              

                return o;
           }

           fixed4 frag(v2f i) : SV_Target
           {
                
                float4 texColor = tex2D(_MainTex, i.uv);
                //FINAL COLOR OF FRAGMENT
                return float4(_EmissiveColor * _Emissiveness)*texColor;
 
            }
            
            ENDCG
 
            
        }
            
    }
}
