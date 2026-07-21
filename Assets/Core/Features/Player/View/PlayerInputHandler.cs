using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Core.Features.Player.Mediator;

namespace Assets.Scripts.Player.View
{
    [RequireComponent(typeof(PlayerMediador))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMediador _playerMediador;
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _playerMediador = GetComponent<PlayerMediador>();
        }

        private void OnEnable()
        {
            _inputActions.Player.Move.performed += Moverse;
            _inputActions.Player.Move.canceled += PararMovimiento;
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= Moverse;
            _inputActions.Player.Move.canceled -= PararMovimiento;
            _inputActions.Player.Disable();
        }

        private void Moverse(InputAction.CallbackContext contexto)
        {
            _playerMediador.InputUsuario(contexto.ReadValue<Vector2>());
        }

        private void PararMovimiento(InputAction.CallbackContext _)
        {
            _playerMediador.InputUsuario(Vector2.zero);
        }
    }
}