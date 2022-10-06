using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBehaviour : MonoBehaviour
{
    [SerializeField] private float _health;

    public virtual void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(this.gameObject);
    }
}
