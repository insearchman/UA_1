using UnityEngine;

namespace Modul_0
{
    public class ForTest : MonoBehaviour
    {
        public RObject RObject = new();
        public RList<int> RList = new();
        public RValue<int> RValue = new();



        public void TestMethod()
        {
            // Проверка RList
            // RObject.ListInt.ElementsBadVariant.Clear(); // плохо, есть доступ с листу
            // RObject.ListInt.ReadOnlyElements.Clear(); // защита на уровне RList
            // RObject.ListInt.EnumerableElements.Clear(); // заботиться о защите в RObject не нужно

            // RList.ElementsBadVariant.Clear(); // плохо, есть доступ к листу
            // RList.ReadOnlyElements.Clear();
            // RList.EnumerableElements.Clear();

            // Проверка RValue
            // RObject.ValueIntBadVariant.Value = 1; // плохо, так как RValue не содержит никакой защиты сама по себе
            // RObject.ValueInt.Value = 0; // извне изменить RValue нельзя, только через методы использующего её класса




        }

    }
}