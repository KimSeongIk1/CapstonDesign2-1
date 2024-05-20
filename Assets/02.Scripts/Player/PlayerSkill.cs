using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerSkill : MonoBehaviour
{
    public SkillData skillData;

    private void Update() {
        if (skillData.skillFire == true && GetComponent<SpriteRenderer>().flipX == true)
        {
            print("좌로이동!@!@!@!");
            this.transform.Translate(Vector3.left * (skillData.skillSpeed*Time.deltaTime));
        }
        else if(skillData.skillFire == true && GetComponent<SpriteRenderer>().flipX == false)
        {
            print("우로이동!@!@!@!");
            this.transform.Translate(Vector3.left * (skillData.skillSpeed * Time.deltaTime));
        }
            //this.transform.Translate(-skillData.skillSpeed, 0, 0 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag.Equals("Enemy") || collision.tag.Equals("Boss"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                bool gotHit = damageable.Hit(skillData.damage, skillData.knockback);
                
                if (gotHit)
                {
                    Debug.Log(collision.name + " hit for " + skillData.damage);
                }
            }
        }
    }
}
