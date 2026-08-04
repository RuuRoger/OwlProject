using Unity.Mathematics;
using UnityEngine;

namespace Assets.Core.Generics.Utils
{
    public class Billboarding : MonoBehaviour
    {
        [SerializeField] private Transform m_camaraTransform;
        [SerializeField] private bool m_soloEjeY = true;

        private void LateUpdate()
        {
            if (m_soloEjeY)
            {
                var direccionCamara = m_camaraTransform.forward;
                direccionCamara.y = 0f;

                if (direccionCamara.sqrMagnitude > 0.001f)
                {
                    transform.rotation = Quaternion.LookRotation(direccionCamara.normalized, Vector3.up);
                }
            }
            else
            {
                transform.rotation = m_camaraTransform.rotation;
            }
        }

    }
}