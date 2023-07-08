using UnityEngine;
using UnityEngine.EventSystems;

public class Pickup : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;
    public GameObject slotButton;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            PickUpItem();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        if (inventoryManager == null)
        {
            Debug.LogWarning("InventoryManager not found.");
            return;
        }

        bool pickedUp = inventoryManager.AddItem(slotButton);
        if (pickedUp)
        {
            Destroy(gameObject);
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickup : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;
    public GameObject slotButton;
    public GameObject itemPanel; // Ссылка на панель с предметом

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
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
                    break;
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // При нажатии на предмет активируем панель с предметом
        itemPanel.SetActive(true);

        // Здесь можно передать информацию о предмете в панель, если необходимо
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public GameObject slotButton;

    private void Start()
    {
        //Debug.Log("init");
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        //inventoryManager = player.GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventoryManager.slots.Length; i++)
            {
                if (inventoryManager.ifFull[i] == false)
                {
                    inventoryManager.ifFull[i] = true;
                    Instantiate(slotButton, inventoryManager.slots[i].transform);
                    Destroy(gameObject);
                    //Debug.Log("Item picked up and added to the inventory."); // Добавьте эту строку для отладки
                    break;
                }
            }
        }
    }
}*/

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
