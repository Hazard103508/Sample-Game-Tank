using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rosso
{
    public class Missile : MonoBehaviour
    {
        private Rigidbody rb;

        void Start()
        {
            this.rb = GetComponent<Rigidbody>();
        }

        public void Impulse(float force, Vector3 direction)
        {
            this.rb.velocity = Vector3.zero;
            this.rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}