// rewritten shader by Joseph Hocking
// original https://web.archive.org/web/20200630011331/http://wiki.unity3d.com/index.php/SkyboxBlended

Shader "Skybox/Blended" {
Properties {
    _Tint ("Tint Color", Color) = (.5, .5, .5, .5)
    [Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
    _Blend ("Blend", Range(0, 1)) = 0.5
    [NoScaleOffset] _FrontTex ("Front (+Z)   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _BackTex ("Back [-Z]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _LeftTex ("Left [+X]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _RightTex ("Right [-X]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _UpTex ("Up [+Y]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _DownTex ("Down [-Y]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _FrontTex2 ("2 Front (+Z)   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _BackTex2 ("2 Back [-Z]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _LeftTex2 ("2 Left [+X]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _RightTex2 ("2 Right [-X]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _UpTex2 ("2 Up [+Y]   (HDR)", 2D) = "white" {}
    [NoScaleOffset] _DownTex2 ("2 Down [-Y]   (HDR)", 2D) = "white" {}
}

SubShader {
    Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
    Cull Off ZWrite Off

    CGINCLUDE
    #include "UnityCG.cginc"

    half4 _Tint;
    half _Exposure;
    float _Blend;

    struct appdata_t {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };
    struct v2f {
        float4 vertex : SV_POSITION;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    v2f vert (appdata_t v)
    {
        v2f o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = v.texcoord;
        return o;
    }
    half4 skybox_frag (v2f i, sampler2D smp1, half4 smpDecode1, sampler2D smp2, half4 smpDecode2)
    {
        half4 tex1 = tex2D(smp1, i.texcoord);
        half3 c1 = DecodeHDR(tex1, smpDecode1);
        c1 = c1 * _Tint.rgb * unity_ColorSpaceDouble.rgb;

        half4 tex2 = tex2D(smp2, i.texcoord);
        half3 c2 = DecodeHDR(tex2, smpDecode2);
        c2 = c2 * _Tint.rgb * unity_ColorSpaceDouble.rgb;

        half3 c = lerp(c1, c2, _Blend) * _Exposure;
        return half4(c, 1);
    }
    ENDCG

    Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _FrontTex;
        half4 _FrontTex_HDR;
        sampler2D _FrontTex2;
        half4 _FrontTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _FrontTex, _FrontTex_HDR, _FrontTex2, _FrontTex2_HDR); }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _BackTex;
        half4 _BackTex_HDR;
        sampler2D _BackTex2;
        half4 _BackTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _BackTex, _BackTex_HDR, _BackTex2, _BackTex2_HDR); }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _LeftTex;
        half4 _LeftTex_HDR;
        sampler2D _LeftTex2;
        half4 _LeftTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _LeftTex, _LeftTex_HDR, _LeftTex2, _LeftTex2_HDR); }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _RightTex;
        half4 _RightTex_HDR;
        sampler2D _RightTex2;
        half4 _RightTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _RightTex, _RightTex_HDR, _RightTex2, _RightTex2_HDR); }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _UpTex;
        half4 _UpTex_HDR;
        sampler2D _UpTex2;
        half4 _UpTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _UpTex, _UpTex_HDR, _UpTex2, _UpTex2_HDR); }
        ENDCG
    }
    Pass{
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        sampler2D _DownTex;
        half4 _DownTex_HDR;
        sampler2D _DownTex2;
        half4 _DownTex2_HDR;
        half4 frag (v2f i) : SV_Target { return skybox_frag(i, _DownTex, _DownTex_HDR, _DownTex2, _DownTex2_HDR); }
        ENDCG
    }
}

Fallback "Skybox/6 Sided", 1
}
