using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;
using Scriptables.Util;
using Dreyever;

namespace Environment.Cannon
{
    public class BallBehaviour : MonoBehaviour
    {
        public Instantiater instantiater;

        private float angle = 0;
        public float minDistancePerSecond = 10;
        public float maxDistancePerSecond = 20;
        private float xPerSecond = 0;
        private float yPerSecond = 0;

        public void SetAngle(float angle)
        {
            this.angle = (angle - 90) * Mathf.Deg2Rad;
            float distancePerSecond = Random.Range(minDistancePerSecond, maxDistancePerSecond);
            this.yPerSecond = Mathf.Sin(this.angle) * distancePerSecond;
            this.xPerSecond = Mathf.Cos(this.angle) * distancePerSecond;
        }

        void FixedUpdate()
        {
            transform.position += new Vector3(xPerSecond * Time.fixedDeltaTime, yPerSecond * Time.fixedDeltaTime, 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.name == "Hitbox")
            {
                collider.transform.parent.GetComponentInChildren<Controls>().Influence(new Influence().Die(true));
            }
            
            Destroy(gameObject);
        }
    }
}