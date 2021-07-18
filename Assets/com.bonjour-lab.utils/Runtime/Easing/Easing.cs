using System.Collections;
using UnityEngine;

namespace Bonjour.Maths

{
    public static class Easing{

        // ==================================================
        // Easing Equations by Robert Penner : http://robertpenner.com/easing/
        // http://www.timotheegroleau.com/Flash/experiments/easing_function_generator.htm
        // Based on ActionScript implementation by gizma : http://gizma.com/easing/
        // Processing implementation by Bonjour, Interactive Lab
        // soit time le temps actuelle ou valeur x à l'instant t;
        // soit start la position x de départ;
        // soit end l'increment de s donnant la position d'arrivee a = s + e;
        // soit duration la durée de l'opération
        // check this link for cheat sheet : https://easings.net/fr
        // ==================================================

        public static float ease(float time, EEasing TYPE){
            switch(TYPE){
                case EEasing.LINEAR :
                   return linear(time);
                case EEasing.INQUAD :
                    return inQuad(time);
                case EEasing.OUTQUAD :
                    return outQuad(time);
                case EEasing.INOUTQUAD :
                    return inoutQuad(time);
                case EEasing.INQUARTIC :
                    return inQuartic(time);
                case EEasing.OUTQUARTIC :
                    return inoutQuartic(time);
                case EEasing.INOUTQUARTIC :
                    return inoutQuartic(time);
                case EEasing.INQUINTIC :
                    return inQuintic(time);
                case EEasing.OUTQUINTIC :
                    return outQuintic(time);
                case EEasing.INOUTQUINTIC :
                    return inoutQuintic(time);
                case EEasing.INSIN :
                    return inSin(time);
                case EEasing.OUTSIN :
                    return outSin(time);
                case EEasing.INOUTSIN :
                    return inoutSin(time);
                case EEasing.INEXP :
                    return inExp(time);
                case EEasing.OUTEXP :
                    return outExp(time);
                case EEasing.INOUTEXP :
                    return inoutExp(time);
                case EEasing.INCIRC :
                    return inCirc(time);
                case EEasing.OUTCIRC :
                    return outExp(time);
                case EEasing.INOUTCIRC :
                    return inoutExp(time);
                default:
                   return linear(time);
            }
        }

        //offset time
        public static float offset(float time, float offset){
            float normTime = time/offset;
            float start = (normTime > 1.0f) ? 0.0f : normTime;
            float end   = (normTime > 1.0f) ? 1.0f - ((time - offset) / (1.0f - offset)) : 0.0f;
            return Mathf.Clamp(start + end, 0.0f, 1.0f);
        }


        // Linear
        public static float linear(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
            inc = inc * time / duration + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    // Quadratic
        public static float inQuad(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = inc * time * time + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outQuad(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = -inc * time * (time - 2) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutQuad(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
                inc = inc / 2 * time * time + start;
            }
            else
            {
                time--;
                inc = -inc / 2 * (time * (time - 2) - 1) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Cubic
        public static float inCubic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = inc * Mathf.Pow(time, 3) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outCubic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            time--;
            float inc = end - start;
            inc = inc * (Mathf.Pow(time, 3) + 1) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutCubic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
                inc = inc / 2 * Mathf.Pow(time, 3) + start;
            }
            else
            {
                time -= 2;
                inc = inc / 2 * (Mathf.Pow(time, 3) + 2) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Quatric
        public static float inQuartic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = inc * Mathf.Pow(time, 4) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outQuartic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            time--;
            float inc = end - start;
            inc = -inc * (Mathf.Pow(time, 4) - 1) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutQuartic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
                inc = inc / 2 * Mathf.Pow(time, 4) + start;
            }
            else
            {
                time -= 2;
                inc = -inc / 2 * (Mathf.Pow(time, 4) - 2) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Quintic
        public static float inQuintic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = inc * Mathf.Pow(time, 5) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outQuintic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            time--;
            float inc = end - start;
            inc = inc * (Mathf.Pow(time, 5) + 1) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutQuintic(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
                inc = inc / 2 * Mathf.Pow(time, 5) + start;
            }
            else
            {
                time -= 2;
                inc = inc / 2 * (Mathf.Pow(time, 5) + 2) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Sinusoïdal
        public static float inSin(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
            inc = -inc * Mathf.Cos(time / duration * (Mathf.PI / 2.0f)) + inc + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outSin(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
            inc = inc * Mathf.Sin(time / duration * (Mathf.PI / 2.0f)) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutSin(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
            inc = -inc / 2 * (Mathf.Cos(Mathf.PI * time / duration) - 1) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Exponential
        public static float inExp(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
        //return inc * pow(2, 10 * (time/duration - 1)) + start;
            if (time <= 0)
            {
                inc = start;
            }
            else
            {
                inc = inc * Mathf.Pow(2, 10 * (time / duration - 1)) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outExp(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            float inc = end - start;
            if (time >= 1.0)
            {
                inc = 1.0f;
            }
            else
            {
                inc = inc * (-Mathf.Pow(2, -10 * (time / duration)) + 1) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutExp(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
                inc = inc / 2 * Mathf.Pow(2, 10 * (time - 1)) + start;
            }
            else
            {
                time--;
                inc = inc / 2 * (-Mathf.Pow(2, -10 * time) + 2) + start;
            }
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    //Circular
        public static float inCirc(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            float inc = end - start;
            inc = -inc * (Mathf.Sqrt(1 - time * time) - 1) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float outCirc(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration;
            time--;
            float inc = end - start;
            inc =  inc * Mathf.Sqrt(1 - time * time) + start;
            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

        public static float inoutCirc(float time)
        {
            float start = 0.0f;
            float end = 1.0f;
            float duration = 1.0f;
            time /= duration / 2;
            float inc = end - start;
            if (time < 1)
            {
               inc = -inc / 2 * (Mathf.Sqrt(1 - time * time) - 1) + start;
            }
            else
            {
                time -= 2;
                inc = inc / 2 * (Mathf.Sqrt(1 - time * time) + 1) + start;
            }

            return Mathf.Clamp(inc, 0.0f, 1.0f);
        }

    }
}