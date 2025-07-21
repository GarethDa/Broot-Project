using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;


public class MenuManager : MonoBehaviour
{
    //public UnityEvent<MenuTypes, MenuTypes> OpenMenuEvent = new UnityEvent<MenuTypes, MenuTypes>();
    public static event System.Action<MenuTypes> e_MenuOpened;

    private static MenuTypes currentMenu = MenuTypes.DEFAULT;
    private static MenuTypes storedMenu = MenuTypes.DEFAULT;

    //Since the stored menutype is meant to hold the last menu that was opened before pausing,
    //store any menutypes in this array that shouldn't be considered
    //(e.g. returning to base pause menu from settings shouldn't consider the settings menu as
    //the menu to return to after closing the pause menu)
    private static MenuTypes[] notToStore = 
        { MenuTypes.PAUSE_MENU, MenuTypes.SETTINGS };

    private static MenuTypes[] storeWhenOpened =
        { MenuTypes.PAUSE_MENU };

    //public static event System.Action<MenuTypes> e_OpenOverlaidMenu;

    //public static event System.Action<MenuTypes> e_OpenPauseMenu;
    /*
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    */

    // Update is called once per frame
    void Update()
    {
    }

    //Pass -1 to open stored menu
    public static void OpenMenu(int menuToOpen)
    {
        MenuTypes menuTypeToOpen;

        menuTypeToOpen = (MenuTypes)menuToOpen;

        //If we open a full, overlaid menu (e.g. pause menu), store the previous menu so we can reopen it when the full menu is closed.
        //Only do this if the menu we're closing isn't in the notToStore list!!
        if (!notToStore.Contains(currentMenu) && storeWhenOpened.Contains(menuTypeToOpen))
            storedMenu = currentMenu;

        e_MenuOpened?.Invoke(menuTypeToOpen);

        currentMenu = menuTypeToOpen;
        
    }

    public static void OpenStoredMenu()
    {
        e_MenuOpened?.Invoke(storedMenu);
        currentMenu = storedMenu;
    }

    /*
    public static void OpenOverlaidMenu(int menuToOpen)
    {
        e_OpenOverlaidMenu?.Invoke((MenuTypes)menuToOpen);
    }

    public static void OpenPauseMenu(int menuToOpen)
    {
        e_OpenPauseMenu?.Invoke((MenuTypes)menuToOpen);
    }
    */
}


