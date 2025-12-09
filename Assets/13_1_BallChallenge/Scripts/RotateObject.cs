using UnityEngine;

namespace Modul_13_1
{
    public class RotateObject : MonoBehaviour
    {
        private const float RotateSpeed = 50;

        private void Update()
        {
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
        }
    }
}