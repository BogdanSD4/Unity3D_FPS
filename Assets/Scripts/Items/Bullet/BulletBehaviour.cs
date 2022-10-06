using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class BulletBehaviour : MonoBehaviour
{
    private Ammo _ammo;

    public Ammo SetAmmo{ set { _ammo = value; } }

    private float Timer;
    private void Update()
    {
        Vector3 CameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Constants.Camera.ScreenPointToRay(CameraCenter);

        Vector3 cam = ray.origin + ray.direction * 5;
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(cam.x, cam.y, cam.z + 20), _ammo._bulletSpeed * Time.deltaTime);
        //transform.Translate(0,0,_ammo._bulletSpeed * Time.deltaTime);
        Timer += Time.deltaTime;
        if (Timer > _ammo._lifeDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Weapon"))
        {
            EnviromentBehaviour eb;
            if ((eb = other.GetComponent<EnviromentBehaviour>()) != null)
            {
                eb.Damage(_ammo._bulletDamage);
            }
            Destroy(this.gameObject);
        }
    }
}
