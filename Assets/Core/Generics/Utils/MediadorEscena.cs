using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Core.Features.Enemies.Models;
using System.Collections;

namespace Assets.Core.Generics.Utils
{
    public class MediadorEscena : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private string _nombreEscena;
        [SerializeField] private AnimacionesEscena _animacionesEscena;
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
            StartCoroutine(TransicionEscena());
        }

        private IEnumerator TransicionEscena()
        {
            _animacionesEscena.ActivarTransicion();
            yield return null;

            // Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(2.5f);

            // Time.timeScale = 1f;
            SceneManager.LoadScene(_nombreEscena);
        }
    }
}