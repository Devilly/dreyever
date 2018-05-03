using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Cannon
{
    public class Shoot : MonoBehaviour
    {

        public GameObject cannonball;

        void Start()
        {
            StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (true)
            {
                yield return new WaitForSeconds(.5f);

                GameObject newCannonball = Instantiate(cannonball, transform.position, transform.rotation);
                BallBehaviour behaviour = newCannonball.GetComponent<BallBehaviour>();
                behaviour.SetAngle(transform.rotation.eulerAngles.z);
            }
        }
    }
}