using UnityEngine;
using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine.Events;
using Bonjour;
using Bonjour.Utils;

namespace Bonjour.Time{
    public struct TimerData{
        public float time;
        public float totalTime;
        public float normalizedTime;
        public string name;
    }
    
    public class Timer{
        public class TimerEvent : UnityEvent<TimerData> { }

        private TimerData timerdata;
        public TimerEvent OnTimerStart      = new TimerEvent();
        public TimerEvent OnTimerEnd        = new TimerEvent();
        public TimerEvent OnTimerUpdated    = new TimerEvent();
        

        private bool start      = false;
        private bool eventflag  = false;

        public Timer(string name, float totalTime){
            timerdata               = new TimerData();
            timerdata.name          = name;
            timerdata.totalTime     = totalTime;
        }

        public IEnumerator Countdown()
        {
            start       = true;
            // Debug.Log("Timer is Started");
            timerdata.normalizedTime  = 0;
            timerdata.time            = 0;
            if (eventflag) OnTimerStart.Invoke(timerdata);

            while(timerdata.normalizedTime <= 1f)
            {   
                timerdata.normalizedTime  += UnityEngine.Time.deltaTime / timerdata.totalTime;
                timerdata.time            += UnityEngine.Time.deltaTime;
                if(eventflag) OnTimerUpdated.Invoke(timerdata);
                // Debug.Log(timerdata.normalizedTime+" :: "+timerdata.time);
                yield return null;
            }

            //end timer
            start       = false;
            timerdata.normalizedTime  = 1;
            timerdata.time            = timerdata.totalTime;
            if (eventflag) OnTimerEnd.Invoke(timerdata);
            // Debug.Log("Timer is Finished");
        }

        public void StopTimer(){
            if(start == true){
                eventflag = false;
                ExtensionMethodHelper.Instance.StopCoroutine(Countdown());
                start                       = false;
                timerdata.normalizedTime    = 1;
                timerdata.time              = timerdata.totalTime;
                OnTimerEnd.Invoke(timerdata);
            }
        }

        public void ResetTimer(){
            StopTimer();
            StartTimer();
        }

        public void StartTimer() {
            if(start != true){
                eventflag = true;
                ExtensionMethodHelper.Instance.StartCoroutine(Countdown());
            }
        }

        public float GetNormalizedTime(){
            return timerdata.normalizedTime;
        }

        public float GetTime(){
            return timerdata.time;
        }

        public float GetRemainingTime(){
            return timerdata.totalTime - timerdata.time;
        }

        public float GetNormalizedRemainingTime(){
            return 1f - timerdata.normalizedTime;
        }

        public void SetTimeout(float timeout){
            timerdata.totalTime = timeout;
        }
    }
}