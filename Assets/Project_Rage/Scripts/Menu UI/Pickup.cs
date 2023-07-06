using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public GameObject slotButton;

    private void Start()
    {
        Debug.Log("init");
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        //inventoryManager = player.GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventoryManager.slots.Length; i++)
            {
                if (inventoryManager.ifFull[i] == false)
                {
                    inventoryManager.ifFull[i] = true;
                    Instantiate(slotButton, inventoryManager.slots[i].transform);
                    Destroy(gameObject);
                    Debug.Log("Item picked up and added to the inventory."); // Добавьте эту строку для отладки
                    break;
                }
            }
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public GameObject slotButton;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventoryManager = player.GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventoryManager.slots.Length; i++)
            {
                if (inventoryManager.ifFull[i] == false)
                {
                    inventoryManager.ifFull[i] = true;
                    Instantiate(slotButton, inventoryManager.slots[i].transform);
                    Destroy(gameObject);
                    Debug.Log("Item picked up and added to the inventory."); // Добавьте эту строку для отладки
                    break;
                }
            }
        }
    }
}*/
