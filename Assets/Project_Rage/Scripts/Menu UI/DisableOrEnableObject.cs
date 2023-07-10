using UnityEngine;
using UnityEngine.UI;

public class DisableOrEnableObject : MonoBehaviour
{
    public GameObject targetObject;
    public Button toggleButton;
    public Color activeColor;
    public Color inactiveColor;
    public string activeText;
    public string inactiveText;
    public float inactiveButtonAlpha = 0.8f;

    private bool isObjectActive = true;
    private Image buttonImage;
    private Text buttonText;

    private void Start()
    {
        buttonImage = toggleButton.GetComponent<Image>();
        buttonText = toggleButton.GetComponentInChildren<Text>();

        // Устанавливаем начальное состояние кнопки и объекта
        SetObjectState(isObjectActive);
        UpdateButtonAppearance();
    }

    public void ToggleObjectState()
    {
        isObjectActive = !isObjectActive;
        SetObjectState(isObjectActive);
        UpdateButtonAppearance();
    }

    private void SetObjectState(bool isActive)
    {
        targetObject.SetActive(isActive);
    }

    private void UpdateButtonAppearance()
    {
        if (isObjectActive)
        {
            buttonImage.color = activeColor;
            buttonText.text = activeText;
        }
        else
        {
            Color inactiveButtonColor = inactiveColor;
            inactiveButtonColor.a = inactiveButtonAlpha;

            buttonImage.color = inactiveButtonColor;
            buttonText.text = inactiveText;
        }
    }
}
