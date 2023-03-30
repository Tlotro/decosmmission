using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject LinkedRoom;
    public Door LinkedDoor;
    public int cellx;
    public int celly;
    public Direction way;
    public void Open()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }

    public void Close()
    {

    }
}
