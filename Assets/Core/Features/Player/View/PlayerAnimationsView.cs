using UnityEngine;
using Assets.Core.Features.Player.Model;

namespace Assets.Core.Features.Player.View
{
    public class PlayerAnimationsView : MonoBehaviour
    {
        [Header("Estados de animación")]
        [Space(15)]
        [SerializeField] private Animator _animator;

        public void ActualizarAniamciones(PlayerAnimationModel.EstadosSinBatalla estado)
        {
            switch(estado)
            {
                case PlayerAnimationModel.EstadosSinBatalla.Idle:
                    _animator.SetBool("isWalking", false);
                    break;

                case PlayerAnimationModel.EstadosSinBatalla.Walk:
                    _animator.SetBool("isWalking", true);
                    break;
            }
        }
    }
}