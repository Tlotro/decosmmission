using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector]
    public GameObject LinkedRoom;
    [HideInInspector]
    public Door LinkedDoor;
    public Vector2 transferPos;
    public int cellx;
    public int celly;
    public Direction way;
    public GameObject marker;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerBase>() != null)
        {
            Door go = transform.GetComponent<Door>().LinkedDoor;
            collision.gameObject.transform.position = (Vector2)go.transform.position + (Vector2)(collision.gameObject.transform.position - transform.position) + go.transferPos;
            go.LinkedRoom.SetActive(true);
            LinkedRoom.SetActive(false);
            RoomDesign designExit = LinkedRoom.GetComponent<RoomDesign>();
            RoomDesign designEnter = go.LinkedRoom.GetComponent<RoomDesign>();
            if (designEnter.roomState == RoomState.unexplored)
            {
                designEnter.roomState = RoomState.explored;
                //FirstRoomEnter(designEnter)
                //UnclearedRoomEnter(designEnter)
            }
            else
            {
                if (designEnter.roomState == RoomState.cleared)
                {
                    //ClearedRoomEnter(designEnter)
                }
                else
                {
                    //UnclearedRoomEnter(designEnter)
                    //UnclearedRoomReEnter(designEnter)
                }
                //RoomReEnter(designEnter)
            }
            //GeneralRoomEnter(designEnter)
            if (designExit.roomState == RoomState.cleared)
            {
                //ExitClearedRoom(designExit)
            }
            else
            {
                //ExitUnclearedRoom(designExit)
            }
        }
    }

    public void Awake()
    {
        LinkedRoom = transform.parent.parent.gameObject;
        Destroy(marker);
        marker = null;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Open()
    {
        GetComponent<TilemapRenderer>().enabled = false;
        GetComponent<TilemapCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled=true;
        GetComponent<ShadowCaster2D>().enabled = false;
    }

    public void Close()
    {

    }
}
