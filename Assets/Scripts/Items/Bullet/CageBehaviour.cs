using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CageBehaviour : MonoBehaviour
{
    [SerializeField] private AmmoBox _ammoBox;

    private void Start()
    {
        _ammoBox.AmmoAmount = Random.Range(_ammoBox.MaxAmmoInBox / 4, _ammoBox.MaxAmmoInBox);
    }
    public int AmmoInBox { get { return _ammoBox.AmmoAmount; } }

    public int Claim(AmmoType type)
    {
        if(_ammoBox._typeAmmo == type)
        {
            Destroy(this.gameObject);
            return AmmoInBox;
        }
        return 0;
    }
}
