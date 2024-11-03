using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;

    public Transform attackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayer;

    public int AttackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime){

            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,AttackRange,EnemyLayer);
        foreach (Collider2D c in enemies)
        {
            c.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,AttackRange);
    }
}
