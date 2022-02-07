using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private AudioSource attackSound;

    private bool _canAttack = true;

    public void FinishAttack()
    {
        _canAttack = true;
    }

    //private void Update() νΰ οκ
    //{
    //    if (Input.GetMouseButtonDown(0) && _canAttack)
    //    {
    //        Attack();
    //   }
    //}

    public void Attack()
    {
        _canAttack = false;
        weapon.EnemyInRange();
        animator.SetTrigger("attack");
        attackSound.Play();
    }
}

