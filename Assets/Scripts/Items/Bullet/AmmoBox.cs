using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class AmmoBox : Ammo
    {
        [Space]
        public int AmmoAmount;
        public int MaxAmmoInBox;
    }
}
