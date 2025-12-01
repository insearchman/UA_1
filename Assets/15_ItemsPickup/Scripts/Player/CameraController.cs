using UnityEngine;

public class CameraController
{
    private Camera _mainCamera;
    private Vector3 _cameraOffset;

    public void InitCamera(Vector3 target)
    {
        _mainCamera = Camera.main;
        _cameraOffset = _mainCamera.transform.position - target;
    }

    public void Foloww(Vector3 target)
    {
        _mainCamera.transform.position = target + _cameraOffset;
    }
}
