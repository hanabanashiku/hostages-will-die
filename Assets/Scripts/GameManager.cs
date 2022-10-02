using System;
using Hanabanashiku.GameJam.Models.Enums;
using UnityEngine;

namespace Hanabanashiku.GameJam {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        
        public GameDifficulty GameDifficulty = GameDifficulty.Hard;
        
        public void Start() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Lose() {
            // TODO
            // Record loss
            // Show loss modal
            // Reload scene
            // Move to last checkpoint
            throw new NotImplementedException();
        }
    }
}