using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

namespace Assets.Core.Generics.Utils
{
    public class IniciadorDialogo : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [SerializeField, TextArea] private string[] m_textoDialogo;
        [SerializeField] private TMP_Text m_textoUI;
        [SerializeField] private float m_tiempoTipeo = 0.05f;
        [SerializeField] private float m_inicioTiempoTipeo = 1f;
        [SerializeField] private GameObject m_trianguloTextoUI;
        [SerializeField] private InputSystem_Actions m_inputPlayer;
        private bool m_waitingForRead;

        private string m_nombreEnemigo;

        /* ================================================================================================================
        ---------------------------------------------------- EVENTOS -----------------------------------------------------
        ================================================================================================================= */
        public static event Action OnCombateIniciado;

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY -----------------------------------------------------
        ================================================================================================================= */

        private void Awake()
        {
            m_inputPlayer = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            GeneradorCombate.OnTagEnemigo += ObtenerNombreEnemigoInicioEscenaBatalla;
            m_inputPlayer.Combat.Read.performed += OnReadPerformed;
            m_inputPlayer.Combat.Enable();
        }

        private void OnDisable()
        {
            GeneradorCombate.OnTagEnemigo -= ObtenerNombreEnemigoInicioEscenaBatalla;
            m_inputPlayer.Combat.Read.performed -= OnReadPerformed;
            m_inputPlayer.Combat.Disable();
        }

        private void OnReadPerformed(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            m_waitingForRead = false;
        }

        private string ObtenerDialogoInicial(int indice)
        {
            if (indice < 0 || indice >= m_textoDialogo.Length)
            {
                return string.Empty;
            }

            string texto = m_textoDialogo[indice];

            if (indice == 0)
            {
                return string.Format(texto, m_nombreEnemigo);   
            }

            return texto;
        }

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS -----------------------------------------------------
        ================================================================================================================= */
        private void ObtenerNombreEnemigoInicioEscenaBatalla(string tagEnemigo)
        {            
            m_nombreEnemigo = tagEnemigo;
            StartCoroutine(MostarTextoInicial(0));
        }

        private IEnumerator MostarTextoInicial(int indice)
        {
            yield return new WaitForSeconds(m_inicioTiempoTipeo);

            m_textoUI.text = string.Empty;

            string textoAProcesar = ObtenerDialogoInicial(indice); 

            foreach (char caracter in textoAProcesar)
            {
                m_textoUI.text += caracter;
                yield return new WaitForSeconds(m_tiempoTipeo);
            }
            
            m_trianguloTextoUI.SetActive(true);
            m_waitingForRead = true;

            yield return new WaitUntil(() => m_waitingForRead == false);

            OnCombateIniciado?.Invoke();
        }
    }
}