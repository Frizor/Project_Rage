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
        // ���������� ��� ��� ��������� ��������
    }

    public void DropItem()
    {
        // ���������� ��� ��� ������������ �������� �� �����
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanelActive : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemPanel; // ������ ItemPanel

    private void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(itemPanel);
        if (itemPanel != null)
        {
            itemPanel.SetActive(true); // ������������ ������ ItemPanel
        }
    }
}
*/