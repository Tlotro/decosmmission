using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
            Stopwatch stopwatch = Stopwatch.StartNew();
            generator.Generate(roomCount);
            stopwatch.Stop();
            Debug.Log($"Generation took: {stopwatch.ElapsedMilliseconds}ms");
            //visualiser.Visualise(generator.Map, generator.MapSize);
        }
    }
}