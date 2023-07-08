using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool[] ifFull;
    public GameObject[] slots;
    public GameObject itemPanel; // —сылка на панель с предметом

    private void Start()
    {
        HideItemPanel(); // —крываем панель с предметом при запуске игры
    }

    public bool AddItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!ifFull[i])
            {
                ifFull[i] = true;
                Instantiate(item, slots[i].transform);
                return true;
            }
        }
        return false;
    }

    public void ShowItemPanel()
    {
        itemPanel.SetActive(true);
    }

    public void HideItemPanel()
    {
        itemPanel.SetActive(false);
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool[] ifFull;
    public GameObject[] slots;
    public GameObject itemPanel; // —сылка на панель с предметом

    private void Start()
    {
        //itemPanel.SetActive(false); // —крываем панель с предметом при запуске игры
    }
}*/
