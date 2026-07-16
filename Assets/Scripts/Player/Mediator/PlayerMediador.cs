using UnityEngine;
using Assets.Scripts.Player.Models;
using Assets.Scripts.Player.Controller;
using Assets.Scripts.Player.View;

namespace Assets.Scripts.Player.Mediator
{
    public class PlayerMediador : MonoBehaviour
    {
        [Header("Configuración y Componentes")]
        [Space(15)]
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerAnimationsView _playerAnimationsView;

        private PlayerAnimationModel _enumEstado;
        private Vector2 _inputDirection;

        private void Awake()
        {
            _enumEstado = new PlayerAnimationModel();
        }

        private void Update()
        {
            // Movimiento
            Vector3 direccionGlobal = transform.forward * _inputDirection.y;
            float rotacionActual = transform.rotation.eulerAngles.y;
            float nuevaRotacion = PlayerController.RotacionSobreEjeY(rotacionActual, _inputDirection.x, _playerModel.VelocidadRotacion, Time.deltaTime);

            transform.position = PlayerController.DesplazamientoPlayer(transform.position, direccionGlobal, _playerModel.VelocidadPlayer, Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, nuevaRotacion, 0f);

            // Animaciones
            ActualizarEstadoAnimacion();
        }

        /// <summary>
        ///    Genera un estado según si el input Y se ha pulsado. Ese estado, lo envía para actualziar la animación del player
        /// </summary>
        private void ActualizarEstadoAnimacion()
        {
            bool seEstaMoviendo = _inputDirection.y != 0f;

            PlayerAnimationModel.EstadosSinBatalla nuevoEstado = seEstaMoviendo 
                ? PlayerAnimationModel.EstadosSinBatalla.Walk 
                : PlayerAnimationModel.EstadosSinBatalla.Idle;

            if (_enumEstado.EstadoActual != nuevoEstado)
            {
                _enumEstado.EstadoActual = nuevoEstado;
                _playerAnimationsView.ActualizarAniamciones(nuevoEstado);
            }
        }

        public void InputUsuario(Vector2 direccion)
        {
            _inputDirection = direccion;
        }
    }
}