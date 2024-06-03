using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_summon : MonoBehaviour
{
    private GameObject player;
    public SkillData skillData;
    public Vector2 playerDistance;
    public Collider2D sommonAttackRange;
    public Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        player = DataManager.Instance.Player;
    }

    private void FixedUpdate() {
        
    }
    void Update()
    {
        playerDistance = this.transform.position - player.transform.position;
    }
}
