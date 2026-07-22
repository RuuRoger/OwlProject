using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Core.Features.Enemies.Models;

namespace Assets.Core.Generics.Utils
{
    public class MediadorEscena : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private string _nombreEscena;
        private static EnemyData _enemigo;

        /* ================================================================================================================
        ---------------------------------------------------- PROPIEDADES -----------------------------------------------------
        ================================================================================================================= */
        public static EnemyData Enemigo
        {
            get
            {
                return _enemigo;
            }
            private set
            {
                _enemigo = value;
            }
        }

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY -----------------------------------------------------
        ================================================================================================================= */
        private void OnEnable()
        {
            EnemigoEncontrado.OnEnemigoEncontrado += HandleEnemiogoEncontrado;
        }

        private void OnDisable()
        {
            EnemigoEncontrado.OnEnemigoEncontrado -= HandleEnemiogoEncontrado;
            
        }

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS -----------------------------------------------------
        ================================================================================================================= */
        private void HandleEnemiogoEncontrado(EnemyData enemigoAEnfrentar)
        {
            Enemigo = enemigoAEnfrentar;
            SceneManager.LoadScene(_nombreEscena);
        }
    }
}