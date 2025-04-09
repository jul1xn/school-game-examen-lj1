using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleMultiplier = 1.1f;
    public float lerpTime = 5;
    Vector3 baseScale;
    Vector3 targetScale;
    bool setScale = false;

    private void Start()
    {
        baseScale = transform.localScale;
        targetScale = baseScale;
        setScale = true;
    }

    private void OnEnable()
    {
        if (setScale)
        {
            OnPointerExit(new PointerEventData(EventSystem.current));
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpTime * Time.deltaTime);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        targetScale = baseScale * scaleMultiplier;
    }

    public void OnPointerExit(PointerEventData data)
    {
        targetScale = baseScale;
    }
}
