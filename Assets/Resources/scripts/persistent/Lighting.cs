using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Persistent
{
    public class Lighting : MonoBehaviour
    {
        private static string username = null;
        private static string ipaddress = null;

        public static void SetUsername(string username)
        {
            Lighting.username = username;
        }

        public static void SetHueIpaddress(string ipaddress)
        {
            Lighting.ipaddress = ipaddress;
        }

        private static void LightsOnFast()
        {
            if (username == null || ipaddress == null) return;

            using (UnityWebRequest request = new UnityWebRequest("http://" + ipaddress + "/api/" + username + "/groups/0/action"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes("{\"on\": true, \"transitiontime\": 0}"));
                request.method = UnityWebRequest.kHttpVerbPUT;

                request.SendWebRequest();
            }
        }

        private static void LightsOffFast()
        {
            if (username == null || ipaddress == null) return;

            using (UnityWebRequest request = new UnityWebRequest("http://" + ipaddress + "/api/" + username + "/groups/0/action"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes("{\"on\": false, \"transitiontime\": 0}"));
                request.method = UnityWebRequest.kHttpVerbPUT;

                request.SendWebRequest();
            }
        }
    }
}