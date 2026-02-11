using System;
using System.Collections.Generic;

namespace Modul_0
{
    public class RList<T>
    {
        public event Action<T> Added;
        public event Action<T> Removed;
        public event Action Cleared;

        private List<T> _elements;


        public RList()
        {
            _elements = new();
        }

        // IEnumerable позволяет передвать сюда много всего, что потом будет храниться в виде листа
        public RList(IEnumerable<T> toList)
        {
            // такая конструкция защищает от изменения передаваемого параметра извне.
            _elements = new List<T>(toList);
        }


        // Плохой незащищенный вариант
        public List<T> ElementsBadVariant => _elements;

        // Ограничивает ссылочный тип. Можно только посмотреть, перебрать, обратиться по индексу
        public IReadOnlyList<T> ReadOnlyElements => _elements;

        // Тоже что и IReadOnlyList, но более ограниченый - нельзя обратиться по индексу
        public IEnumerable<T> EnumerableElements => _elements;


        public virtual void Add(T element)
        {
            _elements.Add(element);
            Added?.Invoke(element);
        }

        public virtual void Remove(T element)
        {
            _elements.Remove(element);
            Removed?.Invoke(element);
        }

        public virtual void Clear()
        {
            _elements.Clear();
            Cleared?.Invoke();
        }
    }
}