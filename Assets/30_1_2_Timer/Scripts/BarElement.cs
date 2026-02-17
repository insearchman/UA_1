using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarElement : MonoBehaviour
{
    [SerializeField] private Sprite _filled;
    [SerializeField] private Sprite _empty;

    private Image _image;

    public bool IsFilled { get; private set; }

    private void Awake()
    {
        _image = GetComponent<Image>();

        SwitchSprite(true);
    }

    public void SwitchSprite(bool isFilled)
    {
        _image.sprite = isFilled ? _filled : _empty;
    }
}
