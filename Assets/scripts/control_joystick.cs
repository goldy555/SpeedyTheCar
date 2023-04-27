using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class control_joystick : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private Image b_img;
    private Image joystick_img;
    private Vector3 in_vector;

    // Start is called before the first frame update
    void Start()
    {
        b_img = GetComponent<Image>();
        joystick_img = transform.GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(b_img.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = pos.x / b_img.rectTransform.sizeDelta.x;
            pos.y = pos.y / b_img.rectTransform.sizeDelta.y;
            in_vector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            in_vector = (in_vector.magnitude > 1.0f) ? in_vector.normalized : in_vector;
            // Debug. Log(input_vector);
            joystick_img.rectTransform.anchoredPosition = new Vector3(in_vector.x * (b_img.rectTransform.sizeDelta.x / 3),
            in_vector.z * (b_img.rectTransform.sizeDelta.y / 3));
        }
    }
    public float joystick_LR()
    {
        if (in_vector.x != 0)
        {
            return in_vector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float joystick_TB()
    {
        if (in_vector.z != 0)
        {
            return in_vector.z;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        in_vector = Vector3.zero;
        joystick_img.rectTransform.anchoredPosition = Vector3.zero;
    }
}