using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private Transform _upBorder;
    [SerializeField] private Transform _downBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _leftBoreder;

    private int _borderOffset = 5;

    void Start()
    {
        _upBorder.transform.position = new Vector3(0, _borderOffset, 0);
        _downBorder.transform.position = new Vector3(0, -_borderOffset, 0);
        _rightBorder.transform.position = new Vector3(_borderOffset, 0, 0);
        _leftBoreder.transform.position = new Vector3(-_borderOffset, 0, 0);
    }

    public bool IsHitWith(Ball ball)
    {
        return ball.transform.position.y > _upBorder.transform.position.y ||
            ball.transform.position.y < _downBorder.transform.position.y ||
            ball.transform.position.x > _rightBorder.transform.position.x ||
            ball.transform.position.x < _leftBoreder.transform.position.x;
    }
}