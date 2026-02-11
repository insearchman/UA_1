using UnityEngine;

namespace Modul_0
{
    public class RObject : MonoBehaviour
    {
        private RValue<int> _max;
        private RValue<int> _current;
        private RList<int> _listInt;
        
        public RObject()
        {
            _current = new RValue<int>();
            _listInt = new RList<int>();
        }


        // Плохой вариант
        public RValue<int> ValueIntBadVariant => _current;

        // Интерфейс позволяет передать отсюда переменную защищённо, при этом позволяет подписаться на ее ивенты
        public IReadOnlyRValue<int> ValueInt => _current;


        // для листа интерфейс избыточен, так как защита листа внутри RList.
        public RList<int> ListInt => _listInt;


        // пример защищённой работы с RValue
        public void Add(int value)
        {
            if (value < 0)
                return;

            _current.Value = Mathf.Clamp(_current.Value + value, 0 , _max.Value);
        }

        public void Reduce(int value)
        {
            if (value < 0)
                return;

            _current.Value = Mathf.Clamp(_current.Value - value, 0, _max.Value);
        }
    }
}