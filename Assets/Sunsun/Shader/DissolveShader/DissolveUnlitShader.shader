Shader "Unlit/DissolveUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
          //Dissolve properties
        //（0 不溶解，1 完全消失）
        //_DissolveTexture  增加溶解的材質欄位
		_DissolveTexture("Dissolve Texture", 2D) = "white" {} 
		_Amount("Amount", Range(0,1)) = 0
    }
    SubShader
    {
        //不透明
        Tags { "RenderType"="Opaque" }
        LOD 100
          //讓材質雙面渲染
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            //Dissolve properties
        //控制遮罩貼圖的變數
        sampler2D _DissolveTexture;
		half _Amount;

             //基本材質參數變數
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
      //決定這個像素「要不要畫出來」、「畫成什麼樣」,處理 render的主要區域
            fixed4 frag (v2f i) : SV_Target
            {
                    //Dissolve function
			half dissolve_value = tex2D(_DissolveTexture, i.uv).r;
			clip(dissolve_value - _Amount);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
