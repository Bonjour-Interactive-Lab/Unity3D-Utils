float3 modulo(float3 divident, float3 divisor){
    float3 positiveDivident = floor(divident % divisor) + divisor;
    return floor(positiveDivident % divisor);
}

/*
author: Inigo Quilez
description: returns 2D/3D value noise in the first channel and in the rest the derivatives. For more details read this nice article http://www.iquilezles.org/www/articles/gradientnoise/gradientnoise.htm
use: noised(<vec2|vec3> space)
Return value between -1 / 1
options:
  NOISED_QUINTIC_INTERPOLATION: Quintic interpolation on/off. Default is off.
license: |
  Copyright © 2017 Inigo Quilez
  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
float4 gnoiseTiled(float3 uvw, float3 period) {
    float3 p = floor(uvw);
    float3 w = frac(uvw);
    
    float3 u = w*w*w*(w*(w*6.0-15.0)+10.0);
    float3 du = 30.0*w*w*(w*(w-2.0)+1.0);

    float a = random3( modulo(p + float3(0.0, 0.0, 0.0), period ));
    float b = random3( modulo(p + float3(1.0, 0.0, 0.0), period ));
    float c = random3( modulo(p + float3(0.0, 1.0, 0.0), period ));
    float d = random3( modulo(p + float3(1.0, 1.0, 0.0), period ));
    float e = random3( modulo(p + float3(0.0, 0.0, 1.0), period ));
    float f = random3( modulo(p + float3(1.0, 0.0, 1.0), period ));
    float g = random3( modulo(p + float3(0.0, 1.0, 1.0), period ));
    float h = random3( modulo(p + float3(1.0, 1.0, 1.0), period ));

    float k0 =  a;
    float k1 =  b - a;
    float k2 =  c - a;
    float k3 =  e - a;
    float k4 =  a - b - c + d;
    float k5 =  a - c - e + g;
    float k6 =  a - b - e + f;
    float k7 = -a + b + c - d + e - f - g + h;

    return float4(    -1.0 + 2.0 * (k0 + k1*u.x + k2*u.y + k3*u.z + k4*u.x*u.y + k5*u.y*u.z + k6*u.z*u.x + k7*u.x*u.y*u.z), 
                    2.0* du * float3( k1 + k4*u.y + k6*u.z + k7*u.y*u.z,
                                      k2 + k5*u.z + k4*u.x + k7*u.z*u.x,
                                      k3 + k6*u.x + k5*u.y + k7*u.x*u.y ) );
}

float4 gnoiseFbmTiled(float3 uvw, float3 period){
    float G = exp2(-.85);
    float amp = 1.;
    float div = (period.x + period.y + period.z)/3.0;
    float4 noise;
    
    #define OCTAVES 6
    for (int i = 0; i < OCTAVES; ++i)
    {
        noise += amp * gnoiseTiled(uvw * period, period);
        period *= 2.;
        amp *= G;
    }

    noise /= div;
    
    return noise;
}

float worleyTiled(float3 uvw, float3 period, float seed){
    float minDist 	= 1.0;
    float3 iuv 		= floor(uvw);
    float3 fuv 		= frac(uvw);
    
    //kernel
    for(int y=-1; y<=1; y++){
     for(int x= -1; x<=1; x++){
        for(int z= -1; z<=1; z++){
            //get neighbors tile space
            float3 neighbor 	= float3(float(x), float(y), float(z));
            
            float3 tileneigh	= modulo(iuv + neighbor, period);
            // float3 tileneigh	= floor((iuv + neighbor) % period);
            
            //Get randomPoint for each neighbor
            float3 pt 	= random3(tileneigh, seed);
            
            //animation comes here 
            // point = 0.5 + 0.5*sin(u_time + 6.2831*point);
            
            //Get vector from pixel to point
            float3 diff = neighbor + pt - fuv;
            
            //get dist from point
            float dst = length(diff);
            
            //get only the min dist
            minDist = min(minDist, dst);
        }
     }
    }
    
    return clamp(minDist, 0.0, 1.0);
}

float worleyFbmTiled(float3 uvw, float3 period, float seed, float3 amplitude){
    return 		worleyTiled(uvw * period	 	, period		, seed) * amplitude.x +
        	 	worleyTiled(uvw * period * 2.   , period * 2.	, seed) * amplitude.y +
        	 	worleyTiled(uvw * period * 4.   , period * 4.	, seed) * amplitude.z;
}

float worleyFbmTiled(float3 uvw, float3 period){
    return 	worleyFbmTiled(uvw, period, 43758.5453123, float3(.625, .25, .125));
}