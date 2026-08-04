using UnityEngine;
using Assets.Core.Features.Camera.Controller;
using Assets.Core.Features.Camera.Model;

namespace Assets.Core.Features.Camera.Mediator
{
    public class CamaraMediador : MonoBehaviour
    {
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private CamaraModel m_camaraModel;

        private void LateUpdate()
        {
            Vector3 nuevaPosicion = CameraController.CalcularPosicion(m_playerTransform.position, m_playerTransform.rotation, m_camaraModel.Offset);
            Quaternion nuevaRotacion = CameraController.CalcularRotacion(nuevaPosicion, m_playerTransform.position);

            this.transform.position = nuevaPosicion;
            this.transform.rotation = nuevaRotacion;
        }
    }
}
