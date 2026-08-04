using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Core.Generics.Utils
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip m_originalMusic;
        [SerializeField] private AudioClip m_transitionMusic;
        [SerializeField] private bool m_loopOriginal = true;
        [SerializeField] private bool m_loopTransition = true;
        [SerializeField] [Range(0f, 1f)] private float m_volume = 1f;

        private static AudioManager s_instance;
        private AudioSource m_audioSource;

        public static AudioManager Instance => s_instance;

        private void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;
            DontDestroyOnLoad(gameObject);

            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
            m_audioSource.volume = m_volume;
            m_audioSource.loop = m_loopOriginal;

            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayOriginalMusic();
        }

        private void OnDestroy()
        {
            if (s_instance == this)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                PlayOriginalMusic();
                return;
            }

            if (scene.buildIndex == 1)
            {
                return;
            }

            StopMusic();
        }

        public void PlayOriginalMusic()
        {
            if (m_originalMusic == null)
            {
                return;
            }

            if (m_audioSource.clip == m_originalMusic && m_audioSource.isPlaying)
            {
                return;
            }

            m_audioSource.clip = m_originalMusic;
            m_audioSource.loop = m_loopOriginal;
            m_audioSource.Play();
        }

        public void PlayTransitionMusic()
        {
            if (m_transitionMusic == null)
            {
                return;
            }

            if (m_audioSource.clip == m_transitionMusic && m_audioSource.isPlaying)
            {
                return;
            }

            m_audioSource.clip = m_transitionMusic;
            m_audioSource.loop = m_loopTransition;
            m_audioSource.Play();
        }

        public void StopMusic()
        {
            if (m_audioSource == null)
            {
                return;
            }

            m_audioSource.Stop();
            m_audioSource.clip = null;
        }
    }
}
