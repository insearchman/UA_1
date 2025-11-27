using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}
