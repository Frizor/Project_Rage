using UnityEngine;

public class CharacterActivator : MonoBehaviour
{
    public Character characterScript;

    private void Start()
    {
        // ���������� ������ Character
        characterScript.enabled = true;
    }
}