using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public MenuTypes thisMenu = MenuTypes.DEFAULT;
    public bool animateTransition = false;
    public Transform openTo;
    public Transform closeTo;
    public bool startsActive = false;

    [System.NonSerialized] public bool opened = false;
    //private MenuManager menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

    public Menu()
    {
    }

    public virtual void OnEnable()
    {
        MenuManager.e_MenuOpened += OpenClose;

        foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
        {
            childTransform.gameObject.SetActive(false);
        }
        GetComponent<Image>().enabled = false;

        if (startsActive)
            MenuManager.OpenMenu((int)thisMenu);

        opened = startsActive;
    }

    public virtual void OnDestroy()
    {
        MenuManager.e_MenuOpened -= OpenClose;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OpenClose(MenuTypes toOpen)
    {
        if (toOpen.Equals(thisMenu) && !opened)
        {
            if (animateTransition)
            {
                //Animate here
            }

            else
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(true);
                }
                GetComponent<Image>().enabled = true;
                opened = true;
            }
        }

        else if (!toOpen.Equals(thisMenu) && opened)
        {
            //if (thisMenu.Equals(MenuTypes.PAUSE_MENU))
            //    MenuManager.OpenStoredMenu();

            if (animateTransition)
            {
                //Animate here
            }

            else
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(false);
                }
                GetComponent<Image>().enabled = false;
                opened = false;
            }
        }
    }
}
