using UnityEngine;
using System.Collections.Generic;

public class TowerMenuManager : MonoBehaviour
{
    //[SerializeField] GameObject groundTowers;
    //[SerializeField] GameObject airTowers;
    [SerializeField] GameObject[] towerSlots;
    [SerializeField] Tower[] towerScriptableObjects;

    private Dictionary<GameObject, TowerTypes> holderTypeDict;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static event System.Action e_TowerPurchased;
    void Start()
    {
        //groundTowers.SetActive(false);
        //airTowers.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void DisplayShop()
    {

    }

    public static void DisplayProfile(GameObject holder)
    {

    }
}
