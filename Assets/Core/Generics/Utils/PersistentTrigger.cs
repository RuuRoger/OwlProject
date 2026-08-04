using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core.Generics.Utils
{
    public class PersistentTrigger : MonoBehaviour
    {
        [SerializeField] private string m_triggerId;

        private static readonly HashSet<string> s_destroyedTriggerIds = new HashSet<string>();

        public string TriggerId => m_triggerId;

        private void Awake()
        {
            if (!string.IsNullOrWhiteSpace(m_triggerId) && s_destroyedTriggerIds.Contains(m_triggerId))
            {
                Destroy(gameObject);
            }
        }

        public static void MarkDestroyed(string triggerId)
        {
            if (string.IsNullOrWhiteSpace(triggerId))
            {
                return;
            }

            s_destroyedTriggerIds.Add(triggerId);
        }
    }
}
