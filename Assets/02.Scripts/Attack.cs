using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    public GameObject mpMange;
    public AudioSource ATKSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if(damageable != null)
        {
           
            bool gotHit = damageable.Hit(attackDamage, knockback);

            if(gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
                mpMange.GetComponent<MP_Manage>().MpCharge();
            }
           
        }
    }
}
