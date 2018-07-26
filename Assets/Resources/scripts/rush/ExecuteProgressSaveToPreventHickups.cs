using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rush
{
    public class ExecuteProgressSaveToPreventHickups : MonoBehaviour
    {
        void Start()
        {
            Persistent.Environment.instance.Save();
        }
    }
}