using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _sensivity = 200;
    [SerializeField] private Transform _player;

    private float mouseX;
    private float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * _sensivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * _sensivity * Time.deltaTime;

        _player.Rotate(mouseX * new Vector3(0, 1, 0));

        mouseY = Mathf.Clamp(mouseY, -90, 70);

        transform.localEulerAngles = new Vector3(mouseY, 0, 0);
    }
}
