using UnityEngine;

public class GeneratorSandbox : MonoBehaviour
{
    [SerializeField] 
    private Generator generator;

    [SerializeField] 
    private MapVisualiser visualiser;

    [SerializeField] 
    private int roomCount;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            generator.Generate(roomCount);
            visualiser.Visualise(generator.Map, generator.MapSize);
        }
    }
}