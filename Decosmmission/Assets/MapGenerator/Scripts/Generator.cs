using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public string[] BaseFactions;
    public Dictionary<string, (int,int)> SpecialRooms;
    List<(int, int, Direction)> doorQueue = new List<(int, int, Direction)>();
    Dictionary<(int,int, Direction), Door> doorMap = new Dictionary<(int,int, Direction), Door>();
    public RoomDesign[] RoomsPrefabs;


    public int MapSizex { get; private set; }
    public int MapSizey { get; private set; }
    //TODO
    public int[,] EntropyMap;
    public CellDesign[,] Map;
    //minimap has to be manually drawn and somehow placed with this

    /// <summary>
    /// returns the amount of doors that will connect to a room if placed in this place.
    /// Use the tile by tile algorythm outside of this for a room with multiple of the same door
    /// </summary>
    /// <returns></returns>
    int CheckRoomPlacement(RoomDesign roomDesign, int xroom, int yroom, int xmap, int ymap, int zmap)
    {
        int res = 0;
        RoomDesign des = roomDesign;
        xmap -= xroom; ymap -= yroom;
        foreach (var a in des.Design)
        {
            int t = xmap;
            foreach (var b in a.cells)
            {
                if (b!= null)
                {
                    if (Map[t, ymap] != null)
                        return 0;
                    if (b.West && Map[t - 1, ymap] != null && Map[t - 1, ymap].East)
                        res++;
                    if (b.East && Map[t + 1, ymap] != null && Map[t + 1, ymap].West)
                        res++;
                    if (b.North && Map[t, ymap - 1] != null && Map[t, ymap - 1].South)
                        res++;
                    if (b.South && Map[t, ymap - 1] != null && Map[t, ymap - 1].North)
                        res++;
                }
                ++t;
            }
            ++ymap;
        }
        return res;
    }

    /// <summary>
    /// Places a room so that coordinates of a cell int the room correspond to the coordinates of the cell on the map
    /// </summary>
    /// <param name="room"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    RoomDesign PlaceRoom(RoomDesign roomDesign, int xroom, int yroom, int xmap, int ymap, int zmap)
    {
        RoomDesign des = roomDesign;
        xmap -= xroom; ymap -= yroom;
        RoomDesign room = Instantiate(roomDesign.transform,new Vector2(xmap*CellDesign.TilePerCell,-ymap*CellDesign.TilePerCell), new Quaternion()).GetComponent<RoomDesign>();

        yroom = 0;
        foreach (var a in des.Design)
        {
            xroom = 0;
            foreach (var b in a.cells)
            {
                Map[xmap + xroom, ymap + yroom] = b;
                if (Map[xmap + xroom, ymap + yroom] != null)
                {
                    if (Map[xmap + xroom, ymap + yroom].West)
                    {
                        if (Map[xmap + xroom - 1, ymap + yroom] == null)
                        {
                            doorQueue.Add((xmap + xroom - 1, ymap + yroom, Direction.east));
                            if (room.doors.Any(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.west))
                                doorMap.Add((xmap + xroom, ymap + yroom, Direction.west), room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.west));
                        }
                        else if (Map[xmap + xroom - 1, ymap + yroom].East)
                        {
                            doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.west));
                            Door t = room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.west);
                            t.Open();
                            doorMap[(xmap + xroom - 1, ymap + yroom, Direction.east)].Open();
                            t.LinkedDoor = doorMap[(xmap + xroom - 1, ymap + yroom, Direction.east)];
                            doorMap[(xmap + xroom - 1, ymap + yroom, Direction.east)].LinkedDoor = t;
                            doorMap.Remove((xmap + xroom - 1, ymap + yroom, Direction.east));
                        }
                    }
                    else if (Map[xmap + xroom - 1, ymap + yroom] != null && Map[xmap + xroom - 1, ymap + yroom].East)
                    {
                        doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.west));
                        doorMap.Remove((xmap + xroom - 1, ymap + yroom, Direction.east));
                    }//-------------------------------------------------------------------------------------------
                    if (Map[xmap + xroom, ymap + yroom].East)
                    {
                        if (Map[xmap + xroom + 1, ymap + yroom] == null)
                        {
                            doorQueue.Add((xmap + xroom + 1, ymap + yroom, Direction.west));
                            if (room.doors.Any(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.east))
                                doorMap.Add((xmap + xroom, ymap + yroom, Direction.east), room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.east));
                        }
                        else if (Map[xmap + xroom + 1, ymap + yroom].West)
                        {
                            doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.east));
                            Door t = room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.east);
                            t.Open();
                            doorMap[(xmap + xroom + 1, ymap + yroom, Direction.west)].Open();
                            t.LinkedDoor = doorMap[(xmap + xroom + 1, ymap + yroom, Direction.west)];
                            doorMap[(xmap + xroom + 1, ymap + yroom, Direction.west)].LinkedDoor = t;
                            doorMap.Remove((xmap + xroom + 1, ymap + yroom, Direction.west));
                        }
                    }
                    else if (Map[xmap + xroom + 1, ymap + yroom] != null && Map[xmap + xroom + 1, ymap + yroom].West)
                    {
                        doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.east));
                        doorMap.Remove((xmap + xroom + 1, ymap + yroom, Direction.west));
                    }//-------------------------------------------------------------------------------------------
                    if (Map[xmap + xroom, ymap + yroom].North)
                    {
                        if (Map[xmap + xroom, ymap + yroom - 1] == null)
                        {
                            doorQueue.Add((xmap + xroom, ymap + yroom - 1, Direction.south));
                            if (room.doors.Any(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.north))
                                doorMap.Add((xmap + xroom, ymap + yroom, Direction.north), room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.north));
                        }
                        else if (Map[xmap + xroom, ymap + yroom - 1].South)
                        {
                            doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.north));
                            Door t = room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.north);
                            t.Open();
                            doorMap[(xmap + xroom, ymap + yroom - 1, Direction.south)].Open();
                            t.LinkedDoor = doorMap[(xmap + xroom, ymap + yroom - 1, Direction.south)];
                            doorMap[(xmap + xroom, ymap + yroom - 1, Direction.south)].LinkedDoor = t;
                            doorMap.Remove((xmap + xroom, ymap + yroom - 1, Direction.south));
                        }
                    }
                    else if (Map[xmap + xroom + 1, ymap + yroom] != null && Map[xmap + xroom + 1, ymap + yroom].West)
                    {
                        doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.north));
                        doorMap.Remove((xmap + xroom, ymap + yroom - 1, Direction.south));
                    }//-------------------------------------------------------------------------------------------
                    if (Map[xmap + xroom, ymap + yroom].South)
                    {
                        if (Map[xmap + xroom, ymap + yroom + 1] == null)
                        {
                            doorQueue.Add((xmap + xroom, ymap + yroom + 1, Direction.north));
                            if (room.doors.Any(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.south))
                                doorMap.Add((xmap + xroom, ymap + yroom, Direction.south), room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.south));
                        }
                        else if (Map[xmap + xroom, ymap + yroom + 1].North)
                        {
                            doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.south));
                            Door t = room.doors.First(x => x.cellx == xroom && x.celly == yroom && x.way == Direction.south);
                            t.Open();
                            doorMap[(xmap + xroom, ymap + yroom + 1, Direction.north)].Open();
                            t.LinkedDoor = doorMap[(xmap + xroom, ymap + yroom + 1, Direction.north)];
                            doorMap[(xmap + xroom, ymap + yroom + 1, Direction.north)].LinkedDoor = t;
                            doorMap.Remove((xmap + xroom, ymap + yroom + 1, Direction.north));
                        }
                    }
                    else if (Map[xmap + xroom + 1, ymap + yroom] != null && Map[xmap + xroom + 1, ymap + yroom].West)
                    {
                        doorQueue.Remove((xmap + xroom, ymap + yroom, Direction.south));
                        doorMap.Remove((xmap + xroom, ymap + yroom + 1, Direction.north));
                    }//-------------------------------------------------------------------------------------------

                }
                xroom++;
            }
            yroom++;
        }
        return room;
    }

    private void LoadRoomPrefabs(params string[] GenerationModifierTags)
    {
        BaseFactions = new string[]{"Military" };
        IEnumerable<RoomDesign> prefabsBase = Resources.LoadAll<GameObject>("").Where(x => x.GetComponent<RoomDesign>() != null).Select(x=>x.GetComponent<RoomDesign>()).Where(x=>BaseFactions.Any(y=>y.Equals(x.Faction)));
        //Add all the code for GenerationModifier handling here, idk, you can remove specific room designs or add specific tags for adding them
        if (GenerationModifierTags.Contains("NoUniversal"))
            prefabsBase = prefabsBase.Where(x => x.Faction != "Universal");
        RoomsPrefabs = prefabsBase.ToArray();
    }
    public void Generate(int roomCount, GameObject player, params string[] GenerationModifierTags)
    {
        LoadRoomPrefabs(GenerationModifierTags);
        IEnumerable<RoomDesign> startingRooms = RoomsPrefabs.Where(x => x is StartRoomDesign);
        MapSizex = roomCount * RoomsPrefabs.Max(x => x.Design.Max(x=>x.cells.Length));
        MapSizey = roomCount * RoomsPrefabs.Max(x => x.Design.Length);
        Map = new CellDesign[MapSizex, MapSizey];
        int xb = MapSizex / 2;
        int yb = MapSizey / 2;

        var startingRoom = startingRooms.ToArray()[Random.Range(0,startingRooms.Count())];

        RoomDesign startroom = PlaceRoom(startingRoom, 0, 0,xb,yb,0);
        Instantiate(player, ((StartRoomDesign)startroom).SpawnPosition + (Vector2)startroom.transform.position,new Quaternion());

        while (doorQueue.Count() > 0 && roomCount>0)
        {
            int x, y;
            Direction direction;
            (x, y, direction) = doorQueue[0];
            List<(int,int,RoomDesign)> WeightedRooms = new List<(int, int, RoomDesign)>();
            foreach (RoomDesign room in RoomsPrefabs)
            {
                for (int i = 0; i < room.Design.Length; i++)
                {
                    for (int j = 0; j < room.Design[i].cells.Length; j++)
                    {
                        if (room.Design[i].cells[j] != null && room.Design[i].cells[j].HasDirection(direction))
                        {
                            int doorcount = CheckRoomPlacement(room, j, i, x, y, 0);
                            if (doorcount > 0)
                            {
                                for (int n = 0; n < Mathf.Min(doorcount,2); n++)
                                WeightedRooms.Add((j,i,room));
                            }
                        }
                    }
                }
            }
            if (WeightedRooms.Count > 0)
            {
                (int xroom,int yroom,RoomDesign room) = WeightedRooms[Random.Range(0, WeightedRooms.Count)];
                PlaceRoom(room,xroom,yroom,x,y,0).gameObject.SetActive(false);
                roomCount--;
            }
            else
            {
                //TODO DisableDoor(Map[x,y],Direction.North);
            }
            doorQueue.RemoveAt(0);
        }
        //spawn player
    }
}