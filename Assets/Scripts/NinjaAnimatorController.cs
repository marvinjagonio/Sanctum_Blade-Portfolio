using UnityEngine;

public class NinjaAnimatorController : MonoBehaviour
{
    private NinjaController ninjaController;
    private Animator ninjaAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ninjaController = transform.parent.GetComponent<NinjaController>();
        ninjaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NinjaMovementAnimation();
    }

    private void NinjaMovementAnimation()
    {
        if (ninjaController.isWalking)
        {
            ninjaAnimator.SetBool("isMoving", true);
        }
        else 
        {
            ninjaAnimator.SetBool("isMoving", false);
        }
    }
}
