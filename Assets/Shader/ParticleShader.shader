Shader "Custom/ParticleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //Define properties for Start and End Color
        _StartCol ("Start Color",Color) = (0,0,0,1)
        _EndCol ("End Color",Color) = (0,0,0,1)

    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Opaque" }
        LOD 100
        
        Blend One One
        ZWrite off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            float4 _StartCol;
            float4 _EndCol;

            struct appdata
            {
                float4 vertex : POSITION;
                //Define UV data
                float3 uv : TEXCOORD0;
            };

            struct v2f
            {   
                float4 vertex : SV_POSITION;
                //Define UV data
                float3 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
          

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = v.uv; Correct this for particle shader
                
                o.uv = v.uv;
                //o.uv.z = v.uv.z;

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                //Get particle age percentage
                float particleAgePercentage = i.uv.z;

                //Sample color from particle texture
                float4 col = tex2D(_MainTex,i.uv.xy);

                //Find "start color"
                float4 start = _StartCol;
                //Find "end color"
                float4 end = _EndCol;
                
                //Do a linear interpolation of start color and end color based on particle age percentage

                float4 finalCol = lerp(start,end,particleAgePercentage)*(col.a);

                return finalCol;
            }
            ENDCG
        }
    }
}
