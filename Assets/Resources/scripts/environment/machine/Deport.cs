using Cinemachine;
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
                StartCoroutine(Displace(collider));
            }
        }

        private IEnumerator Displace(Collider2D collider)
        {
            collider.SendMessage("Influence", new Influence().StopMoving(true));
            symbol.sprite = active;

            yield return new WaitForSeconds(2);

            VirtualCameraManagement vCamManagement = Camera.main.GetComponent<VirtualCameraManagement>();
            GameObject currentVirtualCameraLeft = vCamManagement.virtualCameraLeft;
            GameObject newVirtualCameraLeft = Instantiate(vCamManagement.virtualCameraLeft);
            newVirtualCameraLeft.SetActive(false);
            currentVirtualCameraLeft.GetComponent<CinemachineVirtualCamera>().Follow = null;

            collider.SendMessage("Influence", new Influence()
                .Place(destinations[new System.Random().Next(0, destinations.Length)].transform.position));

            newVirtualCameraLeft.SetActive(true);
            currentVirtualCameraLeft.SetActive(false);
            vCamManagement.virtualCameraLeft = newVirtualCameraLeft;

            yield return new WaitForSeconds(3);

            collider.SendMessage("Influence", new Influence().StartMoving(true));
            symbol.sprite = inactive;
        }
    }
}