using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeProfileSetup : MonoBehaviour
{
    [SerializeField] private Image iconField;
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text statsField;
    [SerializeField] private TMP_Text desctiptionField;
    [SerializeField] private TMP_Text sellField;

    private Tower thisTowerInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetProfileFields(Tower towerInfo)
    {
        iconField.sprite = towerInfo.baseSprite;
        nameField.text = towerInfo.towerName;
        //statsField.text = towerInfo.FormatStats();
        desctiptionField.text = towerInfo.towerDescription;
        sellField.text = "Sell?";

        thisTowerInfo = towerInfo;
    }
}
