using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Persistent
{
    public class Vibrator : MonoBehaviour
    {
        void Awake()
        {
            // Following can never be true and therefore the body will never be executed.
            // This is the same as if(false) but without the compilation warning.
            if (Application.isPlaying && !Application.isPlaying)
            {
                Handheld.Vibrate();
            }
        }

        // VibrationEffect is only added in API level 26 of Android. Once the standard can be used like this:
        // AndroidJavaClass vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
        // AndroidJavaObject vibrationDefinition = vibrationEffect.CallStatic<AndroidJavaObject>("createOneShot", 50L, 100);
        public static void Vibrate(long milliseconds)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            vibrator.Call("vibrate", milliseconds);
#endif
        }
    }
}