using UnityEngine;

public class ItemDialog : MonoBehaviour
{
    public GameObject dialogCanvas;

    public void ShowDialog()
    {
        dialogCanvas.SetActive(true);
    }

    public void CloseDialog()
    {
        dialogCanvas.SetActive(false);
    }

    public void EquipItem()
    {
        // Реализуйте код для надевания предмета
    }

    public void DropItem()
    {
        // Реализуйте код для выбрасывания предмета на землю
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanelActive : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemPanel; // Объект ItemPanel

    private void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(itemPanel);
        if (itemPanel != null)
        {
            itemPanel.SetActive(true); // Активировать объект ItemPanel
        }
    }
}
*/