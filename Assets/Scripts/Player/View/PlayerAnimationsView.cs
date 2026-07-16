using UnityEngine;
using Assets.Scripts.Player.Models;

namespace Assets.Scripts.Player.View
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