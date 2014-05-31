//via Sync1B
//http://forum.unity3d.com/threads/1554-Materials-with-Both-Transparency-and-Self-Illumination?p=10094&viewfull=1#post10094

        Shader "AlphaSelfIllum" {
        Properties {
            _Color ("Color Tint", Color) = (1,1,1,1)
            _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
        }
        Category {
           Lighting On
           ZWrite Off
           Cull Back
           Blend SrcAlpha OneMinusSrcAlpha
           Tags {Queue=Transparent}
           SubShader {
           Tags { "RenderType"="Transparent" }
                Material {
                   Emission [_Color]
                }
                Pass {
                   SetTexture [_MainTex] {
                          Combine Texture * Primary, Texture * Primary
                    }
                }
            }
        }
    }
