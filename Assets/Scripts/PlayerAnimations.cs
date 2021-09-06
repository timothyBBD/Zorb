using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void ShootingFinished()
    {
        animator.SetBool("isShooting", false);
    }

}
