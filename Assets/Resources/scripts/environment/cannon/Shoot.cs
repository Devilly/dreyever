using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;

namespace Environment.Cannon
{
    public class Shoot : MonoBehaviour
    {
        public float interval;
        public GameObject cannonball;

        void Start()
        {
            StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                GameObject newCannonball = Instantiate(cannonball, transform.position, transform.rotation);
                BallBehaviour behaviour = newCannonball.GetComponent<BallBehaviour>();
                behaviour.SetAngle(transform.rotation.eulerAngles.z);
            }
        }
    }
}