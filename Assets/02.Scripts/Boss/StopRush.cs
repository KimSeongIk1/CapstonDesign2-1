using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
            GameObject.Find("Dueogsini").GetComponent<Boss_Controller>().isBroken = true;
    }
}
