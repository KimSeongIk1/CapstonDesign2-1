using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool isStart = false;
    Image image;
    private float fadeSpeed = 1f;
    [SerializeField] private GameObject backImg;
    [SerializeField] private GameObject text;

    public void GameStart(InputAction.CallbackContext context)
    {
        if(isStart == true)
        {
            return;
        }
        isStart = true;
        Debug.Log("게임 시작");
        StartCoroutine(FadeOut());
        
    }
    IEnumerator FadeOut()
    {
        //image.color = backImg.GetComponent<Image>().color;
        while (backImg.GetComponent<Image>().color.a > 0)
        {
            Color color = backImg.color;
            color.a -= Time.deltaTime * fadeSpeed;
            image.color = color;
            yield return null;  
        }
        SceneManager.LoadScene(1);
    }
}

