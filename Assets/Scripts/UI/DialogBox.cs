using Hanabanashiku.GameJam.Database.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hanabanashiku.GameJam.UI {
    public class DialogBox : MonoBehaviour {
        public VoiceLine[] VoiceLines { get; set; }

        public TextMeshProUGUI Nameplate;
        public TextMeshProUGUI DialogText;

        private int _currentIndex;
        private InputControls _input;
        private VoiceLine CurrentLine => VoiceLines[_currentIndex];

        private void Start() {
            Time.timeScale = 0f;
            _input = new InputControls();
            _input.UI.NextDialog.Enable();
            _input.UI.NextDialog.started += OnNextDialog;

            var rect = GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 840);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 196);
            rect.anchoredPosition = new Vector2(0, 40);
            
            RenderLine();
        }

        private void OnDestroy() {
            Time.timeScale = 1f;
            _input.UI.NextDialog.started -= OnNextDialog;
        }

        private void OnNextDialog(InputAction.CallbackContext context) {
            if(VoiceLines.Length == _currentIndex + 1) {
                Destroy(gameObject);
                return;
            }
            
            _currentIndex++;
            RenderLine();
        }

        private void RenderLine() {
            var line = CurrentLine;
            Nameplate.text = line.CharacterName;
            DialogText.text = line.Line;
        }
    }
}