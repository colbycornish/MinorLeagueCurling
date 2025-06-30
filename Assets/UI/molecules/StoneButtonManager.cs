using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class StoneButtonManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
{
    // Content
    public State state = State.Default;
    public Sprite buttonIcon;
    public string buttonTitle = "Button";
    public string buttonDescription = "Description";
    public Sprite priceIcon;
    public string priceText = "123";
    public BackgroundFilter backgroundFilter;

    // Resources
    [SerializeField] private Animator animator;
    public ButtonManager purchaseButton;
    public ButtonManager purchasedButton;
    public GameObject purchasedIndicator;
    public Image iconObj;
    public Image priceIconObj;
    public TextMeshProUGUI titleObj;
    public TextMeshProUGUI descriptionObj;
    public TextMeshProUGUI priceObj;
    public Image filterObj;
    public List<Sprite> filters = new List<Sprite>();

    // Settings
    public bool isInteractable = true;
    // public bool enableIcon = false;
    // public bool enableTitle = true;
    // public bool enableDescription = true;
    public bool enablePrice = true;
    public bool enableFilter = true;
    public bool bypassUpdateOnEnable = false;
    public bool useUINavigation = false;
    public Navigation.Mode navigationMode = Navigation.Mode.Automatic;
    public GameObject selectOnUp;
    public GameObject selectOnDown;
    public GameObject selectOnLeft;
    public GameObject selectOnRight;
    public bool wrapAround = false;
    public bool useSounds = true;

    // Events
    public UnityEvent onPurchaseClick = new UnityEvent();
    public UnityEvent onPurchase = new UnityEvent();
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onHover = new UnityEvent();
    public UnityEvent onLeave = new UnityEvent();
    public UnityEvent onSelect = new UnityEvent();
    public UnityEvent onDeselect = new UnityEvent();

    // Helpers
    bool isInitialized = false;
    float cachedStateLength = 0.5f;
    Button targetButton;
#if UNITY_EDITOR
    public int latestTabIndex = 0;
#endif

    public enum State { Default, Purchased }

    public enum BackgroundFilter
    {
        Aqua,
        Dawn,
        Dusk,
        Emerald,
        Kylo,
        Memory,
        Mice,
        Pinky,
        Retro,
        Rock,
        Sunset,
        Violet,
        Warm,
        Random
    }

    void Awake()
    {
        // cachedStateLength = ReachUIInternalTools.GetAnimatorClipLength(animator, "ShopButton_Highlighted") + 0.02f;
        // InitializePurchaseEvents();
    }

    void OnEnable()
    {
        if (!isInitialized) { Initialize(); }
        if (!bypassUpdateOnEnable) { UpdateUI(); }
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject) { TriggerAnimation("Highlighted"); }
        if (Application.isPlaying && useUINavigation) { AddUINavigation(); }
        else if (Application.isPlaying && !useUINavigation && targetButton == null)
        {
            if (gameObject.GetComponent<Button>() == null) { targetButton = gameObject.AddComponent<Button>(); }
            else { targetButton = GetComponent<Button>(); }

            targetButton.transition = Selectable.Transition.None;
        }
    }

    void Initialize()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) { return; }
#endif
        if (animator == null) { animator = GetComponent<Animator>(); }
        // if (UIManagerAudio.instance == null) { useSounds = false; }
        if (GetComponent<Image>() == null)
        {
            Image raycastImg = gameObject.AddComponent<Image>();
            raycastImg.color = new Color(0, 0, 0, 0);
            raycastImg.raycastTarget = true;
        }

        TriggerAnimation("Start");

        isInitialized = true;
    }

    public void UpdateUI()
    {
        iconObj.gameObject.SetActive(true); iconObj.sprite = buttonIcon;
        titleObj.gameObject.SetActive(true); titleObj.text = buttonTitle;
        descriptionObj.gameObject.SetActive(true); descriptionObj.text = buttonDescription;
        
        UpdateState();

        if (!Application.isPlaying || !gameObject.activeInHierarchy) { return; }

        animator.enabled = true;
        animator.SetTrigger("Start");

        StopCoroutine("DisableAnimator");
        StartCoroutine("DisableAnimator");
    }

    public void UpdateState()
    {
        if (purchaseButton == null || purchasedButton == null || purchasedIndicator == null)
            return;

        if (state == State.Default)
        {
            purchaseButton.gameObject.SetActive(true);
            purchasedButton.gameObject.SetActive(false);
            purchasedIndicator.gameObject.SetActive(false);
        }

        else if (state == State.Purchased)
        {
            purchaseButton.gameObject.SetActive(false);
            purchasedButton.gameObject.SetActive(true);
            purchasedIndicator.gameObject.SetActive(true);
        }
    }

    public void SetState(State tempState)
    {
        state = tempState;
        UpdateState();
    }

    public void Purchase()
    {
        if (state == State.Purchased)
            return;

        SetState(State.Purchased);
        onPurchase.Invoke();
    }


    public void SetText(string text) { buttonTitle = text; UpdateUI(); }
    public void SetIcon(Sprite icon) { buttonIcon = icon; UpdateUI(); }
    public void SetPrice(string text) { priceText = text; UpdateUI(); }
    public void SetInteractable(bool value) { isInteractable = value; }

    public void AddUINavigation()
    {
        if (targetButton == null)
        {
            if (gameObject.GetComponent<Button>() == null) { targetButton = gameObject.AddComponent<Button>(); }
            else { targetButton = GetComponent<Button>(); }

            targetButton.transition = Selectable.Transition.None;
        }

        if (targetButton.navigation.mode == navigationMode)
            return;

        Navigation customNav = new Navigation();
        customNav.mode = navigationMode;

        if (navigationMode == Navigation.Mode.Vertical || navigationMode == Navigation.Mode.Horizontal)
        {
            customNav.wrapAround = wrapAround;
        }
        else if (navigationMode == Navigation.Mode.Explicit)
        {
            StartCoroutine("InitUINavigation", customNav);
            return;
        }

        targetButton.navigation = customNav;
    }

    public void DisableUINavigation()
    {
        if (targetButton != null)
        {
            Navigation customNav = new Navigation();
            Navigation.Mode navMode = Navigation.Mode.None;
            customNav.mode = navMode;
            targetButton.navigation = customNav;
        }
    }

    public void InvokeOnClick() 
    { 
        onClick.Invoke(); 
    }

    void TriggerAnimation(string triggername)
    {
        animator.enabled = true;

        animator.ResetTrigger("Start");
        animator.ResetTrigger("Normal");
        animator.ResetTrigger("Highlighted");

        animator.SetTrigger(triggername);

        StopCoroutine("DisableAnimator");
        StartCoroutine("DisableAnimator");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable || eventData.button != PointerEventData.InputButton.Left) { return; }
        // if (useSounds) { UIManagerAudio.instance.audioSource.PlayOneShot(UIManagerAudio.instance.UIManagerAsset.clickSound); }

        // Invoke click actions
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) { return; }
        // if (useSounds) { UIManagerAudio.instance.audioSource.PlayOneShot(UIManagerAudio.instance.UIManagerAsset.hoverSound); }

        TriggerAnimation("Highlighted");
        onHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable)
            return;

        TriggerAnimation("Normal");
        onLeave.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!isInteractable) { return; }
        // if (useSounds) { UIManagerAudio.instance.audioSource.PlayOneShot(UIManagerAudio.instance.UIManagerAsset.hoverSound); }

        TriggerAnimation("Highlighted");
        onSelect.Invoke();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!isInteractable)
            return;

        TriggerAnimation("Normal");
        onDeselect.Invoke();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (!isInteractable) { return; }
        // if (useSounds) { UIManagerAudio.instance.audioSource.PlayOneShot(UIManagerAudio.instance.UIManagerAsset.clickSound); }
        if (EventSystem.current.currentSelectedGameObject != gameObject) { TriggerAnimation("Normal"); }

        onClick.Invoke();
    }

    IEnumerator InitUINavigation(Navigation nav)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        
        if (selectOnUp != null) { nav.selectOnUp = selectOnUp.GetComponent<Selectable>(); }
        if (selectOnDown != null) { nav.selectOnDown = selectOnDown.GetComponent<Selectable>(); }
        if (selectOnLeft != null) { nav.selectOnLeft = selectOnLeft.GetComponent<Selectable>(); }
        if (selectOnRight != null) { nav.selectOnRight = selectOnRight.GetComponent<Selectable>(); }
        
        targetButton.navigation = nav;
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSecondsRealtime(cachedStateLength);
        animator.enabled = false;
    }
}



    // public void InitializePurchaseEvents()
    // {
    //     if (purchaseButton == null)
    //     {
    //         Debug.LogError("<b>[Shop Button]</b> 'Purchase Button' is missing.", this);
    //         return;
    //     }

    //     purchaseButton.onClick.RemoveAllListeners();

    //     if (useModalWindow && purchaseModal != null)
    //     {
    //         onPurchaseClick.AddListener(delegate
    //         {
    //             purchaseModal.onConfirm.RemoveAllListeners();
    //             purchaseModal.onConfirm.AddListener(Purchase);
    //             purchaseModal.onConfirm.AddListener(purchaseModal.CloseWindow);
    //             purchaseModal.OpenWindow();
    //         });
    //     }

    //     purchaseButton.onClick.AddListener(onPurchaseClick.Invoke);
    // }