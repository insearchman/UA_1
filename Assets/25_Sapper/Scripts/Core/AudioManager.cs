using Modul_25.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Modul_25.Core
{
    public class AudioManager : MonoBehaviour
    {

        private const float OFF_VOLUME_VALUE = -80f;
        private const float ON_VOLUME_VALUE = 0f;

        private const string MUSIC_KEY = "MusicVolume";
        private const string SOUND_KEY = "SoundVolume";

        private const float EPSILON = 0.01F;

        [SerializeField] private AudioMixer _audioMixer;

        [Header("Music")]
        [SerializeField] private bool _isMusicsOn = true;
        [SerializeField] private SpriteToggle _musicButtonView;

        [Header("Sound")]
        [SerializeField] private bool _isSoundsOn = true;
        [SerializeField] private SpriteToggle _soundButtonView;


        private void Start()
        {
            AudioGroupToggle(MUSIC_KEY, _musicButtonView, _isMusicsOn);
            AudioGroupToggle(SOUND_KEY, _soundButtonView, _isSoundsOn);
        }

        public void MusicToggle()
        {
            _isMusicsOn = !_isMusicsOn;

            AudioGroupToggle(MUSIC_KEY, _musicButtonView, _isMusicsOn);
        }

        public void SoundToggle()
        {
            _isSoundsOn = !_isSoundsOn;

            AudioGroupToggle(SOUND_KEY, _soundButtonView, _isSoundsOn);
        }

        private void AudioGroupToggle(string key, SpriteToggle buttonView, bool isOn)
        {
            buttonView.SwitchSprite(isOn);

            if(isOn)
                _audioMixer.SetFloat(key, ON_VOLUME_VALUE);
            else
                _audioMixer.SetFloat(key, OFF_VOLUME_VALUE);
        }

        public bool IsMusicOn() => IsVolumeOn(MUSIC_KEY);
        public bool IsSoundOn() => IsVolumeOn(SOUND_KEY);
        private bool IsVolumeOn(string key) => _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - ON_VOLUME_VALUE) <= EPSILON;
    }
}