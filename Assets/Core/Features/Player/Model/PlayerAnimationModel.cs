using UnityEngine;

namespace Assets.Core.Features.Player.Model
{
    public class PlayerAnimationModel
    {
        public enum EstadosSinBatalla
        {
            Idle,
            Walk
        }

        public EstadosSinBatalla EstadoActual {get; set;} = EstadosSinBatalla.Idle;
    }
}