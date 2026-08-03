using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.core.Features.EscenaAniamciones.Batalla
{
    public class AnimacionEntradaBatallaEscena : MonoBehaviour
    {
        [Header("Paneles")]
        [Space(10)]
        [SerializeField] private GameObject m_panelVS;
        [SerializeField] private GameObject m_menuBatalla;
        [SerializeField] private GameObject m_cajaTexto;
        
        [Header("UI")]
        [Space(10)]
        [SerializeField] private GameObject m_vidaPlayer;
        [SerializeField] private GameObject m_vidaEnemigo;
        
        [Header("Configuraciones escena")]
        [Space(10)]
        [SerializeField] private float m_tiempoAQuitarVS = 2f;

        [Header("Líneas de diálogo")]
        [Space(10)]
        [SerializeField, TextArea] private string[] m_dialogo;


        private void Start()
        {
            StartCoroutine(InicioEscenaBatalla());
        }

        private IEnumerator InicioEscenaBatalla()
        {
            yield return new WaitForSecondsRealtime(m_tiempoAQuitarVS);

            m_panelVS.SetActive(false);
            AgregarUI();
        }

        private void AgregarUI()
        {
            m_menuBatalla.SetActive(true);
            m_vidaPlayer.SetActive(true);
            m_vidaEnemigo.SetActive(true);
            m_cajaTexto.SetActive(true);
        }
    }
}