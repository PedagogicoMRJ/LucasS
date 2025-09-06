using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool inBattle;

    private static readonly int HashInBattle = Animator.StringToHash("InBattle");
    private static readonly int HashAttack  = Animator.StringToHash("Attack");
    
    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    // Chame quando a batalha começar/terminar
    public void SetInBattle(bool value)
    {
        inBattle = value;
        if (animator != null)
            animator.SetBool(HashInBattle, inBattle);
    }

    // Chame quando o inimigo DECIDIR atacar
    public void RequestAttackAnimation()
    {
        if (!inBattle || animator == null)
            return;

        // Garante que o trigger será reconhecido
        animator.ResetTrigger(HashAttack);
        animator.SetTrigger(HashAttack);
    }
}
