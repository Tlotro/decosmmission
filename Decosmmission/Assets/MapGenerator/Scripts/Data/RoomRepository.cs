using UnityEngine;

public class RoomRepository : MonoBehaviour
{
    [SerializeField] private RoomDesign[] designs;

    public int Count => designs.Length;

    public Room GetRandomRoom()
    {
        return designs[Random.Range(0, designs.Length)].ToRoom();
    }
}