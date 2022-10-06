using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

[DefaultExecutionOrder(10)]
public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Ammo _ammo;
    [Header("Settings")]
    [SerializeField] private Transform _stand;
    [SerializeField] private BoxCollider _box;
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rigi;

    private bool _isUsed;
    private bool _drop;
    protected bool _shootDelay;
    private Transform _currentStand;

    private float timer;

    public Weapon Weapon => _weapon;
    public Ammo Ammo => _ammo;
    private void Start()
    {
        if (transform.root.CompareTag("Player"))
        {
            _isUsed = true;
            _anim.enabled = false;
            _rigi.isKinematic = true;
        }

        if (!_isUsed)
        {
            _weapon.AmmoLeft = Random.Range(_weapon.AmmoCageAmount / 2,
                _weapon.AmmoCageAmount);

            float distance = 0;
            RaycastHit hit;
            Physics.Raycast(new Ray(_weapon.Handler.position, -transform.up), out hit);

            distance = 1f - hit.distance;

            transform.position += new Vector3(0, distance, 0);

            _currentStand = Instantiate(_stand == null ? Constants.WeaponStand : _stand,
                hit.point, Quaternion.identity, transform);

            _rigi.isKinematic = true;
        }

        _anim.SetFloat("Speed", Random.Range(0.8f, 1.2f));
    }
    private void Update()
    {
        if (_drop)
        {
            RaycastHit hit;
            Physics.Raycast(new Ray(_weapon.Handler.position, -transform.up), out hit);

            if(hit.distance <= 1)
            {
                _currentStand = Instantiate(_stand == null ? Constants.WeaponStand : _stand,
                hit.point, Quaternion.identity, transform);

                _rigi.isKinematic = true;
                _anim.enabled = true;
                _drop = false;
            }
        }

        if (_shootDelay)
        {
            timer += Time.deltaTime;
            if(timer >= _weapon.ShootDelay)
            {
                _shootDelay = false;
                timer = 0;
            }
        }
    }
    public virtual void Shoot()
    {
        if (Weapon.AmmoLeft == 0) return;
        if (_shootDelay) return;

        BulletBehaviour bb = Instantiate(Ammo._bulletPrefab,
             Weapon.ShootPoint.position, Quaternion.identity, Constants.BulletPack);

        bb.SetAmmo = Ammo;
        bb.transform.rotation = Constants.Camera.transform.rotation;

        Weapon.AmmoLeft--;
        _shootDelay = true;
    }

    public virtual void Drop(Vector3 dir)
    {
        transform.SetParent(null);
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.Euler(0, Constants.Player.eulerAngles.y, 0);

        _rigi.isKinematic = false;
        _rigi.AddForce(dir * 5, ForceMode.Impulse);

        _drop = true;
    }

    public virtual WeaponBehaviour Claim(Transform parent)
    {
        Destroy(_currentStand.gameObject);
        _anim.StopPlayback();
        _anim.enabled = false;
        _isUsed = true;

        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one;

        _weapon.Handler.localPosition = Items.Weapon.HandlerPos;
        _weapon.Handler.localRotation = Quaternion.Euler(0, 0, 0);

        return this;
    }
}
