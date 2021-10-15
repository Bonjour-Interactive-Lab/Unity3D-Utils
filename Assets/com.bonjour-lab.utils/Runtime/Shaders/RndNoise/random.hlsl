#define RNDSEED 43758.5453123

//----------------------------------------
//HASH Functions
//----------------------------------------
float random(float x, float seed){
    return frac(sin(x) * seed);
}

float random2(float2 xy, float seed){
    return frac(sin(dot(xy, float2(12.9898, 4.1414))) * seed);
}

float random3(float3 xyz, float seed){
    return frac(sin(dot(xyz, float3(12.9898, 4.1414, 52.12486))) * seed);
}

//----------------------------------------
//HASH w/seed
//----------------------------------------
float random(float x){
    return random(x, RNDSEED);
}

float random2(float2 xy){
    return random2(xy, RNDSEED);
}

float random3(float3 xyz){
    return random3(xyz, RNDSEED);
}