using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistent
{
    public class Lighting : MonoBehaviour
    {
        private static string username;

        public static void SetUsername(string username)
        {
            Lighting.username = username;
        }

        public static void Flash()
        {

        }
    }
}