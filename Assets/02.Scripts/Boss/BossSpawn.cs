using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject introCamera;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject introFire;
    [SerializeField] private GameObject introBoss;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player.GetComponent<PlayerController>().getKeyIgnore = true;
        StartCoroutine(BossIntro());
    }
    IEnumerator BossIntro()
    {
        yield return new WaitForSeconds(5f);
        introFire.SetActive(false);
        introBoss.SetActive(true);
        yield return new WaitForSeconds(5f);
        introBoss.SetActive(false);
        boss.SetActive(true);

        yield return new WaitForSeconds(1f);
        introCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().getKeyIgnore = false;
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
}
