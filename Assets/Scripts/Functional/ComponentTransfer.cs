using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentTransfer : MonoBehaviour
{
    [SerializeField] private List<Component> _components;

    public Component GetObject(System.Type type, int index = 0) 
        => _components[index].GetComponent(type);
}
