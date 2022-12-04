using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ares : MonoBehaviour
{
    List<GameObject> entities = new List<GameObject>();
    public List<GameObject> Prefabs = new List<GameObject>();
    public List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    TMPro.TMP_Text text;
    int wave = 0;
    // Start is called before the first frame update
    void Start()
    {
        Prefabs.Add(Resources.Load<GameObject>("Units/UnitTestPrefab"));
        Prefabs.Add(Resources.Load<GameObject>("Units/UnitTestPrefab2"));
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnWave()
    {
        foreach(var pos in positions)
        {
            if(Random.Range(0, 3) < 1)
            {
                entities.Add(Instantiate(Prefabs[Random.Range(0, 2)], pos, new Quaternion()));
                entities[entities.Count - 1].GetComponent<BaseEntity>().DeathDelegate += Remove;
            }
        }
        if (entities.Count == 0)
        {
            SpawnWave();
        }
        else
        {
            wave++;
            text.text = wave.ToString();
        }
    }

    void Remove(BaseEntity unit)
    {
        entities.Remove(unit.gameObject);
        if (entities.Count == 0)
            SpawnWave();
    }
}
