using UnityEngine;
using UnityEngine.EventSystems;

public class ClickLayBomb : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;
    public void OnPointerClick(PointerEventData eventData)
    {
        _player.LayBomb();
    }
}
