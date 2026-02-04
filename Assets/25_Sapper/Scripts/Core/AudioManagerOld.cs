using Modul_25.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Modul_25.Core
{
    public class AudioManagerOld : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private bool _isMusicsOn = true;
        [SerializeField] private List<AudioSource> _musics;
        [SerializeField] private SpriteToggle _musicButtonView;

        [Header("Sound")]
        [SerializeField] private bool _isSoundsOn = true;
        [SerializeField] private List<AudioSource> _sounds;
        [SerializeField] private SpriteToggle _soundButtonView;

        private void Start()
        {
            AudioToggle(_musics, _musicButtonView, _isMusicsOn);
            AudioToggle(_sounds, _soundButtonView, _isSoundsOn);
        }

        public void MusicToggle()
        {
            _isMusicsOn = !_isMusicsOn;

            AudioToggle(_musics, _musicButtonView, _isMusicsOn);
        }

        public void SoundToggle()
        {
            _isSoundsOn = !_isSoundsOn;

            AudioToggle(_sounds, _soundButtonView, _isSoundsOn);
        }

        private void AudioToggle(List<AudioSource> audioSource, SpriteToggle buttonView, bool isOn)
        {
            buttonView.SwitchSprite(isOn);

            for (int i = 0; i < audioSource.Count; i++)
            {
                if (audioSource[i] != null)
                    audioSource[i].mute = !isOn;
                else
                    audioSource.Remove(audioSource[i]);
            }
        }
    }
}