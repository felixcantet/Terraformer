float4 Blend(float4 from, float4 to, float4 mask, int blendMode)
{
    return lerp(from, to, mask);
}