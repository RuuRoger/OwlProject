using UnityEngine;

namespace Assets.Scripts.Player.Models
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