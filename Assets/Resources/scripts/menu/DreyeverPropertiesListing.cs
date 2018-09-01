using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreyeverPropertiesListing : MonoBehaviour {

    private Text textObject;

    void Start()
    {
        textObject = GetComponent<Text>();

        SetText();
    }

	void Update () {
        SetText();
    }

    private void SetText()
    {
        List<string> properties = new List<string>();
        Scriptables.Dreyevers.Dreyever currentDreyever = Persistent.Environment.instance.GetCurrentDreyever();

        if(currentDreyever.canAirJump)
        {
            properties.Add("Airjump");
        }

        textObject.text = string.Join(System.Environment.NewLine, properties.ToArray());
    }
}
