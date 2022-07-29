using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _moveDirection;
    private bool _moving;

    public void OnPointerDown(PointerEventData eventData)
    {
        _player.Rotate(_moveDirection);
        _moving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _moving = false;
    }

    void Update()
    {
        if (_moving)
            _player.TryMoveTo(_moveDirection);
    }
}
