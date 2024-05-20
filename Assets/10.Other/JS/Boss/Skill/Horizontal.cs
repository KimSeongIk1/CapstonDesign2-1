using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    public GameObject player;
    private int damage = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 knockback = new Vector2(2, 3);
        Debug.Log("할퀴기 적중");
        player.GetComponent<Damageable>().Hit(damage,knockback);
    }
    public void AnimeEvent()
    {
        this.gameObject.SetActive(false);
    }
}
