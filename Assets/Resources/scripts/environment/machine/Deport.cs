﻿using Cinemachine;
using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    public class Deport : MonoBehaviour
    {

        public SpriteRenderer symbol;

        public Sprite inactive;
        public Sprite active;

        public GameObject[] destinations;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "dreyever")
            {
                StartCoroutine(TurnAround(collider.transform));
            }
        }

        private IEnumerator TurnAround(Transform dreyever)
        {
            State state = dreyever.GetComponent<State>();

            state.StopMoving();
            symbol.sprite = active;

            yield return new WaitForSeconds(2);

            VirtualCameraManagement vCamManagement = Camera.main.GetComponent<VirtualCameraManagement>();
            GameObject currentVirtualCameraLeft = vCamManagement.virtualCameraLeft;
            GameObject newVirtualCameraLeft = Instantiate(vCamManagement.virtualCameraLeft);
            newVirtualCameraLeft.SetActive(false);
            currentVirtualCameraLeft.GetComponent<CinemachineVirtualCamera>().Follow = null;

            dreyever.position = destinations[new System.Random().Next(0, destinations.Length)].transform.position;

            newVirtualCameraLeft.SetActive(true);
            currentVirtualCameraLeft.SetActive(false);
            vCamManagement.virtualCameraLeft = newVirtualCameraLeft;

            yield return new WaitForSeconds(3);

            state.StartMoving();
            symbol.sprite = inactive;
        }
    }
}