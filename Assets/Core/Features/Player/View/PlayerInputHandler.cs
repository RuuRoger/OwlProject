using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Core.Features.Player.Mediator;

namespace Assets.Core.Features.Player.View
{
    [RequireComponent(typeof(PlayerMediador))]
    public class PlayerInputHandler : MonoBehaviour
    {
        /* ================================================================================================================
        ---------------------------------------------------- CAMPOS -----------------------------------------------------
        ================================================================================================================= */
        private PlayerMediador m_playerMediador;
        private InputSystem_Actions m_inputActions;

        private void Awake()
        {
            m_inputActions = new InputSystem_Actions();
            m_playerMediador = GetComponent<PlayerMediador>();
        }

        private void OnEnable()
        {
            m_inputActions.Player.Move.performed += Moverse;
            m_inputActions.Player.Move.canceled += PararMovimiento;
            m_inputActions.Combat.Attack.performed += Atacar;
            m_inputActions.Combat.Charge.performed += Cargar;
            m_inputActions.Combat.Defense.performed += Defernderse;
            m_inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Player.Move.performed -= Moverse;
            m_inputActions.Player.Move.canceled -= PararMovimiento;
            m_inputActions.Combat.Attack.performed -= Atacar;
            m_inputActions.Combat.Charge.performed -= Cargar;
            m_inputActions.Combat.Defense.performed -= Defernderse;
            m_inputActions.Player.Disable();
        }

        private void ModoActual(bool enCombate)
        {
            if (enCombate)
            {
                ActivarModoCombate();
            }
            else
            {
                ActivarModoExploracion();                
            }
        }

        private void ActivarModoCombate()
        {
            m_inputActions.Player.Disable();
            m_inputActions.Combat.Enable();
        }

        private void ActivarModoExploracion()
        {
            m_inputActions.Player.Enable();
            m_inputActions.Combat.Disable();
        }

        private void Moverse(InputAction.CallbackContext contexto)
        {
            m_playerMediador.InputUsuario(contexto.ReadValue<Vector2>());
        }

        private void PararMovimiento(InputAction.CallbackContext _)
        {
            m_playerMediador.InputUsuario(Vector2.zero);
        }

        private void Atacar(InputAction.CallbackContext contexto)
        {
            
        }

        private void Cargar(InputAction.CallbackContext contexto)
        {
            
        }

        private void Defernderse(InputAction.CallbackContext contexto)
        {
            
        }
    }
}