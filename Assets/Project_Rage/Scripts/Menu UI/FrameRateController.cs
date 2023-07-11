using UnityEngine;
using UnityEngine.UI;

public class FrameRateController : MonoBehaviour
{
    public Button toggleButton;
    public Text buttonText;

    private bool isTarget60FPS = false;

    private Color color30FPS = Color.red;
    private Color color60FPS = Color.green;

    private string text30FPS = "30 FPS";
    private string text60FPS = "60 FPS";

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleFrameRate);
        UpdateButtonVisuals();
    }

    private void ToggleFrameRate()
    {
        isTarget60FPS = !isTarget60FPS;

        if (isTarget60FPS)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }

        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        if (isTarget60FPS)
        {
            buttonText.text = text60FPS;
            buttonText.color = color60FPS;
        }
        else
        {
            buttonText.text = text30FPS;
            buttonText.color = color30FPS;
        }
    }
}