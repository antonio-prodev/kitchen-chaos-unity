using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;

    private int previousCountDownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.State.CountdownToStart)
        {
            int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
            countDownText.text = countDownNumber.ToString();

            if(countDownNumber != previousCountDownNumber)
            {
                previousCountDownNumber = countDownNumber;
                animator.SetTrigger(NUMBER_POPUP);
                SoundManager.Instance.PlayCountdownSound();
            }
        }
    }

    private void Show()
    {
        countDownText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        countDownText.gameObject.SetActive(false);
    }
}
