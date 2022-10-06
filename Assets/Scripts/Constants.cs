using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class Constants : MonoBehaviour
{
    [SerializeField] private Transform _bulletPack;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _weaponStand;
    [SerializeField] private Interface _interface;
    [SerializeField] private Transform _levelItemsPack;

    public static Transform BulletPack;
    public static Camera Camera;
    public static Transform Player;
    public static Transform WeaponStand;
    public static Interface Interface;
    public static Transform LevelItemsPack;

    public const int Layer_Weapon = 6;
    void Awake()
    {
        BulletPack = _bulletPack;
        Camera = _camera;
        Player = _player;
        WeaponStand = _weaponStand;
        Interface = _interface;
        LevelItemsPack = _levelItemsPack;
    }

    public static Vector3 VectorRotation(float angle)
    {
        int factor = 0;
        int x = 1;
        int y = 1;

        while (angle > 90)
        {
            angle -= 90;
            factor++;
        }

        switch (factor)
        {
            case 1: x = -1;
                break;
            case 2: y = -1; x = -1;
                break;
            case 3: y = -1;
                break;
        }

        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * y, 1, Mathf.Cos(Mathf.Deg2Rad * angle) * x);
    }
}
