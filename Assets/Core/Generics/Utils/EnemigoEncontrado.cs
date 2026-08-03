using System;
using UnityEngine;
using Assets.Core.Features.Enemies.Models;

namespace Assets.Core.Generics.Utils
{
    public class EnemigoEncontrado : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private EnemyData _enemyData;

        private static Vector3 s_ignoreTriggerPosition;
        private static float s_ignoreTriggerRadius;
        private static bool s_ignoreNextTrigger;

        /* ================================================================================================================
        ---------------------------------------------------- EVENTOS -----------------------------------------------------
        ================================================================================================================= */
        public static event Action<EnemyData, Vector3, Quaternion, GameObject> OnEnemigoEncontrado;

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY -----------------------------------------------------
        ================================================================================================================= */ 
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (ShouldIgnoreThisTrigger())
            {
                s_ignoreNextTrigger = false;
                return;
            }

            OnEnemigoEncontrado?.Invoke(_enemyData, other.transform.position, other.transform.rotation, gameObject);
        }

        private bool ShouldIgnoreThisTrigger()
        {
            if (!s_ignoreNextTrigger)
            {
                return false;
            }

            return Vector3.Distance(transform.position, s_ignoreTriggerPosition) <= s_ignoreTriggerRadius;
        }

        public static void IgnoreNextTriggerAt(Vector3 position, float radius = 1f)
        {
            s_ignoreTriggerPosition = position;
            s_ignoreTriggerRadius = radius;
            s_ignoreNextTrigger = true;
        }
    }
}