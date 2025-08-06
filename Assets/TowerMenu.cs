using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TowerMenuManager : MonoBehaviour
{
    //[SerializeField] GameObject groundTowers;
    //[SerializeField] GameObject airTowers;
    //[SerializeField] GameObject[] towerSlots;
    //[SerializeField] Tower[] towerScriptableObjects;

    [SerializeField] private ToggleGroup groundToggleGroup;
    [SerializeField] private ToggleGroup airToggleGroup;
    [SerializeField] private GameObject baseTowerPrefab;

    private TowerSlots currentTowerOpen = TowerSlots.NONE;

    public static event System.Action<GameObject, TowerSlots> e_TowerPurchased;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //groundTowers.SetActive(false);
        //airTowers.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayShop(TowerSlots slotOpened)
    {

    }

    public void DisplayProfile(GameObject holder)
    {

    }

    public void PurchaseTower()
    {
        Tower newTowerInfo;
        if (groundToggleGroup.GetFirstActiveToggle() != null)
            newTowerInfo = groundToggleGroup.GetFirstActiveToggle().gameObject.GetComponent<TowerButton>().GetTowerInfo();
            

        else
            newTowerInfo = airToggleGroup.GetFirstActiveToggle().gameObject.GetComponent<TowerButton>().GetTowerInfo();

        GameObject newTower = Instantiate(baseTowerPrefab);
        newTower.GetComponent<TowerBehaviour>().SetTowerInfo(newTowerInfo);
        e_TowerPurchased.Invoke(newTower, currentTowerOpen);
    }
}