using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonjour{
    public class DebugController : MonoBehaviour
    {
        [Tooltip("List of all the GameObject to display when debug is active")] public List<GameObject> objectsToShowAtDebug;
        [Tooltip("Key to active debug mode")]public KeyCode debugKey = KeyCode.D;

        [Tooltip("Is the debug mode is acvtive at start")] public bool isShownAtStart = false;

        private void Awake() {
            UpdateDebugObjectsState();
        }

        private void Update(){
            if (Input.GetKeyDown(debugKey))
            {
                isShownAtStart = !isShownAtStart;
                UpdateDebugObjectsState();
            }
        }

        protected void UpdateDebugObjectsState(){
            foreach(GameObject go in objectsToShowAtDebug){
                go.SetActive(isShownAtStart);
            }       
        }
    }
}