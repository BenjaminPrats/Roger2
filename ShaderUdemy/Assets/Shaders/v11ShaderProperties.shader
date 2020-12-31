Shader "Udemy/ShaderProperties"
{
    Properties
    {
        _color("Color", Color) = (1, 1, 1, 1) // fixed4
        _range("Range", Range(0, 5)) = 1 // half
        _tex("Texture", 2D) = "white" {} // sampler2D
        _cube("Cube", CUBE) = "" {} // samplerCUBE
        _float("Float", Float) = 0.5 // float
        _vector("Vector", Vector) = (0.5, 1, 1, 1) // float4

        _tex3D("3D Texture", 3D) = "white" {}
        // ...
    }
        SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        fixed4      _color;
        half        _range;
        sampler2D   _tex;
        samplerCUBE _cube;
        float       _float;
        float4      _vector;

        struct Input
        {
            float2 uv_tex; // prefix uv + suffixe name of the texture property
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_tex, IN.uv_tex).rgb * _range);
            o.Emission = texCUBE(_cube, IN.worldRefl).rgb;
        }
        ENDCG
    }
        FallBack "Diffuse"
}
