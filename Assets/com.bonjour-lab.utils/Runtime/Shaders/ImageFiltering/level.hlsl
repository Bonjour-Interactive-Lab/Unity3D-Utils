
float3 _MinInput;
float3 _MaxInput;
float3 _MinOutput;
float3 _MaxOutput;
float3 _Gamma;

#define gammaCorrection(color, gamma)											pow(color, float3(1.0, 1.0, 1.0) / gamma)
#define levelsControlInputRange(color, minInput, maxInput)						min(max(color - minInput, float3(0.0, 0.0, 0.0)) / (maxInput - minInput), float3(1.0, 1.0, 1.0))
#define levelsControlInput(color, minInput, gamma, maxInput)					gammaCorrection(levelsControlInputRange(color, minInput, maxInput), gamma)
#define levelsControlOutputRange(color, minOutput, maxOutput) 					lerp(minOutput, maxOutput, color)
#define levelsControl(color, minInput, gamma, maxInput, minOutput, maxOutput) 	levelsControlOutputRange(levelsControlInput(color, minInput, gamma, maxInput), minOutput, maxOutput)
