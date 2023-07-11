using UnityEngine;

public class CharacterActivator : MonoBehaviour
{
    public Character characterScript;

    private void Start()
    {
        // Активируем скрипт Character
        characterScript.enabled = true;
    }
}