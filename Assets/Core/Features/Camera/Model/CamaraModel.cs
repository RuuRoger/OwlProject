using UnityEngine;

namespace Assets.Core.Features.Camera.Model
{
    [CreateAssetMenu(fileName ="CamaraData", menuName = "OwlProject/CamaraModel")]
    public class CamaraModel : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Distancia de la cámara respecto al player")]
        private Vector3 _offset = new Vector3(0f, 5f, -5f);
        public Vector3 Offset => _offset;
    }
}