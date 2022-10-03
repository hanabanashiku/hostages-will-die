using System;
using System.Collections;
using Hanabanashiku.GameJam.UI;
using UnityEngine;

namespace Hanabanashiku.GameJam {
    public class HostageCounter : MonoBehaviour {
        public int HostagesRemaining = Constants.MAX_HOSTAGES;
        
        private HostageDisplay _display;
        private Coroutine _coroutine;

        public void StopCounter() {
            StopCoroutine(_coroutine);
            _display.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void Start() {
            _display = FindObjectOfType<HostageDisplay>();
            Debug.Assert(_display);
            _display.gameObject.SetActive(false);
            _coroutine = StartCoroutine(StartCounter());
        }

        private IEnumerator StartCounter() {
            while(HostagesRemaining > 0) {
                yield return new WaitForSeconds(10f);

                HostagesRemaining--;
                _display.gameObject.SetActive(true);
            }
            
            GameManager.Instance.Lose();
        }
    }
}