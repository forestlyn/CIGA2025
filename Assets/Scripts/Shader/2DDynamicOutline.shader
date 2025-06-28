Shader "Custom/SpriteEdgeFlow"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
        _EdgeWidth ("Edge Width", Range(0, 0.1)) = 0.02
        _FlowSpeed ("Flow Speed", Float) = 1.0
        _FlowScale ("Flow Scale", Float) = 10.0
        [Toggle(ENABLE_OUTLINE)] _EnableOutline ("Enable Outline", Float) = 1
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            fixed4 _Color;
            fixed4 _EdgeColor;
            float _EdgeWidth;
            float _FlowSpeed;
            float _FlowScale;
            float _EnableOutline;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            // 检测边缘
            float edgeDetect(float2 uv)
            {
                fixed4 center = tex2D(_MainTex, uv);
                if (center.a == 0) return 0;
                
                // 检查周围像素
                float up = tex2D(_MainTex, uv + float2(0, _MainTex_TexelSize.y)).a;
                float down = tex2D(_MainTex, uv - float2(0, _MainTex_TexelSize.y)).a;
                float left = tex2D(_MainTex, uv - float2(_MainTex_TexelSize.x, 0)).a;
                float right = tex2D(_MainTex, uv + float2(_MainTex_TexelSize.x, 0)).a;
                
                // 如果周围有透明像素，则认为是边缘
                return (up == 0 || down == 0 || left == 0 || right == 0) ? 1 : 0;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                if(_EnableOutline < 0.5) return col; // 如果未启用边缘效果，直接返回颜色
                // 边缘检测
                float edge = edgeDetect(i.uv);
                
                if (edge > 0)
                {
                    // 计算流动效果 - 使用UV坐标和时间创建动画
                    float flow = sin(i.uv.x * _FlowScale + _Time.y * _FlowSpeed) * 0.5 + 0.5;
                    
                    // 应用边缘颜色和流动效果
                    col.rgb = lerp(col.rgb, _EdgeColor.rgb, flow);
                    col.a = max(col.a, _EdgeColor.a * flow);
                }
                return col;
            }
            ENDCG
        }
    }
}