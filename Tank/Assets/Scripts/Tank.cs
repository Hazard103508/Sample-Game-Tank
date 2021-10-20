using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rosso
{
    public class Tank : MonoBehaviour
    {
        #region Objects
        [SerializeField] private AudioSource audioFire;
        [SerializeField] private GameObject tower;
        [SerializeField] private GameObject cannon;
        [SerializeField] private Missile Misile;

        private Rigidbody rb;

        /// <summary>
        /// Cantidad de balas que tiene el tanque
        /// </summary>
        public int Bullets { get; protected set; }
        /// <summary>
        /// Porcentage de enfriamiento luego de un disparo
        /// </summary>
        public float CoolingProgress { get; private set; }
        #endregion

        #region Properties
        public Vector2 Direction { get; set; }
        public float TowerAngle { get; set; }
        public float Movement_Speed { get; protected set; }
        public float Rotation_Speed { get; protected set; }
        public float Power { get; set; }
        public TankState State { get; private set; }
        #endregion

        #region Unity Methods
        protected void Start()
        {
            this.rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (this.State == TankState.Normal)
            {
                this.transform.Translate(Vector3.forward * this.Movement_Speed * this.Direction.y * Time.deltaTime);
                this.transform.Rotate(Vector3.up * this.Rotation_Speed * this.Direction.x * Time.deltaTime);

                float angleRotation = Math.Abs(this.tower.transform.localRotation.eulerAngles.y - this.TowerAngle);
                float rotationDelay = angleRotation / 360f;
                this.tower.transform.localRotation = Quaternion.Euler(Vector3.up * Mathf.MoveTowardsAngle(this.tower.transform.localRotation.eulerAngles.y, this.TowerAngle, 360f * Time.deltaTime));

                Misile.transform.position = cannon.transform.position;
            }

            if (CoolingProgress > 0)
                CoolingProgress -= (Time.deltaTime / 3);
            else if (CoolingProgress < 0)
                CoolingProgress = 0;
        }
        #endregion

        #region Methods
        public void Shoot_Missile()
        {
            if (Bullets > 0 && CoolingProgress == 0)
                StartCoroutine(Shoot_Missile_Co());
        }
        public IEnumerator Shoot_Missile_Co()
        {
            this.State = TankState.Shooting;
            Bullets--;
            CoolingProgress = 1f;

            float towerAngle = Mathf.Round(tower.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Sin(towerAngle), 0, Mathf.Cos(towerAngle)).normalized;

            audioFire.Play();
            Misile.transform.rotation = tower.transform.rotation;
            Misile.transform.position = cannon.transform.position;
            Misile.Impulse(this.Power, direction);

            this.rb.AddForce(-direction * this.Power * 1000, ForceMode.Force);
            yield return new WaitForSeconds(1f);
            this.State = TankState.Normal;
        }
        #endregion

        #region Structures
        public enum TankState
        {
            Normal,
            Shooting,
        }
        #endregion
    }
}

