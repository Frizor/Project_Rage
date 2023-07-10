using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    public delegate void ExperienceChangeHandle(int amount);
    public event ExperienceChangeHandle OnExperienceChange;

    //Одноэлементная проверка
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}

