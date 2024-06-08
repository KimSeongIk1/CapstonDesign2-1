using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject introCamera;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject fade;
    //[SerializeField] private GameObject introFire;
    [SerializeField] private GameObject introEffect;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip[] audioClip;
    private UIManager uiManager;

    private void Start()
    {
        fade.SetActive(true);
        player.GetComponent<PlayerController>().getKeyIgnore = true;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.clipShow(audioClip[0]);
        StartCoroutine(BossIntro());
    }
    IEnumerator BossIntro()
    {
        yield return new WaitForSeconds(3f);
        // introFire.SetActive(false);
        uiManager.clipShow(audioClip[1]);
        uiManager.clipShow(audioClip[2]);
        yield return new WaitForSeconds(0.1f);

        introEffect.SetActive(true);

        yield return new WaitForSeconds(0.52f);
        boss.SetActive(true);

        introEffect.SetActive(false);
        uiManager.clipShow(audioClip[0]);
        yield return new WaitForSeconds(2f);
        //boss.GetComponent<Animator>().SetTrigger("Intro");
        introCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().getKeyIgnore = false;
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        boss.GetComponent<Boss_Controller>().NextPatternPlay(0);
    }
}
