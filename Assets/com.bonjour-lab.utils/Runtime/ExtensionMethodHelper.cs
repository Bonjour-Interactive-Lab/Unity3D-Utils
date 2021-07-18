using UnityEngine;
using System;

namespace Bonjour.Utils{
    //The following helper provide a MonoBehaviro instance on the scene which can be used for calling MonoBehavior Methods such as Co routines
    public class ExtensionMethodHelper : MonoBehaviour
    {
        private static ExtensionMethodHelper _Instance;

        public static ExtensionMethodHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameObject("ExtensionMethodHelper").AddComponent<ExtensionMethodHelper>();
                }

                return _Instance;
            }
        }
    }
}