using UnityEngine;
using Assets.Core.Features.Enemies.Models;
using System;

namespace Assets.Core.Generics.Utils
{
    public class GeneradorCombate : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private Transform _spawnEnemigo;

        /* ================================================================================================================
        ---------------------------------------------------- EVENTOS -----------------------------------------------------
        ================================================================================================================= */
        public static event Action<string> OnTagEnemigo;

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY ---------------------------------------------
        ================================================================================================================= */
        private void Start()
        {
            EnemyData datosEnemigo = MediadorEscena.Enemigo;
            var enemigoInstanciado = Instantiate(datosEnemigo.PrefabEnemigo, _spawnEnemigo.position, _spawnEnemigo.rotation);
            OnTagEnemigo?.Invoke(enemigoInstanciado.tag);

            var spriteRender = enemigoInstanciado.GetComponentInChildren<SpriteRenderer>();
            spriteRender.flipX = true;
        }
    }
}