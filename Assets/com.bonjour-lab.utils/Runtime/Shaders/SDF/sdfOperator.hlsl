#include "../Utils/maths.hlsl"

float2 opUnite(float2 d1, float2 d2){
    return (d1.x < d2.x) ? d1 : d2;
}

float opUnite(float d1, float d2){
    return (d1 < d2) ? d1 : d2;
}

float2 opSubstract(float2 d1, float2 d2){
  return (-d1.x < d2.x) ? d2 : float2(-d1.x, d1.y);//max(-d1, d2);
}

float opSubstract(float d1, float d2){
  return max(-d1, d2);
}

float2 opIntersect(float2 d1, float2 d2){
  return (d1.x < d2.x) ? d2 : d1;//max(d1, d2);
}

float opIntersect(float d1, float d2){
  return max(d1, d2);
}

float2 opMorph(float2 d1, float2 d2, float offset){
  return lerp(d1, d2, offset);
}

float opMorph(float d1, float d2, float offset){
  return lerp(d1, d2, offset);
}

float2 opSmoothUnite(float2 d1, float2 d2, float k){
  return smin(d1, d2, k);
}

float opSmoothUnite(float d1, float d2, float k){
  return smin(d1, d2, k);
}

float2 opSmoothSubstract(float2 d1, float2 d2, float k){
  return smax(float2(-d1.x, d1.y), d2, k);
}

float opSmoothSubstract(float d1, float d2, float k){
  return smax(-d1.x, d2, k);
}

float2 opSmoothIntersect(float2 d1, float2 d2, float k){
  return smax(d1, d2, k);
}

float opSmoothIntersect(float d1, float d2, float k){
  return smax(d1, d2, k);
}

float opRound(float sdf, float r )
{
  return sdf - r;
}

float2 opRound(float2 sdf, float r)
{
  return float2(sdf.x - r, sdf.y);
}

float opOnion(float sdf, float r )
{
  return abs(sdf) - r;
}

float2 opOnion(float2 sdf, float r)
{
  return float2(abs(sdf.x) - r, sdf.y);
}