float stroke(float sdf, float edge, float thickness){
    float value =   step(edge - thickness * 0.5, sdf) * 
                    (1.0 - step(edge + thickness * 0.5, sdf));
    return value;
}

float stroke(float sdf, float edge, float thickness, float smoothness){
    float value =   smoothstep(edge - thickness * 0.5 - smoothness, edge - thickness * 0.5, sdf) * 
                    (1.0 - smoothstep(edge + thickness * 0.5, edge + thickness * 0.5 + smoothness, sdf));
    return value;
}

float fill(float sdf, float edge){
    return 1.0 - step(edge, sdf);
}

float fill(float sdf, float edge, float smoothness){
    return 1.0 - smoothstep(edge - smoothness * 0.5, edge + smoothness * 0.5, sdf);
}

float fill(float sdf, float edge, float smoothness, float smoothnessIn, float smoothnessOut){
    return 1.0 - smoothstep(edge - smoothness * smoothnessIn, edge + smoothness * smoothnessOut, sdf);
}