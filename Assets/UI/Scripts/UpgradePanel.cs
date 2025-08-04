using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject upgradeModule;
    [SerializeField] private Sprite inactiveTexture;
    [SerializeField] private Sprite activeTexture;
    [SerializeField] private Image moduleImage;
    [SerializeField] [Min(1)] private int numUpgrades = 1;
    [SerializeField] private RectTransform panelConstraint;
    [SerializeField] private TMP_Text buttonText;
    //[SerializeField] [Min(0)] private float spaceBetweenModules = 1;
    //This will override spaceBetweenModules if true
    [SerializeField] private bool centeredModules = true;
    private float spaceBetweenModules = 0;
    private int activatedUpgrades = 0;
    private GameObject[] upgradeModules; //Should only contain objects with image components corresponding to upgrade modules
    private float moduleAspectRatio;
    private GameObject moduleToAdd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeModules = new GameObject[numUpgrades];

        //Get the aspect ratio of whatever sprite we're using for the upgrade modules before we start adjusting the size
        moduleAspectRatio = moduleImage.sprite.rect.height / moduleImage.sprite.rect.width;

        //Find how wide each module should be by dividing up the panel width
        var widthPerModule = (panelConstraint.rect.width / numUpgrades);

        //If the corresponding width leads to a height larger than the height of the panel, readjust the height
        if (widthPerModule * moduleAspectRatio > panelConstraint.rect.height)
            widthPerModule = (panelConstraint.rect.height / moduleAspectRatio);

        //If the modules are centered, find the amount of extra space, if any, and divide it up by the number of modules
        if (centeredModules)
        {
            spaceBetweenModules += (panelConstraint.rect.width - (widthPerModule * numUpgrades)) / numUpgrades;
        }

        //Go through and instantiate each module
        for (int i = 0; i < numUpgrades; i++)
        {
            moduleToAdd = Instantiate(upgradeModule, panelConstraint);

            //Adjust the new module's size and position
            moduleToAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(widthPerModule, widthPerModule * moduleAspectRatio);
            moduleToAdd.GetComponent<RectTransform>().localPosition = new Vector2((i % numUpgrades) * widthPerModule + (0.5f * widthPerModule) - (0.5f * panelConstraint.rect.width), 0f);
            moduleToAdd.GetComponent<RectTransform>().localPosition += new Vector3(spaceBetweenModules * (i + 0.5f), 0f, 0f);

            //Add it to the array of modules
            upgradeModules[i] = moduleToAdd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradePurchased()
    {
        if (activatedUpgrades < numUpgrades)
        {
            //This is where you would play an animation to switch between states
            upgradeModules[activatedUpgrades].GetComponent<Image>().sprite = activeTexture;
            activatedUpgrades++;

            if (activatedUpgrades >= numUpgrades)
            {
                buttonText.text = "Fully Upgraded.";
                GetComponent<Button>().interactable = false;
            }
        }
    }

    public int GetCurrentUpgrade()
    {
        return activatedUpgrades;
    }
}
