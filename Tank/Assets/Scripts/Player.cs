using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosso
{
    public class Player : MonoBehaviour
    {
        private Tank tank;

        #region Properties
        /// <summary>
        /// Cantidad de balas que tiene el tanque
        /// </summary>
        public int Bullets { get => tank.Bullets; }
        /// <summary>
        /// Porcentage de enfriamiento luego de un disparo
        /// </summary>
        public float CoolingProgress { get => tank.CoolingProgress; }
        #endregion

        #region Unity Methods
        void Start()
        {
            tank = GetComponent<Tank>();
        }

        void Update()
        {
            tank.Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            tank.TowerAngle = this.Get_TowerAngle();

            if (Input.GetMouseButtonDown(0))
                tank.Shoot_Missile();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene el angulo de rotacion de la torre del tanque
        /// </summary>
        /// <returns></returns>
        private float Get_TowerAngle()
        {
            Vector2 tankScreenPos = Camera.main.WorldToViewportPoint(tank.gameObject.transform.position);
            Vector2 mouseRelativePos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition) - tankScreenPos;
            return Vector2.Angle(Vector2.up, mouseRelativePos) * (mouseRelativePos.x < 0 ? -1 : 1);
        }
        #endregion
    }
}
