using System;
using System.Collections;
using UnityEngine;

namespace Hanabanashiku.GameJam.Helpers {
    public static class AudioHelper {
        public static IEnumerator LoopClip(this AudioSource source, AudioClip clip, int times, float volume = 1.0f) {
            if(times <= 0) {
                throw new ArgumentOutOfRangeException();
            }
            if(volume is < 0f or > 1f) {
                throw new ArgumentOutOfRangeException();
            }

            var timesPlayed = 0;

            do {
                if(!source.isPlaying) {
                    source.PlayOneShot(clip, volume);
                    timesPlayed++;
                }

                yield return new WaitForFixedUpdate();
            } while(timesPlayed < times);
        }
    }
}