using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : Interactable
{
    public bool sabotaged;
    Animator animator;
    public override void Interact()
    {
        sabotaged = true;
        animator = GetComponent<Animator>();
        StartCoroutine(Sabotage());
    }

    private IEnumerator Sabotage()
    {
        while (animator.speed < 3)
        {
            animator.speed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
