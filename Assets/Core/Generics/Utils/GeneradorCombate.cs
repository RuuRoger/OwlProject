using UnityEngine;
using Assets.Core.Features.Enemies.Models;

namespace Assets.Core.Generics.Utils
{
    public class GeneradorCombate : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private Transform _spawnEnemigo;

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY ---------------------------------------------
        ================================================================================================================= */
        private void Start()
        {
            EnemyData datosEnemigo = MediadorEscena.Enemigo;
            var enemigoInstanciado = Instantiate(datosEnemigo.PrefabEnemigo, _spawnEnemigo.position, _spawnEnemigo.rotation);

            var spriteRender = enemigoInstanciado.GetComponentInChildren<SpriteRenderer>();
            spriteRender.flipX = true;
        }
    }
}