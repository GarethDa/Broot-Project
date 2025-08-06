using UnityEngine;

public static class EventHolder
{
    public static event System.Action<MenuTypes> e_MenuOpened;

    public static void InvokeEvent(string eventName)
    {
    }
}
