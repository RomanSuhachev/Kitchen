using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Player player;
    
    private void Update()
    {
        animator.SetBool("IsWalking", player.IsWalking);
    }
}
