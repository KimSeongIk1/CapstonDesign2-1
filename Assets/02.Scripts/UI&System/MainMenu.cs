using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool isStart = false;
    TextMesh text;
    Image image;
    CanvasGroup canvas;
    [SerializeField] private GameObject backImg;
    [SerializeField] private GameObject textObj;

    private void Start()
    {
        canvas = textObj.GetComponent<CanvasGroup>();
        image = backImg.GetComponent<Image>();
        if(isStart != true)
        {
            StartCoroutine(FadeText());
        }
    }
    IEnumerator FadeText()
    {
        while(canvas.alpha >= 0.3f)
        {
            canvas.alpha -= Time.deltaTime * 0.5f;
            yield return null;
        }
        while (canvas.alpha < 1f)
        {
            canvas.alpha += Time.deltaTime * 0.5f;
            yield return null;
        }
        StartCoroutine(FadeText());
    }
    public void GameStart(InputAction.CallbackContext context)
    {
        if (isStart == true)
        {
            return;
        }
        isStart = true;
        Debug.Log("게임 시작");
        StartCoroutine(FadeOut());

    }

    [SerializeField] private float fadeSpeed = 5f;
    IEnumerator FadeOut()
    {
        //image.color = backImg.GetComponent<Image>().color;
        while (image.color.a > 0)
        {
            Color color = image.color;
            color.a -= Time.deltaTime * fadeSpeed;
            image.color = color;
            yield return null;  
        }
        SceneManager.LoadScene(1);
    }
}

