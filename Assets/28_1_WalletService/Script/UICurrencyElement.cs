using UnityEngine;
using UnityEngine.UI;

namespace Modul_28_1
{
    public class UICurrencyElement : MonoBehaviour
    {
        [field: SerializeField] private Text _name;
        [field: SerializeField] private Text _value;

        public void Init(string name)
        {
            _name.text = name;
        }

        public void UpdateValue(string value)
        {
            _value.text = value;
        }
    }
}