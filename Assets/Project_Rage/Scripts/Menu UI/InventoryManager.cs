using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool[] ifFull;
    public GameObject[] slots;

    private void Start()
    {
        Debug.Log("Inventory Manager initialized."); // Добавьте эту строку для отладки
    }
}
