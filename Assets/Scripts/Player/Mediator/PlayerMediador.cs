using UnityEngine;
using Assets.Scripts.Player.Models;
using Assets.Scripts.Player.Controller;

namespace Assets.Scripts.Player.Mediator
{
    public class PlayerMediador : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        private Vector2 _inputDirection;

        private void Update()
        {
            var direccionGlobal = transform.forward * _inputDirection.y;
            var rotacionActual = transform.rotation.eulerAngles.y;
            var nuevaRotacion = PlayerController.RotacionSobreEjeY(rotacionActual, _inputDirection.x, _playerModel.VelocidadRotacion, Time.deltaTime);

            transform.position = PlayerController.DesplazamientoPlayer(transform.position, direccionGlobal, _playerModel.VelocidadPlayer, Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, nuevaRotacion, 0f);
        }

        public void InputUsuario(Vector2 direccion)
        {
            _inputDirection = direccion;
        }
    }
}