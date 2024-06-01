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

    private void Start()
    {
        StartCoroutine(BossIntro());
    }
    IEnumerator BossIntro()
    {
        yield return new WaitForSeconds(3f);
        introFire.SetActive(false);
        introBoss.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        introBoss.SetActive(false);
        boss.SetActive(true);

        yield return new WaitForSeconds(1f);
        introCamera.SetActive(false);
        mainCamera.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
}
