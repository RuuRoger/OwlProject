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
        private static GameObject _triggerSource;
        private static Vector3 _playerPosition;
        private static Quaternion _playerRotation;
        private static bool _playerTransformGuardado;
        private static Vector3 _triggerPosition;
        private static bool _disableReloadedTrigger;

        static MediadorEscena()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

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

        private void HandleEnemiogoEncontrado(EnemyData enemigoAEnfrentar, Vector3 playerPosition, Quaternion playerRotation, GameObject triggerSource)
        {
            Enemigo = enemigoAEnfrentar;
            GuardarEstadoSalida(playerPosition, playerRotation, triggerSource);
            DestroyTriggerSource();
            StartCoroutine(TransicionEscena());
        }

        private static void GuardarEstadoSalida(Vector3 playerPosition, Quaternion playerRotation, GameObject triggerSource)
        {
            _playerPosition = playerPosition;
            _playerRotation = playerRotation;
            _playerTransformGuardado = true;
            _triggerSource = triggerSource;

            if (_triggerSource != null)
            {
                _triggerPosition = _triggerSource.transform.position;
                _disableReloadedTrigger = true;
                EnemigoEncontrado.IgnoreNextTriggerAt(_triggerPosition, 1f);
            }
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex != 0)
            {
                return;
            }

            DisableReloadedTrigger();
            GameObject player = GameObject.FindWithTag("Player");
            RestaurarTransformJugador(player);
            DestroyTriggerSource();
        }

        private static void DisableReloadedTrigger()
        {
            if (!_disableReloadedTrigger)
            {
                return;
            }

            EnemigoEncontrado[] triggers = Object.FindObjectsByType<EnemigoEncontrado>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var trigger in triggers)
            {
                if (Vector3.Distance(trigger.transform.position, _triggerPosition) < 0.2f)
                {
                    Object.Destroy(trigger.gameObject);
                    break;
                }
            }

            _disableReloadedTrigger = false;
        }

        public static void RestaurarTransformJugador(GameObject player)
        {
            if (!_playerTransformGuardado || player == null)
            {
                return;
            }

            player.transform.position = _playerPosition;
            player.transform.rotation = _playerRotation;
            _playerTransformGuardado = false;
        }

        public static void DestroyTriggerSource()
        {
            if (_triggerSource == null)
            {
                return;
            }

            Object.Destroy(_triggerSource);
            _triggerSource = null;
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