using UnityEngine;
using System.Collections.Generic;

public class SpawnFriendlyUnity : MonoBehaviour
{
    [SerializeField] private GameObject[] units;
    [SerializeField] private Transform spawnLoc;

    private Dictionary<string, GameObject> unitDict = new Dictionary<string, GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(GameObject gObject in units){
            unitDict[gObject.name] = gObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit(string unitName)
    {
        Instantiate(unitDict[unitName], spawnLoc.position, Quaternion.identity);
    }
}
