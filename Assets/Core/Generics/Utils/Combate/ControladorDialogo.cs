using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

namespace Assets.Core.Generics.Utils.Combate
{
    public class ControladorDialogo : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        [Header("Inputs")]
        [SerializeField] private InputSystem_Actions m_inputPlayer;

        [Header("Textos")]
        [SerializeField, TextArea] private string[] m_textoDialogo;

        [Header("Vida UI")]
        [SerializeField] private Sprite[] m_estadoVida;
        [SerializeField] private GameObject m_objVidaJugador;
        [SerializeField] private GameObject m_objVidaEnemigo;

        [Header("Textos UI")]
        [SerializeField] private GameObject m_panelTexto;
        [SerializeField] private TMP_Text m_atacarBotonUI;
        [SerializeField] private TMP_Text m_cargarBotonUI;
        [SerializeField] private TMP_Text m_defenderBotonUI;
        [SerializeField] private TMP_Text m_leerBotonUI;
        [SerializeField] private GameObject m_trianguloTextoUI;
        [SerializeField] private TMP_Text m_textoUI;
        [SerializeField] private float m_tiempoTipeo = 0.05f;
        [SerializeField] private float m_inicioTiempoTipeo = 1f;

        private enum Accion { Ninguna = 0, Atacar = 1, Cargar = 2, Defender = 3 }
        private int m_cargasJugador;
        private int m_cargasEnemigo;
        private const int k_VidaMax = 5;
        private float m_vidaJugador = k_VidaMax;
        private float m_vidaEnemigo = k_VidaMax;
        private bool m_modoTexto;
        private bool m_modoTextoResultado;
        private bool m_esperandoSeleccion;
        private Accion m_accionJugador = Accion.Ninguna;
        private Accion m_accionEnemigo = Accion.Ninguna;
        private string m_nombreJugador = "Carol";
        private string m_nombreEnemigo;
        private SpriteRenderer m_spriteVidaJugadorRenderer;
        private SpriteRenderer m_spriteVidaEnemigoRenderer;
        /* ================================================================================================================
        ---------------------------------------------------- EVENTOS -----------------------------------------------------
        ================================================================================================================= */
        public static event Action OnLeer;

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS DE UNITY -----------------------------------------------------
        ================================================================================================================= */
        private void Awake()
        {
            m_inputPlayer = new InputSystem_Actions();
            if (m_objVidaJugador != null)
                m_spriteVidaJugadorRenderer = m_objVidaJugador.GetComponent<SpriteRenderer>();
            if (m_objVidaEnemigo != null)
                m_spriteVidaEnemigoRenderer = m_objVidaEnemigo.GetComponent<SpriteRenderer>();
        }
        
        private void OnEnable()
        {
            m_inputPlayer.Combat.Read.performed += Leer;
            m_inputPlayer.Combat.Attack.performed += Atacar;
            m_inputPlayer.Combat.Charge.performed += Cargar;
            m_inputPlayer.Combat.Defense.performed += Defenderse;
            m_inputPlayer.Combat.Enable();
            IniciadorDialogo.OnCombateIniciado += ControlDialogo;
            GeneradorCombate.OnTagEnemigo += EstablecerNombreEnemigo;
        }

        private void OnDisable()
        {
            m_inputPlayer.Combat.Read.performed -= Leer;
            m_inputPlayer.Combat.Attack.performed -= Atacar;
            m_inputPlayer.Combat.Charge.performed -= Cargar;
            m_inputPlayer.Combat.Defense.performed -= Defenderse;
            m_inputPlayer.Combat.Disable();
            IniciadorDialogo.OnCombateIniciado -= ControlDialogo;
            GeneradorCombate.OnTagEnemigo -= EstablecerNombreEnemigo;
        }

        /* ================================================================================================================
        ---------------------------------------------------- MÉTODOS -----------------------------------------------------
        ================================================================================================================= */
        private void ControlDialogo()
        {
            m_modoTexto = false;
            m_cargasJugador = 0;
            m_cargasEnemigo = 0;
            m_vidaJugador = k_VidaMax;
            m_vidaEnemigo = k_VidaMax;
            m_accionJugador = Accion.Ninguna;
            m_accionEnemigo = Accion.Ninguna;
            ActualizarImagenesVida();

            StartCoroutine(ProcesarTurno());
        }

        private void AplicarModoTexto()
        {
            m_modoTexto = true;
            m_modoTextoResultado = false;
            CambiarAlfaTextosUI(m_leerBotonUI, 1f);
            CambiarAlfaTextosUI(m_atacarBotonUI, 0.5f);
            CambiarAlfaTextosUI(m_defenderBotonUI, 0.5f);
            CambiarAlfaTextosUI(m_cargarBotonUI, m_cargasJugador == 0 ? 0.5f : 1f);
        }

        private void AplicarModoSeleccion()
        {
            m_modoTexto = false;
            m_modoTextoResultado = false;
            CambiarAlfaTextosUI(m_leerBotonUI, 0.5f);
            CambiarAlfaTextosUI(m_atacarBotonUI, m_cargasJugador > 0 ? 1f : 0.5f);
            CambiarAlfaTextosUI(m_defenderBotonUI, 1f);
            CambiarAlfaTextosUI(m_cargarBotonUI, 1f);
        }

        private void AplicarModoTextoResultado()
        {
            m_modoTexto = true;
            m_modoTextoResultado = true;
            CambiarAlfaTextosUI(m_leerBotonUI, 1f);
            CambiarAlfaTextosUI(m_atacarBotonUI, 0.5f);
            CambiarAlfaTextosUI(m_defenderBotonUI, 0.5f);
            CambiarAlfaTextosUI(m_cargarBotonUI, 0.5f);
        }

        private IEnumerator ProcesarTurno()
        {
            m_esperandoSeleccion = false;
            m_accionJugador = Accion.Ninguna;
            m_accionEnemigo = Accion.Ninguna;

            AplicarModoSeleccion();
            yield return StartCoroutine(EjecutarTexto(0, true));
            m_esperandoSeleccion = true;
        }

        private void EstablecerNombreEnemigo(string tagEnemigo)
        {
            m_nombreEnemigo = tagEnemigo;
        }

        private void CambiarAlfaTextosUI(TMP_Text texto, float alpha)
        {
            if (texto == null) return;

            Color c = texto.color;
            c.a = Mathf.Clamp01(alpha);
            texto.color = c;
        }

        private IEnumerator LeerResultadoFinal()
        {
            yield return StartCoroutine(EjecutarTexto(0, true));
            AplicarModoSeleccion();
            m_esperandoSeleccion = true;
        }

        private float ObtenerAlpha(TMP_Text texto)
        {
            return texto == null ? 0f : texto.color.a;
        }

        private bool ControladorAlpha(TMP_Text texto)
        {
            return ObtenerAlpha(texto) >= 0.99f;
        }

        private void Leer(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!ControladorAlpha(m_leerBotonUI)) return;

            if (m_modoTextoResultado)
            {
                StartCoroutine(LeerResultadoFinal());
                return;
            }

            if (m_modoTexto)
            {
                // Avanzar a modo selección después de ver texto
                AplicarModoSeleccion();
                m_esperandoSeleccion = true;
                return;
            }

            OnLeer?.Invoke();
        }

        private void Atacar(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!ControladorAlpha(m_atacarBotonUI)) return;
            if (!m_esperandoSeleccion) return;

            m_accionJugador = Accion.Atacar;
            m_cargasJugador = Mathf.Max(0, m_cargasJugador - 1);
            m_esperandoSeleccion = false;
            StartCoroutine(ResolverAccionJugador());
        }

        private void Cargar(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!ControladorAlpha(m_cargarBotonUI)) return;
            if (!m_esperandoSeleccion) return;

            m_accionJugador = Accion.Cargar;
            m_cargasJugador++;
            m_esperandoSeleccion = false;
            StartCoroutine(ResolverAccionJugador());
        }

        private void Defenderse(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!ControladorAlpha(m_defenderBotonUI)) return;
            if (!m_esperandoSeleccion) return;

            m_accionJugador = Accion.Defender;
            m_esperandoSeleccion = false;
            StartCoroutine(ResolverAccionJugador());
        }

        private IEnumerator ResolverAccionJugador()
        {
            m_accionEnemigo = ElegirAccionEnemigo();
            if (m_accionEnemigo == Accion.Cargar)
            {
                m_cargasEnemigo++;
            }
            else if (m_accionEnemigo == Accion.Atacar)
            {
                m_cargasEnemigo = Mathf.Max(0, m_cargasEnemigo - 1);
            }

            // Texto acción jugador
            yield return StartCoroutine(EjecutarTexto((int)m_accionJugador, true));

            // Texto acción enemigo
            yield return StartCoroutine(EjecutarTexto((int)m_accionEnemigo, false));

            // Texto resultado dinámico
            int indiceResultado = ObtenerIndiceResultado();
            string nombrePrimario;
            string nombreSecundario;
            ObtenerNombresResultado(indiceResultado, out nombrePrimario, out nombreSecundario);
            yield return StartCoroutine(EjecutarTexto(indiceResultado, nombrePrimario, nombreSecundario));

            ProcesarResultado();
            AplicarModoTextoResultado();
            m_esperandoSeleccion = false;
        }

        private int ObtenerIndiceResultado()
        {
            if (m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Atacar)
            {
                return 4;
            }
            if ((m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Cargar) ||
                (m_accionJugador == Accion.Cargar && m_accionEnemigo == Accion.Atacar))
            {
                return 5;
            }
            if ((m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Defender) ||
                (m_accionJugador == Accion.Defender && m_accionEnemigo == Accion.Atacar))
            {
                return 6;
            }
            if (m_accionJugador == Accion.Cargar && m_accionEnemigo == Accion.Cargar)
            {
                return 7;
            }
            if ((m_accionJugador == Accion.Cargar && m_accionEnemigo == Accion.Defender) ||
                (m_accionJugador == Accion.Defender && m_accionEnemigo == Accion.Cargar))
            {
                return 9;
            }
            if (m_accionJugador == Accion.Defender || m_accionEnemigo == Accion.Defender)
            {
                return 8;
            }

            return 0;
        }

        private void ObtenerNombresResultado(int indiceResultado, out string nombrePrimario, out string nombreSecundario)
        {
            nombrePrimario = m_nombreJugador;
            nombreSecundario = m_nombreEnemigo;

            switch (indiceResultado)
            {
                case 5:
                    if (m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Cargar)
                    {
                        nombrePrimario = m_nombreEnemigo; // enemigo sufre daño
                        nombreSecundario = m_nombreJugador;
                    }
                    else if (m_accionJugador == Accion.Cargar && m_accionEnemigo == Accion.Atacar)
                    {
                        nombrePrimario = m_nombreJugador; // jugador sufre daño
                        nombreSecundario = m_nombreEnemigo;
                    }
                    break;
                case 6:
                    if (m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Defender)
                    {
                        nombrePrimario = m_nombreEnemigo; // enemigo se defiende
                        nombreSecundario = m_nombreJugador;
                    }
                    else if (m_accionJugador == Accion.Defender && m_accionEnemigo == Accion.Atacar)
                    {
                        nombrePrimario = m_nombreJugador; // jugador se defiende
                        nombreSecundario = m_nombreEnemigo;
                    }
                    break;
                case 8:
                    if (m_accionJugador == Accion.Defender)
                    {
                        nombrePrimario = m_nombreJugador;
                        nombreSecundario = m_nombreEnemigo;
                    }
                    else if (m_accionEnemigo == Accion.Defender)
                    {
                        nombrePrimario = m_nombreEnemigo;
                        nombreSecundario = m_nombreJugador;
                    }
                    break;
                default:
                    nombrePrimario = m_nombreJugador;
                    nombreSecundario = m_nombreEnemigo;
                    break;
            }
        }

        private void ProcesarResultado()
        {
            // Ajustar cargas cuando un atacante golpea a un oponente que estaba cargando.
            if (m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Cargar)
            {
                m_cargasEnemigo = Mathf.Max(0, m_cargasEnemigo - 1);
                m_vidaEnemigo = Mathf.Max(0f, m_vidaEnemigo - 1f);
            }
            else if (m_accionJugador == Accion.Cargar && m_accionEnemigo == Accion.Atacar)
            {
                m_cargasJugador = Mathf.Max(0, m_cargasJugador - 1);
                m_vidaJugador = Mathf.Max(0f, m_vidaJugador - 1f);
            }
            else if (m_accionJugador == Accion.Atacar && m_accionEnemigo == Accion.Atacar)
            {
                m_vidaJugador = Mathf.Max(0f, m_vidaJugador - 1f);
                m_vidaEnemigo = Mathf.Max(0f, m_vidaEnemigo - 1f);
            }

            ActualizarImagenesVida();

            if (m_vidaJugador <= 0f)
            {
                string tagPerdedor = m_nombreJugador;
                string nombreGanador = m_nombreEnemigo;
                StartCoroutine(FinalizarCombate(10, 2, tagPerdedor, nombreGanador));
            }
            else if (m_vidaEnemigo <= 0f)
            {
                string tagPerdedor = m_nombreEnemigo;
                string nombreGanador = m_nombreJugador;
                StartCoroutine(FinalizarCombate(11, 0, tagPerdedor, nombreGanador));
            }
        }

        private IEnumerator FinalizarCombate(int indiceTexto, int indiceEscena, string tagPerdedor, string nombreGanador)
        {
            yield return StartCoroutine(EjecutarTexto(indiceTexto, tagPerdedor, nombreGanador));
            SceneManager.LoadScene(indiceEscena);
        }

        private void ActualizarImagenesVida()
        {
            ActualizarSpriteVida(m_vidaJugador, m_estadoVida, m_spriteVidaJugadorRenderer);
            ActualizarSpriteVida(m_vidaEnemigo, m_estadoVida, m_spriteVidaEnemigoRenderer);
        }

        private void ActualizarSpriteVida(float vidaActual, Sprite[] sprites, SpriteRenderer renderer)
        {
            if (sprites == null || sprites.Length == 0 || renderer == null)
                return;

            int indice = Mathf.Clamp(Mathf.FloorToInt(vidaActual) - 1, 0, sprites.Length - 1);
            renderer.sprite = sprites[indice];
        }

        private IEnumerator EjecutarTexto(int indice, bool esTextoJugador)
        {
            return EjecutarTexto(indice,
                esTextoJugador ? m_nombreJugador : m_nombreEnemigo,
                esTextoJugador ? m_nombreEnemigo : m_nombreJugador);
        }

        private IEnumerator EjecutarTexto(int indice, string nombrePrimario, string nombreSecundario)
        {
            m_trianguloTextoUI.SetActive(false);

            yield return new WaitForSeconds(m_inicioTiempoTipeo);

            m_textoUI.text = string.Empty;
            string textoAProcesar = ObtenerTextoConNombre(indice, nombrePrimario, nombreSecundario);

            foreach (char caracter in textoAProcesar)
            {
                m_textoUI.text += caracter;
                yield return new WaitForSeconds(m_tiempoTipeo);
            }
            m_trianguloTextoUI.SetActive(true);
        }

        private string ObtenerTextoConNombre(int indice, string nombrePrimario, string nombreSecundario)
        {
            if (indice < 0 || indice >= m_textoDialogo.Length)
            {
                return string.Empty;
            }

            string texto = m_textoDialogo[indice];
            return string.Format(texto, nombrePrimario, nombreSecundario);
        }

        private Accion ElegirAccionEnemigo()
        {
            if (m_cargasEnemigo <= 0)
            {
                return UnityEngine.Random.value < 0.5f ? Accion.Cargar : Accion.Defender;
            }

            return (Accion)UnityEngine.Random.Range(1, 4);
        }
    }
}