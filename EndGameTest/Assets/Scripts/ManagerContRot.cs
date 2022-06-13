using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ManagerContRot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    /*
    private Image imgCharRotArea, imgCharRotStick;
    private Vector2 posOut;
    // Start is called before the first frame update
    void Start()
    {
        imgCharRotArea = GetComponent<Image>();
        imgCharRotStick = transform.GetChild(0).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgCharRotArea.rectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out posOut
            ))
        {
            Debug.Log(posOut.x.ToString() + "/" + posOut.y.ToString());
            imgCharRotStick.rectTransform.anchoredPosition = new Vector2(
                posOut.x * (imgCharRotStick.rectTransform.sizeDelta.x / 100),
                posOut.y * (imgCharRotStick.rectTransform.sizeDelta.y / 100));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        imgCharRotStick.rectTransform.anchoredPosition = Vector2.zero;
        posOut.x = 0;
        posOut.y = 0;
    }


    public float InputRotHorizontal()
    {
        if (posOut.x != 0)
            return posOut.x;
        else
            return 0;
    }
    
    public float InputRotVertical()
    {
        if (posOut.y != 0)
            return posOut.y;
        else
            return 0;


    }
    */
    private Image imgJoystickBg;
    private Image imgJoystick;
    private Vector2 posInput;
    private float limitJoystick = 2;
    void Start()
    {
        imgJoystickBg = GetComponent<Image>();
        imgJoystick = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgJoystickBg.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput))
        {
            posInput.x = posInput.x / (imgJoystickBg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgJoystickBg.rectTransform.sizeDelta.y);

            //normalize
            if (posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }

            //Debug.Log(posInput.x.ToString() + "/" + posInput.y.ToString());
            //joystickMove
            imgJoystick.rectTransform.anchoredPosition = new Vector2(
                posInput.x * (imgJoystickBg.rectTransform.sizeDelta.x / limitJoystick),
                posInput.y * (imgJoystickBg.rectTransform.sizeDelta.y / limitJoystick));
        }
    }

    public void OnPointerDown(PointerEventData evenData)
    {
        OnDrag(evenData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }


    public float inputHorizontal()
    {
        if (posInput.x != 0)
            return posInput.x;
        else
            return 0;
    }

    public float inputVertical()
    {
        if (posInput.y != 0)
            return posInput.y;
        else
            return 0;

    }
}
