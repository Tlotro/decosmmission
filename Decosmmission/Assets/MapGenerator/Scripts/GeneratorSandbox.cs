using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GeneratorSandbox : MonoBehaviour
{
    [SerializeField] 
    private Generator generator;

    [SerializeField] 
    private int roomCount;
    private bool generated = false;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !generated)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            generator.Generate(roomCount, Resources.Load<GameObject>("Units/PlayerTestPrefab"));
            stopwatch.Stop();
            Debug.Log($"Generation took: {stopwatch.ElapsedMilliseconds}ms");
            //visualiser.Visualise(generator.Map, generator.MapSize);
            generated = true;
        }
    }
}