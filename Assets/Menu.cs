using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Menu : MonoBehaviour
{
    [Header("Basic Menu Info")]
    [SerializeField] private MenuTypes thisMenu = MenuTypes.DEFAULT;
    [SerializeField] private bool startsActive = false;

    //[Tooltip("If the menu isn't animation don't bother touching these.")]
    [Header("Animation Variables")]
    [Header("(Don't touch if not animating)")]
    [SerializeField] private bool animateTransition = false;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private animPaths animationDirection;
    [SerializeField] private animFunctions animationType;
    private enum animPaths
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private enum animFunctions
    {
        LINEAR,
        BOUNCE
    }

    private bool opened = false;
    private Vector2 onScreenPosition;
    private Vector2 newLocation;
    private AnimationCurve openingCurve;
    private AnimationCurve closingCurve;
    private Coroutine animationCoroutine;
    private Rect canvasRect;
    //private Canvas parentCanvas;
    //private MenuManager menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

    void Start()
    {
        onScreenPosition = GetComponent<RectTransform>().anchoredPosition;
        newLocation = GetComponent<RectTransform>().anchoredPosition + new Vector2(1000, 0);
        canvasRect = GetComponentInParent<Canvas>().pixelRect;

        //openingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        //AnimationCurve.Eas
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

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator AnimateWindow()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            //float eval = openingCurve.Evaluate(elapsedTime / animationTime);
            float eval = AnimFunctionEvaluate(elapsedTime / animationDuration);
            GetComponent<RectTransform>().anchoredPosition = Vector2.LerpUnclamped(onScreenPosition, newLocation, eval);

            elapsedTime += Time.deltaTime;

            Debug.Log(GetComponent<RectTransform>().anchoredPosition);

            yield return null;
        }

    }

    public virtual void OpenClose(MenuTypes toOpen)
    {
        if (toOpen.Equals(thisMenu) && !opened)
        {
            if (animateTransition)
            {
                foreach (Transform childTransform in gameObject.GetComponentInChildren<Transform>())
                {
                    childTransform.gameObject.SetActive(true);
                }
                GetComponent<Image>().enabled = true;

                animationCoroutine = StartCoroutine(AnimateWindow());
                opened = true;
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
                //GetComponent<Transform>().position = Vector2.Lerp(openTo.position, closeTo.position, animationTime);
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

    private float AnimFunctionEvaluate(/*animFunctions animType, */float t)
    //shouldn't need to specify animtype on function call unless design changes, just use the selected type
    {
        switch (animationType)
        {
            case animFunctions.LINEAR:
                return t;
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

    private Vector2 FindClosePosition(/*animPaths animDir*/)
    //Shouldn't need to specify animDirection on function call unless design changes, just use selected type
    {
        RectTransform panelRect = GetComponent<RectTransform>();
        switch(animationDirection)
        {
            case animPaths.UP:
                return new Vector2(onScreenPosition.x, onScreenPosition.y - canvasRect.height - panelRect.sizeDelta.y);

            case animPaths.DOWN:
                return new Vector2(onScreenPosition.x, onScreenPosition.y + canvasRect.height - panelRect.sizeDelta.y);

            default:
                return Vector2.zero;
        }
    }
}
