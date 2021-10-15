#define PI 3.1415926538
#define TWOPI 6.2831853076

/**
This function will return a 2d rotation matrix form the desired rectangle
Learn more about rotation matrix : https://en.wikipedia.org/wiki/Rotation_matrix
*/
float2x2 rotate2d(float angle){
  return float2x2(  cos(angle), -sin(angle),
                    sin(angle),  cos(angle));
}

/**
this function will rotate the fragment coordinate at a desired angle from the center
*/
float2 rotate(float2 st, float angle){
    float2x2 mat = rotate2d(angle);
     st = mul(mat, st);

    return st;
}

/**
This function will return a 2d scaling matrix form the desired rectangle
Learn more about scaling matrix : https://en.wikipedia.org/wiki/Scaling_(geometry)
*/
float2x2 scale2d(float2 scale){
  return float2x2(  scale.x, 0.0,
                    0.0    , scale.y);
}

/**
this function will scale the fragment coordinate at a desired size from the center
*/
float2 scale(float2 st, float2 scale){
    float2x2 mat = scale2d(scale);
     st = mul(mat, st);

    return st;
}
