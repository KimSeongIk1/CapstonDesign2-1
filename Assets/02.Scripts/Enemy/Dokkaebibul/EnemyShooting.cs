using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int Range;
    public float coolTime;
    public bool isAttack;

    private float timer;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!isAttack)
        {
            StartCoroutine(Shoot());
        }
        /*timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if(distance < Range)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
            }
        }*/
    }
    public IEnumerator Shoot()
    {
        isAttack = true;
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        yield return new WaitForSeconds(coolTime);
        isAttack = false;
    }

}