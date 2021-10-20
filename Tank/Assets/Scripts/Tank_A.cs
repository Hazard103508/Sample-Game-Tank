using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosso
{
    public class Tank_A : Tank
    {
        new void Start()
        {
            base.Start();

            base.Bullets = 20;
            base.Power = 20f;
            base.Movement_Speed = 5f;
            base.Rotation_Speed = 90f;
        }
    }
}