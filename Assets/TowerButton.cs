using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private Tower towerInfo;
    [SerializeField] private GameObject profileTemplate;

    private GameObject towerProfile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = towerInfo.towerName;
        
        //Instantiate as a child of the canvas in order to get the same transform as the template,
        //then set as child of button for organization
        towerProfile = Instantiate(profileTemplate, transform.parent.parent);
        towerProfile.transform.SetParent(transform);
        towerProfile.GetComponent<ProfileSetup>().SetProfileFields(towerInfo);
        
        towerProfile.SetActive(false);

        if (GetComponent<Toggle>() != null)
        {
            GetComponent<Toggle>().onValueChanged.AddListener(SetProfileActive);
            //GetComponent<Toggle>().group.SetAllTogglesOff;
        }

        //e_TowerShopButtonClicked += HideProfile;
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

    public void SetProfileActive(bool activated)
    {
        if (activated)
            towerProfile.SetActive(true);
        else
            towerProfile.SetActive(false);
        //e_TowerShopButtonClicked.Invoke();
    }

    public Tower GetTowerInfo()
    {
        return towerInfo;
    }
}
