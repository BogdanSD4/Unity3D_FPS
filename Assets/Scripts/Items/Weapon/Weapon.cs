using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class Weapon
    {
        public Transform ShootPoint;
        public Transform Handler;

        public int AmmoCageAmount;
        public int AmmoLeft;
        public int AmmoMax;
        public int AmmoStuck;

        public float ShootDelay;

        public static Vector3 HandlerPos = new Vector3(0, 0.05f, 0.5f);

        public void Reload()
        {
            if (AmmoLeft == AmmoStuck | AmmoCageAmount == 0) return;

            if (AmmoCageAmount < AmmoStuck)
            {
                AmmoLeft += AmmoCageAmount;
                AmmoCageAmount = 0;
            }
            else
            {
                AmmoCageAmount -= AmmoStuck - AmmoLeft;
                AmmoLeft = AmmoStuck;
            }
        }
    }
}
