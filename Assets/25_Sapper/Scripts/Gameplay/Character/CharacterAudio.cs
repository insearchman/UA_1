using UnityEngine;

namespace Modul_25.Gameplay
{
    public class CharacterAudio
    {
        private AudioSource _audioSource;
        private AudioClip[] _footstepClips;

        private int _lastPlayedIndex = -1;
        private float _stepDelay;
        private float _delayTime;

        public CharacterAudio(AudioSource audioSource, AudioClip[] footstepClips, float stepDelay)
        {
            _audioSource = audioSource;
            _footstepClips = footstepClips;
            _stepDelay = stepDelay;
        }

        public void PlayRandomFootstep()
        {
            if (_audioSource.isPlaying)
                return;

            if (_footstepClips.Length == 0)
                return;

            _delayTime += Time.deltaTime;

            if (_delayTime >= _stepDelay)
            {
                int randomIndex;

                do
                {
                    randomIndex = Random.Range(0, _footstepClips.Length);
                }
                while (randomIndex == _lastPlayedIndex);

                _audioSource.PlayOneShot(_footstepClips[randomIndex]);
                _lastPlayedIndex = randomIndex;

                _delayTime = 0;
            }
        }
    }
}