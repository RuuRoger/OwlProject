using UnityEngine;
using Assets.Scripts.Camera.Controller;
using Assets.Scripts.Camera.Model;

namespace Assets.Scripts.Camera.Mediator
{
    public class CamaraMediador : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private CamaraModel _camaraModel;

        private void LateUpdate()
        {
            Vector3 nuevaPosicion = CameraController.CalcularPosicion(_playerTransform.position, _playerTransform.rotation, _camaraModel.Offset);
            Quaternion nuevaRotacion = CameraController.CalcularRotacion(nuevaPosicion, _playerTransform.position);

            this.transform.position = nuevaPosicion;
            this.transform.rotation = nuevaRotacion;
        }
    }
}
