using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private GameObject pressKeyToRebindGameObject;

    [Header("Buttons")]
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;

    private Action onCloseAction;


    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseAction();
        });

        moveUpButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { PerformRebinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();

        Hide();
        HidePressKeyToRebind();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show(Action onCloseAction)
    {
        this.onCloseAction = onCloseAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressKeyToRebind()
    {
        pressKeyToRebindGameObject.SetActive(true);
    }

    private void HidePressKeyToRebind()
    {
        pressKeyToRebindGameObject.SetActive(false);
    }

    private void PerformRebinding(GameInput.Binding binding)
    {
        ShowPressKeyToRebind();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressKeyToRebind();
            UpdateVisual();
        });
    }
}
