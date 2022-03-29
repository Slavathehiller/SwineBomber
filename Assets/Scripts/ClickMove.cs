using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Player player;
    public Vector3 moveDirection;
    private bool moving;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.Rotate(moveDirection);
        moving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            player.MoveBy(player.CurrentSpeed * Time.deltaTime * moveDirection);
    }
}
