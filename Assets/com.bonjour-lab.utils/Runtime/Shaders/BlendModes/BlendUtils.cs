using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Bonjour
{
    /*
    #pragma BLEND_DARKEN BLEND_MULTIPLY BLEND_COLORBURN BLEND_LINEARBURN 
    #pragma BLEND_LIGHTEN BLEND_SCREEN BLEND_COLORDODGE BLEND_LINEARDODGE
    #pragma BLEND_OVERLAY BLEND_SOFTLIGHT BLEND_HARDLIGHT BLEND_VIVIDLIGHT BLEND_LINEARLIGHT BLEND_PINLIGHT BLEND_HARDMIX
    #pragma BLEND_DIFFERENCE BLEND_EXCLUSION BLEND_SUBSTRACT BLEND_DIVIDE 
    #pragma BLEND_HUE BLEND_SATURATION BLEND_COLOR BLEND_LUMINOSITY BLEND_DARKERCOLOR BLEND_LIGHTERCOLOR
    */
    public class BlendUtils{
        public enum BLENDMODE{
            BLENDNONE          ,
            BLENDDARKEN        ,             
            BLENDMULTIPLY      ,
            BLENDCOLORBURN     ,
            BLENDLINEARBURN    ,
            BLENDADD           ,
            BLENDLIGHTEN       ,
            BLENDSCREEN        ,
            BLENDCOLORDODGE    ,
            BLENDLINEARDODGE   ,
            BLENDOVERLAY       ,
            BLENDSOFTLIGHT     ,
            BLENDHARDLIGHT     ,
            BLENDVIVIDLIGHT    ,
            BLENDLINEARLIGH    ,
            BLENDPINLIGHT      ,
            BLENDHARDMIX       ,
            BLENDDIFFERENCE    ,
            BLENDEXCLUSION     ,
            BLENDSUBSTRACT     ,
            BLENDDIVIDE        ,
            BLENDHUE           ,
            BLENDSATURATION    ,
            BLENDCOLOR         ,
            BLENDLUMINOSITY    ,
            BLENDDARKERCOLOR   ,
            BLENDLIGHTERCOLOR
        }

        public static List<BLENDMODE> GetListOfBlendMode(){
            List<BLENDMODE> blendmodes = Enum.GetValues(typeof(BLENDMODE))
                                            .Cast<BLENDMODE>()
                                            .ToList();
            return blendmodes;
        }

        public static void EnableBlend(BLENDMODE blend, Material mat){
            List<BLENDMODE> blendmodes = GetListOfBlendMode();
            foreach(BLENDMODE _blend in blendmodes){
                if(blend != _blend) mat.DisableKeyword(_blend.ToString());
            }

            mat.EnableKeyword(blend.ToString());
        }
    }
}
