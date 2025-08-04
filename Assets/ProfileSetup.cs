using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileSetup : MonoBehaviour
{
    [SerializeField] private Image iconField;
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text statsField;
    [SerializeField] private TMP_Text desctiptionField;
    [SerializeField] private TMP_Text purchaseField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProfileFields(Sprite towerIcon, string towerName, string towerStats, string towerDescription, float towerCost)
    {
        iconField.sprite = towerIcon;
        nameField.text = towerName;
        statsField.text = towerStats;
        desctiptionField.text = towerDescription;
        purchaseField.text = "Purchase? Cost - " + towerCost;
    }
}
