using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject introCamera;
    [SerializeField] private GameObject mainCamera;
    //[SerializeField] private GameObject introFire;
    [SerializeField] private GameObject introEffect;
    [SerializeField] private GameObject player;
    private void Start()
    {
        player.GetComponent<PlayerController>().getKeyIgnore = true;
        StartCoroutine(BossIntro());
    }
    IEnumerator BossIntro()
    {
        yield return new WaitForSeconds(3f);
       // introFire.SetActive(false);
        introEffect.SetActive(true);
        yield return new WaitForSeconds(0.52f);
        boss.SetActive(true);
        introEffect.SetActive(false);
        
        yield return new WaitForSeconds(3.1f);
        //boss.GetComponent<Animator>().SetTrigger("Intro");
        introCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().getKeyIgnore = false;
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
}
