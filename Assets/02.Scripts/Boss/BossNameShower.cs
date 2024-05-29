using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNameShower : MonoBehaviour
{
    [SerializeField] private GameObject bossName;
    
    private void Awake()
    {
       // bossName =  GameObject.FindGameObjectWithTag("BossName"); //보스 이름
        bossName.GetComponent<RectTransform>().position = new Vector2(850, 0);
    }
    public void ShowBossTitel()
    {
        bossName.GetComponent<RectTransform>().position = new Vector2(-90, 0);
    }

    public void HideBossTitel()
    {
        bossName.GetComponent<RectTransform>().position = new Vector2(850, 0);
    }
}
