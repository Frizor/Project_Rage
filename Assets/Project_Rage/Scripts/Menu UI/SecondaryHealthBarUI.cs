using UnityEngine;
using UnityEngine.UI;

public class SecondaryHealthBarUI : MonoBehaviour
{
    public Slider healthSlider; // ������ �� Slider ������ ������� ��������
    public Gradient gradient; // �������� ��� ������ ������� ��������
    public Image fill; // ����������� ��� ������ ������� ��������

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}


/*using UnityEngine;
using UnityEngine.UI;

public class SecondaryHealthBarUI : MonoBehaviour
{
    public Slider healthSlider; // ������ �� Slider ������ ������� ��������

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}*/