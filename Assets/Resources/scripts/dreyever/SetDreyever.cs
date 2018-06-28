using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDreyever : MonoBehaviour
{
	void Awake () {
        GetComponent<SpriteRenderer>().sprite = Persistent.Environment.instance.GetCurrentDreyever().sprite;
	}
}
