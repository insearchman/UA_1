using UnityEngine;
using UnityEngine.UI;

namespace Modul_25.UI
{
    [RequireComponent(typeof(Image))]
    public class SpriteToggle : MonoBehaviour
    {
        [SerializeField] private Sprite _on;
        [SerializeField] private Sprite _off;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SwitchSprite(bool isOn)
        {
            _image.sprite = isOn ? _on : _off;
        }
    }
}