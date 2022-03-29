using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickLayBomb : MonoBehaviour, IPointerClickHandler
{
    public Player player;

    public void OnPointerClick(PointerEventData eventData)
    {
        player.LayBomb();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
