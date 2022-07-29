using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _upperBorder;
    [SerializeField] private Transform _lowerBorder;

    public float Left;
    public float Right;
    public float Upper;
    public float Lower;

    private void Start()
    {
        Left = _leftBorder.position.x;
        Right = _rightBorder.position.x;
        Upper = _upperBorder.position.y;
        Lower = _lowerBorder.position.y;
    }
}
