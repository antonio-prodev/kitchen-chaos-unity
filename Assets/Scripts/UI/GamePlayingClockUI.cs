using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.State.GamePlaying)
        {
            timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        }
    }
}
