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
        [SerializeField] private string m_nombreEscena;
        [SerializeField] private AnimacionesEscena m_animacionesEscena;
        private static EnemyData m_enemigo;
        private static GameObject m_triggerSource;
        private static Vector3 _playerPosition;
        private static Quaternion m_playerRotation;
        private static bool m_playerTransformGuardado;
        private static Vector3 m_triggerPosition;
        private static bool m_disableReloadedTrigger;

        /* ================================================================================================================
        ---------------------------------------------------- PROPIEDADES -----------------------------------------------------
        ================================================================================================================= */
        public static EnemyData Enemigo
        {
            get
            {
                return m_enemigo;
            }
            private set
            {
                m_enemigo = value;
            }
        }

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY -----------------------------------------------------
        ================================================================================================================= */
        private void OnEnable()
        {
            EnemigoEncontrado.OnEnemigoEncontrado += HandleEnemiogoEncontrado;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            EnemigoEncontrado.OnEnemigoEncontrado -= HandleEnemiogoEncontrado;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void HandleEnemiogoEncontrado(EnemyData enemigoAEnfrentar, Vector3 playerPosition, Quaternion playerRotation, GameObject triggerSource)
        {
            Enemigo = enemigoAEnfrentar;
            GuardarEstadoSalida(playerPosition, playerRotation, triggerSource);
            DestroyTriggerSource();
            AudioManager.Instance?.PlayTransitionMusic();
            StartCoroutine(TransicionEscena());
        }

        private static void GuardarEstadoSalida(Vector3 playerPosition, Quaternion playerRotation, GameObject triggerSource)
        {
            _playerPosition = playerPosition;
            m_playerRotation = playerRotation;
            m_playerTransformGuardado = true;
            m_triggerSource = triggerSource;

            if (m_triggerSource != null)
            {
                m_triggerPosition = m_triggerSource.transform.position;
                m_disableReloadedTrigger = true;
                EnemigoEncontrado.IgnoreNextTriggerAt(m_triggerPosition, 1f);

                if (m_triggerSource.TryGetComponent<PersistentTrigger>(out var persistentTrigger))
                {
                    PersistentTrigger.MarkDestroyed(persistentTrigger.TriggerId);
                }
            }
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                AudioManager.Instance?.PlayOriginalMusic();
            }
            else
            {
                AudioManager.Instance?.StopMusic();
            }

            DisableReloadedTrigger();
            GameObject player = GameObject.FindWithTag("Player");
            RestaurarTransformJugador(player);
            DestroyTriggerSource();
        }

        private static void DisableReloadedTrigger()
        {
            if (!m_disableReloadedTrigger)
            {
                return;
            }

            EnemigoEncontrado[] triggers = Object.FindObjectsByType<EnemigoEncontrado>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var trigger in triggers)
            {
                if (Vector3.Distance(trigger.transform.position, m_triggerPosition) < 0.2f)
                {
                    Object.Destroy(trigger.gameObject);
                    break;
                }
            }

            m_disableReloadedTrigger = false;
        }

        public static void RestaurarTransformJugador(GameObject player)
        {
            if (!m_playerTransformGuardado || player == null)
            {
                return;
            }

            player.transform.position = _playerPosition;
            player.transform.rotation = m_playerRotation;
            m_playerTransformGuardado = false;
        }

        public static void DestroyTriggerSource()
        {
            if (m_triggerSource == null)
            {
                return;
            }

            if (m_triggerSource.TryGetComponent<TriggerDestroyCounter>(out var destroyCounter))
            {
                TriggerDestroyCounter.AddDestroyedTrigger();
            }

            Object.Destroy(m_triggerSource);
            m_triggerSource = null;
        }

        private IEnumerator TransicionEscena()
        {
            m_animacionesEscena.ActivarTransicion();
            yield return null;

            // Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(2.5f);

            // Time.timeScale = 1f;
            SceneManager.LoadScene(m_nombreEscena);
        }
    }
}