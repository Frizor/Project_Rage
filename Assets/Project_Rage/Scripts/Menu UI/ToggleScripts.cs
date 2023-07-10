using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public MonoBehaviour script1;
    public MonoBehaviour script2;

    private bool isScript1Active = true;

    public void ToggleScripts()
    {
        if (isScript1Active)
        {
            script1.enabled = false;
            script2.enabled = true;
        }
        else
        {
            script1.enabled = true;
            script2.enabled = false;
        }

        isScript1Active = !isScript1Active;
    }
}
