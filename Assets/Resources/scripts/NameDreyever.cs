using UnityEngine;
using UnityEngine.UI;

namespace Garage
{
    public class NameDreyever : MonoBehaviour
    {
        private Text text;

        void Start()
        {
            text = GetComponent<Text>();

            SetName();
        }

        void Update()
        {
            SetName();
        }

        void SetName()
        {
            text.text = Persistent.Environment.instance.GetCurrentDreyever().displayName;
        }
    }
}