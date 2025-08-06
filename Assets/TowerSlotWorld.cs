using UnityEngine;

public class TowerSlotWorld : MonoBehaviour
{
    [SerializeField] private TowerSlots towerSlotNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TowerMenuManager.e_TowerPurchased += PlaceTower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceTower(GameObject newTower, TowerSlots slotToPlace)
    {
        if (towerSlotNum.Equals(slotToPlace))
        {
            newTower.transform.position = transform.position;
            //newTower.transform.SetParent(transform);
        }
    }
}
