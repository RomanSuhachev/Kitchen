using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Player player;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private void Update()
        {
            animator.SetBool(IsWalking, player.IsWalking);
        }
    }
}
