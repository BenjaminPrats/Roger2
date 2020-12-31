Shader "Udemy/PropertyChallenge4"
{
    Properties
    {
        _texDiffuse("Diffuse Texture", 2D) = "white" {}
        // black to avoid over exposition when only diffuse is given
        _texEmissive("Emissive Texture", 2D) = "black" {}
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D   _texDiffuse;
        sampler2D   _texEmissive;

        struct Input
        {
            float2 uv_texDiffuse; // prefix uv + suffixe name of the texture property
            float2 uv_texEmissive; // prefix uv + suffixe name of the texture property
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_texDiffuse, IN.uv_texDiffuse).rgb;
            o.Emission = tex2D(_texEmissive, IN.uv_texEmissive).rgb;
        }
        ENDCG
    }


    FallBack "Diffuse"
}
