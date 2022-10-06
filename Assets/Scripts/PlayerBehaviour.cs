using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Rigidbody _rigi;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private CapsuleCollider _collider;

    public static WeaponBehaviour _currentWeapon;
    private bool _haveWeapon;

    private float _boost = 1f;
    private float _moveX;
    private float _moveY;

    RaycastHit hit;
    private void Start()
    {
        foreach (Transform i in _rightHand)
        {
            _currentWeapon = i.GetComponent<WeaponBehaviour>();

            AmmoUI();

            _haveWeapon = true;
        }
        if (!_haveWeapon) Constants.Interface.CloseAmmoUI();
    }
    private void FixedUpdate()
    {
        ControllerMove();
    }
    private void Update()
    {
        ControllerAction();
        PlayerSight();
    }

    private void ControllerMove()
    {
        if ((_moveX = Input.GetAxis("Horizontal")) != 0)
        {
            transform.Translate(_moveX * _speed * _boost * Time.deltaTime, 0, 0);
        }

        if ((_moveY = Input.GetAxis("Vertical")) != 0)
        {
            transform.Translate(0, 0, _moveY * _speed * _boost * Time.deltaTime);
        }
    }
    private void ControllerAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigi.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _boost = 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _boost = 1;
        }




        if (Input.GetMouseButton(0))
        {
            if (!_haveWeapon) return;
            
            _currentWeapon.Shoot(); 

            Constants.Interface.OpenAmmoUI
                        (_currentWeapon.Weapon.AmmoLeft, _currentWeapon.Weapon.AmmoCageAmount);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _haveWeapon)
        {
            _currentWeapon.Drop(Constants.VectorRotation(transform.eulerAngles.y));

            Constants.Interface.CloseAmmoUI();
            _currentWeapon = null;
            _haveWeapon = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _currentWeapon.Weapon.Reload();

            AmmoUI();
        }
    }

    private void PlayerSight()
    {
        Vector3 CameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Physics.Raycast(Constants.Camera.ScreenPointToRay(CameraCenter), out hit);

        if (hit.distance <= 2 && hit.collider != null)
        {
            UseButton(hit.transform);
        }
        else
        {
            if (Constants.Interface.PlayerMessage != null)
                Constants.Interface.PlayerMessage = null;
        }
    }

    private void UseButton(Transform hit)
    {
        if (hit.transform.CompareTag("Weapon"))
        {
            if (Constants.Interface.PlayerMessage != "Press E")
                Constants.Interface.PlayerMessage = "Press E";

            if (Input.GetKeyDown(KeyCode.E) && !_haveWeapon)
            {
                _currentWeapon = hit.transform.GetComponent<WeaponBehaviour>().
                    Claim(_rightHand);

                AmmoUI();

                _haveWeapon = true;
            }
        }
        else if (hit.transform.CompareTag("AmmoCage"))
        {
            if (Constants.Interface.PlayerMessage != "Press E")
                Constants.Interface.PlayerMessage = "Press E";

            if (Input.GetKeyDown(KeyCode.E) && _haveWeapon)
            {
                int ammo = hit.transform.GetComponent<CageBehaviour>().
                    Claim(_currentWeapon.Ammo._typeAmmo);

                if (ammo == 0) return;
                else
                {
                    if ((_currentWeapon.Weapon.AmmoCageAmount += ammo) >
                        _currentWeapon.Weapon.AmmoMax)
                    {
                        _currentWeapon.Weapon.AmmoCageAmount = _currentWeapon.Weapon.AmmoMax;
                    }
                }

                AmmoUI();
            }
        }

    }

    private void AmmoUI() 
        => Constants.Interface.OpenAmmoUI
        (_currentWeapon.Weapon.AmmoLeft, _currentWeapon.Weapon.AmmoCageAmount);
}