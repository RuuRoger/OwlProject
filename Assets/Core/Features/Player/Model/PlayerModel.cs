using UnityEngine;

namespace Assets.Core.Features.Player.Model
{
    [CreateAssetMenu(fileName ="PlayerData", menuName ="OwlProject/Player Model")]
    public class PlayerModel : ScriptableObject
    {
        // Editor
        [SerializeField]
        [Tooltip("Velocidad del jugador en unidades por segundo")]
        private float _velocidadPlayer = 5f;
        
        [SerializeField]
        [Tooltip("Velocidad del jugador al momento de rotar, cuantificado como número decimal")]
        private float _velocidadRotacion = 5f;
        
        // Propiedades
        public float VelocidadPlayer => _velocidadPlayer;
        public float VelocidadRotacion => _velocidadRotacion;
    }
}