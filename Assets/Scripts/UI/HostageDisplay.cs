using System;
using System.Collections;
using UnityEngine;

namespace Hanabanashiku.GameJam.UI {
    [RequireComponent(typeof(AudioSource))]
    public class HostageDisplay : MonoBehaviour {
        public float ShowForSeconds = 2.7f;

        private AudioSource _audioSource;
        private HostageCounter _counter;
        private bool _firstLoop = true;

        private void Start() {
            _audioSource = GetComponent<AudioSource>();
            _counter = GameManager.Instance.gameObject.GetComponentInChildren<HostageCounter>();
        }

        private void OnEnable() {
            if(_firstLoop) {
                _firstLoop = false;
                return;
            }
            
            PlayBellChime();
            StartCoroutine(DisableAfterSeconds());
        }

        private IEnumerator DisableAfterSeconds() {
            yield return new WaitForSeconds(ShowForSeconds);
            gameObject.SetActive(false);
        }

        private void PlayBellChime() {
            var percent = (Constants.MAX_HOSTAGES - _counter.HostagesRemaining) / (float)Constants.MAX_HOSTAGES;
            _audioSource.volume = Math.Max(0.005f, percent);
            _audioSource.Play();
        }
    }
}