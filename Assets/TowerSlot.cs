using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TowerSlot : MonoBehaviour
{
    [SerializeField] private GameObject emptyPanelToDisplay;
    [SerializeField] private bool displayedByDefault = false;
    //[SerializeField] private GameObject sampleProfile;
   
    private Button thisButton;
    private BaseEventData eventData;
    private GameObject currentItemToDisplay;
    private GameObject thisTowerProfile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //thisTowerProfile = Instantiate(sampleProfile);

        currentItemToDisplay = emptyPanelToDisplay;
        thisButton = GetComponent<Button>();

        if (!displayedByDefault)
        {
            currentItemToDisplay.SetActive(false);
        }

        GetComponent<Toggle>().onValueChanged.AddListener(SetItemActive);

        //thisButton.onClick.AddListener(DisplayItem);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDisable()
    {
        //If a toggle is on when it's disabled, turn it off (so when the menu is opened again, it's a fresh start)
        if (GetComponent<Toggle>().isOn)
            GetComponent<Toggle>().isOn = false;
    }
    private void SetItemActive(bool activated)
    {
        if (activated)
            currentItemToDisplay.SetActive(true);
        else
            currentItemToDisplay.SetActive(false);
    }
}
