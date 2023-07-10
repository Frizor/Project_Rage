using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 joystickPosition = Vector2.zero;
    private bool isJoystickPressed = false;

    private void Update()
    {
        if (isJoystickPressed)
        {
            // Обновление позиции игрового персонажа на основе данных сенсорного джойстика
            // Можно вызвать методы движения или поворота персонажа здесь
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            Vector2 clampedPosition = Vector2.ClampMagnitude(position, joystickBackground.rect.width * 0.5f);
            joystickHandle.localPosition = clampedPosition;
            joystickPosition = clampedPosition.normalized;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickPressed = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickPressed = false;
        joystickHandle.localPosition = Vector2.zero;
        joystickPosition = Vector2.zero;
    }

    public Vector2 GetJoystickAxes()
    {
        return joystickPosition;
    }
}


/*using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 joystickPosition = Vector2.zero;
    private bool isJoystickPressed = false;

    private void Update()
    {
        if (isJoystickPressed)
        {
            // Обновление позиции игрового персонажа на основе данных сенсорного джойстика
            // Можно вызвать методы движения или поворота персонажа здесь
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            Vector2 clampedPosition = Vector2.ClampMagnitude(position, joystickBackground.rect.width * 0.5f);
            joystickHandle.localPosition = clampedPosition;
            joystickPosition = clampedPosition.normalized;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickPressed = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickPressed = false;
        joystickHandle.localPosition = Vector2.zero;
        joystickPosition = Vector2.zero;
    }

    public Vector2 GetJoystickAxes()
    {
        return joystickPosition;
    }
}*/

/*using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 joystickPosition = Vector2.zero;
    private bool isJoystickPressed = false;

    private void Update()
    {
        if (isJoystickPressed)
        {
            // Обновление позиции игрового персонажа на основе данных сенсорного джойстика
            // Можно вызвать методы движения или поворота персонажа здесь
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            Vector2 clampedPosition = Vector2.ClampMagnitude(position, joystickBackground.rect.width * 0.5f);
            joystickHandle.localPosition = clampedPosition;
            joystickPosition = clampedPosition.normalized;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickPressed = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickPressed = false;
        joystickHandle.localPosition = Vector2.zero;
        joystickPosition = Vector2.zero;
    }
}
*/