Shader "Udemy/NormalMap"
{
    Properties
    {
        _texDiffuse("Diffuse Texture", 2D) = "white" {}
        _texNormal("Normal Texture", 2D) = "bump" {}
        _slider("Bump Amount", Range(0,10)) = 1
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D   _texDiffuse;
        sampler2D   _texNormal;
        half        _slider;

        struct Input
        {
            float2 uv_texDiffuse; // prefix uv + suffixe name of the texture property
            float2 uv_texNormal; // prefix uv + suffixe name of the texture property
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_texDiffuse, IN.uv_texDiffuse).rgb;
            o.Normal = UnpackNormal(tex2D(_texNormal, IN.uv_texNormal));
            o.Normal *= float3(_slider, _slider, 1);
        }
        ENDCG
    }


    FallBack "Diffuse"
}
