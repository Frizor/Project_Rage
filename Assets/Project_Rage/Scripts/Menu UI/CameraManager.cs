using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Transform MainCameraTransform { get; private set; }

    private void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }
}