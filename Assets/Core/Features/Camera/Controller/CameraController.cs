using UnityEngine;

namespace Assets.Core.Features.Camera.Controller
{
    public class CameraController
    {
        public static Vector3 CalcularPosicion(Vector3 posicionPLayer, Quaternion rotacionPlayer, Vector3 offset)
        {
            Vector3 offsetRotacion = rotacionPlayer * offset;

            return posicionPLayer + offsetRotacion;
        }

        public static Quaternion CalcularRotacion(Vector3 posicionCamara, Vector3 posicionPlayer)
        {
            Vector3 direccion = posicionPlayer - posicionCamara;

            if (direccion == Vector3.zero)
            {
                return Quaternion.identity;
            }

            return Quaternion.LookRotation(direccion);
        }
    }
}