//based on https://www.iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm

float sdSegment(float2 uv, float2 a, float2 b )
{
    float2 pa   = uv-a, ba = b-a;
    float h     = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );

    return length( pa - ba*h );
}

float sdCircle(float2 uv){
    return length(uv);
}

float sdCircle(float2 uv, float r )
{
    return length(uv) - r;
}

float sdRect(float2 uv, float2 bounds){
    float2 sdf = abs(uv) - bounds;
    return length(max(sdf,0.0)) + min(max(sdf.x,sdf.y), 0.0);
}

float sdCross(float2 uv, float2 boundsX, float2 boundsY){
    return min( sdRect(uv, boundsX),
                sdRect(uv, boundsY));
}

float ndot(float2 a, float2 b ) { return a.x*b.x - a.y*b.y; }

float sdDiamond(float2 uv, float2 bounds) 
{
    float2 q    = abs(uv);
    float h     = clamp((-2.0 * ndot(q,bounds) + ndot(bounds,bounds)) / dot(bounds,bounds), -1.0, 1.0);
    float d     = length( q - 0.5 * bounds * float2(1.0-h, 1.0+h));

    return d * sign( q.x*bounds.y + q.y*bounds.x - bounds.x*bounds.y);
}

float sdPolarShape(float2 uv, int nbEdges, float size){
    
  //get angle of the pixel coordinate
  //atant will return a value between -PI and PI
  float angle   = atan2(uv.y, uv.x) + 3.14159265359;
  float radius  = 6.2831853076 / float(nbEdges);

  //modulate the distance to define the shape
  float d = cos(floor(0.5 + angle/radius) * radius - angle) * length(uv);

  return d + size;
}

float sdTriangle(float2 uv, float2 p0, float2 p1, float2 p2 )
{
    float2 e0 = p1-p0, e1 = p2-p1, e2 = p0-p2;
    float2 v0 = uv -p0, v1 = uv -p1, v2 = uv -p2;

    float2 pq0 = v0 - e0 * clamp( dot(v0,e0)/dot(e0,e0), 0.0, 1.0 );
    float2 pq1 = v1 - e1 * clamp( dot(v1,e1)/dot(e1,e1), 0.0, 1.0 );
    float2 pq2 = v2 - e2 * clamp( dot(v2,e2)/dot(e2,e2), 0.0, 1.0 );
    float s = sign( e0.x*e2.y - e0.y*e2.x );
    float2 d = min(min( float2(dot(pq0,pq0), s*(v0.x*e0.y-v0.y*e0.x)),
                        float2(dot(pq1,pq1), s*(v1.x*e1.y-v1.y*e1.x))),
                        float2(dot(pq2,pq2), s*(v2.x*e2.y-v2.y*e2.x)));

    return -sqrt(d.x)*sign(d.y);
}