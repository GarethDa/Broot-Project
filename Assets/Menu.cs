using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Menu : MonoBehaviour
{
    [Header("Basic Menu Info")]
    
    [Tooltip("Corresponds to the MenuTypes enum. If creating a new type of menu, add it to the MenuTypes enum script.")]
    [SerializeField] private MenuTypes thisMenu = MenuTypes.DEFAULT;
    
    [Tooltip("Select true for the menu you want open by default (ONLY ONE MENU OPEN AT A TIME IN CURRENT SYSTEM).")]
    [SerializeField] private bool startsActive = false;
    
    [Tooltip("Select true for menu transitions, false for menu to simply appear/disappear when opened/closed")]
    [SerializeField] private bool animateTransition = false;

    //[Tooltip("If the menu isn't animation don't bother touching these.")]
    [Header("Animation Variables")]
    [Header("(Don't touch if not animating transition)")]
    
    [SerializeField] private float openingDuration = 1f;
    [SerializeField] private float closingDuration = 1f;
    
    [Tooltip("Select the side of the screen that the menu will transition from when opening.")]
    [SerializeField] private animPaths openFrom;
    
    [Tooltip("Type of easing function being used for opening transition.")]
    [SerializeField] private animFunctions openingAnimation;
    
    [Tooltip("Type of easing function being used for closing transition.")]
    [SerializeField] private animFunctions closingAnimation;

    //Enum for selecting direction for opening/closing menu transitions.
    //Could add more if desired (e.g. middle-left, middle-right, corners, etc.)
    private enum animPaths
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }

    //Enum for selecting type of easing function used in menu transitions.
    //Could easily add more, see https://easings.net/
    private enum animFunctions
    {
        LINEAR,
        SINE,
        CUBIC,
        QUART,
        OVERSHOOT,
        BOUNCE
    }

    private bool opened = false;
    private bool currentlyAnimating = false;
    private float currentT = 0f;

    //Holds the resting positions for the menu on- and off-screen
    private Vector2 onScreenPosition;
    private Vector2 offScreenPosition;

    //Coroutine for doing menu animation
    private Coroutine animationCoroutine;

    //The parent canvas' rect
    private Rect canvasRect;

    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().pixelRect;

        //For now, resting on-screen position should be the position that the menu sits at by default in the editor
        onScreenPosition = GetComponent<RectTransform>().anchoredPosition;

        //Find the close position corresponding to the side of the screen that the menu animates to/from
        offScreenPosition = FindClosePosition();

        //Subscribe the Opening/Closing function to the menu opening event
        MenuManager.e_MenuOpened += OpenClose;

        //If it isn't an animating menu, just set all of the menu's elements to inactive and disable the image (unless it's open by default)
        if (!animateTransition)
        {
            if (!startsActive)
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(false);
                }
                GetComponent<Image>().enabled = false;
            }

        }

        //If it is an animating menu:
        else
        {
            //If it is the default open menu, set it to its on-screen position
            if (startsActive)
            {
                GetComponent<RectTransform>().anchoredPosition = onScreenPosition;
            }

            //Otherwise, set it to its off-screen position
            else
                GetComponent<RectTransform>().anchoredPosition = offScreenPosition;
        }

        opened = startsActive;
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDestroy()
    {
        //Unsubscribe on destruction
        MenuManager.e_MenuOpened -= OpenClose;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator AnimateWindow(bool opening)
    {
        float elapsedTime = 0f;

        //Set the animation duration to the corresponding opening or closing duration
        float animDuration = opening ? openingDuration : closingDuration;

        Vector2 startingPos;

        //If the menu was already in the middle of animating (rare case), set the starting position
        //to be the current position and shorten the animation duration accordingly
        if (currentlyAnimating)
        {
            startingPos = GetComponent<RectTransform>().anchoredPosition;
            animDuration *= currentT;
        }

        //Otherwise (most commonly) this is a fresh animation from rest, so keep the animation duration
        //and select the correct starting position (off-screen if opening, on-screen if closing)
        else
            startingPos = opening ? offScreenPosition : onScreenPosition;

        //Coroutine beginning, set currentlyanimating to true
        currentlyAnimating = true;

        //Coroutine body
        while (elapsedTime < animDuration)
        {
            //Find the interpolation variable
            currentT = elapsedTime / animDuration;

            //Evaluate at the current elapsed time, using the animation type corresponding to if it's opening or closing
            float eval = AnimFunctionEvaluate(opening ? openingAnimation : closingAnimation, currentT);
            
            //If the menu is opening, lerp to the on-screen position
            if (opening)
                GetComponent<RectTransform>().anchoredPosition = Vector2.LerpUnclamped(startingPos, onScreenPosition, eval);

            //Else the menu is closing, so lerp to the off-screen position
            else
                GetComponent<RectTransform>().anchoredPosition = Vector2.LerpUnclamped(startingPos, offScreenPosition, eval);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //Coroutine finished, set currentlyanimating to false
        currentlyAnimating = false;
    }

    //Function for opening/closing the menu, this should be subscribed to and only used with the menu open event,
    //which will send the type of menu to open
    public virtual void OpenClose(MenuTypes toOpen)
    {
        //If the menu to open equals this menu type, and the menu isn't already open, then let's open the menu
        if (toOpen.Equals(thisMenu) && !opened)
        {
            //If we're doing an animated transition, start the animation coroutine. If a coroutine is already in progress, stop it first.
            if (animateTransition)
            {
                if (currentlyAnimating)
                    StopCoroutine(animationCoroutine);

                animationCoroutine = StartCoroutine(AnimateWindow(true));
            }

            //If we're not doing an animated transition, just activate all of the children and enable the image
            else
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(true);
                }
                GetComponent<Image>().enabled = true;
            }

            opened = true;
        }

        //If this menu is currently opened and the menu to open isn't this menu, that means we should close this menu
        else if (!toOpen.Equals(thisMenu) && opened)
        {
            //If we're doing an animation transition, start the animation coroutine. If a coroutine is already in progress, stop it first.
            if (animateTransition)
            {
                if (currentlyAnimating)
                    StopCoroutine(animationCoroutine);

                animationCoroutine = StartCoroutine(AnimateWindow(false));
            }

            //If we're not doing an animated transition, just deactivate all of the children and disable the image
            else
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(false);
                }
                GetComponent<Image>().enabled = false;
                
            }

            opened = false;
        }
    }

    //Function for evaluating the lerp parameter (t) based on which type of interpolation function being used
    //If you want to add more types, just add the type to the animFunctions enum,
    //and go to https://easings.net/ to find the implementation for the corresponding function
    private float AnimFunctionEvaluate(animFunctions animType, float t)
    {
        switch (animType)
        {
            case animFunctions.LINEAR:
                return t;

            case animFunctions.SINE:
                return Mathf.Sin((t * Mathf.PI) / 2);

            case animFunctions.CUBIC:
                return 1 - Mathf.Pow(1 - t, 3);

            case animFunctions.QUART:
                return 1 - Mathf.Pow(1 - t, 4);

            case animFunctions.OVERSHOOT:
                return 1 + (2.70158f * Mathf.Pow(t - 1, 3)) + (1.70158f * Mathf.Pow(t - 1, 2));

            case animFunctions.BOUNCE:
                if (t == 0)
                    return 0;
                else if (t == 1)
                    return 1;
                else
                    return Mathf.Pow(2, -10 * t) * Mathf.Sin((float)((t * 10 - 0.75) * ((2 * Mathf.PI) / 3))) + 1;
                
            default:
                return t;
        }
    }

    //Returns the closing position given the selected opening direction
    private Vector2 FindClosePosition(/*animPaths animDir*/)
    //Shouldn't need to specify animDirection on function call unless design changes, just use selected type
    {
        Rect panelRect = GetComponent<RectTransform>().rect;

        //Note that, for rects, the position is in the center, meaning that we need to divide widths and heights by 2 when doing these calculations
        switch(openFrom)
        {
            case animPaths.TOP:
                return new Vector2(onScreenPosition.x, canvasRect.height / 2f + panelRect.height / 2f);

            case animPaths.BOTTOM:
                return new Vector2(onScreenPosition.x, -panelRect.height / 2f - canvasRect.height / 2f);

            case animPaths.LEFT:
                return new Vector2(-panelRect.width / 2f - canvasRect.width / 2f, onScreenPosition.y);

            case animPaths.RIGHT:
                return new Vector2(canvasRect.width / 2f + panelRect.width / 2f, onScreenPosition.y);

            default:
                return Vector2.zero;
        }
    }
}
