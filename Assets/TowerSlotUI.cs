using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TowerSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject emptyPanelToDisplay;
    //[SerializeField] private bool displayedByDefault = false;
    [SerializeField] private TowerSlots towerSlotNum;
    //[SerializeField] private GameObject sampleProfile;
    [SerializeField] private GameObject upgradeProfileTemplate;
   
    private Toggle thisToggle;
    private GameObject currentItemToDisplay;
    private GameObject thisTowerProfile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisTowerProfile = Instantiate(upgradeProfileTemplate, transform.parent.parent);
        thisTowerProfile.transform.SetParent(transform);

        thisTowerProfile.SetActive(false);

        currentItemToDisplay = emptyPanelToDisplay;
        thisToggle = GetComponent<Toggle>();

        currentItemToDisplay.SetActive(false);

        thisToggle.onValueChanged.AddListener(SetItemActive);

        TowerMenuManager.e_TowerPurchased += UpdateProfile;

        //thisButton.onClick.AddListener(DisplayItem);
    }

    public void UpdateProfile(GameObject towerObject, TowerSlots slotToUpdate)
    {
        thisTowerProfile.GetComponent<UpgradeProfileSetup>().SetProfileFields(towerObject.GetComponent<TowerBehaviour>().GetTowerInfo());

        currentItemToDisplay = thisTowerProfile;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDisable()
    {
        //If a toggle is on when it's disabled, turn it off (so when the menu is opened again, it's a fresh start)
        if (thisToggle.isOn)
            thisToggle.isOn = false;
    }
    private void SetItemActive(bool activated)
    {
        if (activated)
            currentItemToDisplay.SetActive(true);
        else
            currentItemToDisplay.SetActive(false);
    }
}
