#version 330 core
layout (location = 0) out vec4 gPosition;
layout (location = 1) out vec3 gNormal;
layout (location = 2) out vec4 gAlbedo;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} fs_in;

const float NEAR = 0.1; // Projection matrix's near plane distance
const float FAR = 50.0f; // Projection matrix's far plane distance
float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // Back to NDC
    return (2.0 * NEAR * FAR) / (FAR + NEAR - z * (FAR - NEAR));
}

void main()
{
    // 存储第一个G缓冲纹理中的片段位置向量
    gPosition.xyz = fs_in.FragPos;
    // And store linear depth into gPositionDepth's alpha component
    gPosition.a = LinearizeDepth(gl_FragCoord.z);
    // 同样存储对每个逐片段法线到G缓冲中
    gNormal = normalize(fs_in.Normal);
    // 和漫反射对每个逐片段颜色
    gAlbedo.rgb = vec3(0.95);
}
