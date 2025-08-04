using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;


public class MenuManager : MonoBehaviour
{
    //The menu opening event, invoked whenever a menu is opened
    public static event System.Action<MenuTypes> e_MenuOpened;

    //The currently opened menu
    private static MenuTypes currentMenu = MenuTypes.DEFAULT;

    //The most recently stored menu, to be opened the next time a menu in storeWhenOpened is closed
    //(as long as a menu in notToStore wasn't opened)
    private static MenuTypes storedMenu = MenuTypes.DEFAULT;

    //Since the stored menutype is meant to hold the last menu that was opened before pausing,
    //store any menutypes in this array that shouldn't be considered
    //(e.g. returning to base pause menu from settings shouldn't consider the settings menu as
    //the menu to return to after closing the pause menu)
    private static MenuTypes[] notToStore = 
        { MenuTypes.PAUSE_MENU, MenuTypes.SETTINGS };

    private static MenuTypes[] storeWhenOpened =
        { MenuTypes.PAUSE_MENU };

    // Update is called once per frame
    void Update()
    {
    }

    //Function for invoking the menu opening event.
    //This should mainly be attached to buttons that are used to open menus,
    //and to menu-opening inputs (e.g. ESC to open pause menu).
    //Unfortunately, Unity doesn't allow for enums in the button event editor,
    //so this allows an int as input and converts it to the corresponding MenuType within the function.
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

    //Function for opening a stored menu, don't think I need this anymore but will hold onto it for more
    public static void OpenStoredMenu()
    {
        e_MenuOpened?.Invoke(storedMenu);
        currentMenu = storedMenu;
    }
}


