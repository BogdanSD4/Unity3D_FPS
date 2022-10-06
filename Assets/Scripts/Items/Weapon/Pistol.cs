using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    class Pistol : WeaponBehaviour
    {
        public override void Shoot()
        {
            base.Shoot();  
        }

        public override void Drop(Vector3 dir)
        {
            base.Drop(dir);
        }

        public override WeaponBehaviour Claim(Transform parent)
        {
            return base.Claim(parent);
        }
    }
}
