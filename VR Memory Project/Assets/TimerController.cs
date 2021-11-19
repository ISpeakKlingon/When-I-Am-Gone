using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameObject timerComponents;
    [SerializeField] Image timerGraphic;
    [SerializeField] float gameTime;

    float maxgameTime;
    bool canTimerCountdown = false;

    private void Awake() => maxgameTime = gameTime;

    public void ActivateTimer()
    {
        timerComponents.SetActive(true);
        canTimerCountdown = true;
    }

    private void Update()
    {
        if (!canTimerCountdown)
            return;

        updateTimer();
        CheckTimer();
    }

    private void updateTimer()
    {
        gameTime -= Time.deltaTime;

        var updateTimerGraphicValue = gameTime / maxgameTime;

        timerGraphic.fillAmount = updateTimerGraphicValue;
    }

    private void CheckTimer()
    {
        if(timerGraphic.fillAmount <= 0f)
        {
            GameManager.Instance.GameOver();
            canTimerCountdown = false;
            gameTime = maxgameTime;

            timerComponents.SetActive(false);
        }
    }
}
