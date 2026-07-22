using UnityEngine;

namespace Assets.Core.Features.Enemies
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "OwlProject/Data/Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        // Editor 
        [SerializeField] 
        [Tooltip("Nombre del enemigo")]
        private string _nombre = "Enemigo";
        
        [SerializeField]
        [Tooltip("Vida del enemigo")]
        private float _vidaMaxima = 50f;
        
        [SerializeField]
        [Tooltip("Daño causado por el ataque básico")]
        private float _danoAtaqueBasico = 10f;
        
        [SerializeField]
        [Tooltip("Daño causado por el ataque fuerte")]
        private float _danoAtaqueFuerte = 15f;

        // Propiedades
        public string Nombre => _nombre;
        public float VidaMaxima => _vidaMaxima;
        public float DanoAtaqueBasico => _danoAtaqueBasico;
        public float DanoAtaqueFuerte => _danoAtaqueFuerte;
    }
}