using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public int i;

    private void Start()
    {
        //Debug.Log("init");
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        //Debug.Log(other);
        if (transform.childCount <= 0)
        {
            inventoryManager.ifFull[i] = false;
        }

    }
}
