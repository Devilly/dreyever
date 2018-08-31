using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Garage
{
    public class DreyeverBehaviour : MonoBehaviour
    {
        
        void Start()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener(data => Entered((PointerEventData) data));
            trigger.triggers.Add(entry);
        }

        public void Entered(PointerEventData data)
        {
            Persistent.Environment.instance.SetCurrentDreyever(GetComponent<Image>().sprite);

            Persistent.Vibrator.Vibrate(10);
        }
    }
}