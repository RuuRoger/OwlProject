using Unity.Mathematics;
using UnityEngine;

namespace Assets.Core.Generics.Utils
{
    public class Billboarding : MonoBehaviour
    {
        [SerializeField] private Transform _camaraTransform;
        [SerializeField] private bool _soloEjeY = true;

        private void LateUpdate()
        {
            if (_soloEjeY)
            {
                var direccionCamara = _camaraTransform.forward;
                direccionCamara.y = 0f;

                if (direccionCamara.sqrMagnitude > 0.001f)
                {
                    transform.rotation = Quaternion.LookRotation(direccionCamara.normalized, Vector3.up);
                }
            }
            else
            {
                transform.rotation = _camaraTransform.rotation;
            }
        }

    }
}