using System;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator p_Animator;

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        p_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerMoveAnimation();
    }

    private void PlayerMoveAnimation()
    {
        if(playerController.p_moveAmt.sqrMagnitude > 0.001f)
        {
            p_Animator.SetBool("isRunning", true);
        }
        else
        {
            p_Animator.SetBool("isRunning", false);
        }
    }
}
