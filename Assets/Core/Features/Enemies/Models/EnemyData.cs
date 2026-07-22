using System;
using UnityEngine;

namespace Assets.Core.Features.Enemies.Models
{
    [CreateAssetMenu(fileName ="Enemigo", menuName ="OwlProject/Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField] private GameObject _prefabEnemigo;
        [SerializeField] private float _vidaEnemigo;
        [SerializeField] private float _ataqueBasico;
        [SerializeField] private float _ataqueFuerte;

        /* ================================================================================================================
        ---------------------------------------------------- PROPIEDADES -----------------------------------------------------
        ================================================================================================================= */
        public GameObject PrefabEnemigo
        {
            get
            {
                return _prefabEnemigo;
            }
        }

        public float VidaEnemigo
        {
            get
            {
                return _vidaEnemigo;
            }
            set
            {
                _vidaEnemigo = value;
                
                if (_vidaEnemigo <= 0)
                {
                    _vidaEnemigo = 0f;
                }
            }
        }

        public float AtaqueBasico
        {
            get
            {
                return _ataqueBasico; 
            }
            set
            {
                _ataqueBasico = value;
            }
        }
    
        public float AtaqueFuerte
        {
            get
            {
                return _ataqueFuerte;
            }
            set
            {
                _ataqueFuerte = value;
            }
        }
    }
}