using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerMassage;

    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _ammoIcon;

    public string PlayerMessage 
    { 
        get { return _playerMassage.text; }  
        set { _playerMassage.text = value; } 
    }

    public void OpenAmmoUI(int current, int max)
    {
        if(!_ammoIcon.activeSelf)_ammoIcon.SetActive(true);

        _ammoText.text = $"{current} / {max}";
    }
    public void CloseAmmoUI() => _ammoIcon.SetActive(false);
}
