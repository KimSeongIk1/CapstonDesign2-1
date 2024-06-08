using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIDown : MonoBehaviour
{
    private RectTransform rectTransform;
    private float downSpeed = -0.1f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.position = new Vector2(0, 381f);
        StartCoroutine(UIDown());
    }
    IEnumerator UIDown()
    {
        while (rectTransform.position.y > -126)
        {
            rectTransform.position = new Vector2(0, downSpeed);
            yield return null;
        }
    }
}
