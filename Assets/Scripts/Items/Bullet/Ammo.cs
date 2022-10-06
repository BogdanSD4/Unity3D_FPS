using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class Ammo
    {
        public AmmoType _typeAmmo;
        public float _bulletSpeed;
        public float _bulletDamage;
        public float _lifeDuration = 5;
        public BulletBehaviour _bulletPrefab;
    }

    public enum AmmoType
    {
        Pistol,
        Automatic,
        Relif,
    }
}
