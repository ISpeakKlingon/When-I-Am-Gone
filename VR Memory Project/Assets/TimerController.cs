using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameObject timerComponents, gameStartObject;
    [SerializeField] Image timerGraphic;
    [SerializeField] float gameTime;

    float maxgameTime;
    bool canTimerCountdown = false;

    private GameStartManager gameStartManager;

    private void Awake()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            GameManager.Instance.PlayerToZero();
            maxgameTime = gameTime;
            Debug.Log("TimerController script reset gameTime to maxgameTime;");
            GameManager.Instance.GameStart();
            Debug.Log("TimerController script asked GameManager to GameStart().");
        }
        else
        {
            maxgameTime = gameTime;
            gameTime = GameManager.Instance.gameTime;
        }
    }

    private void Start()
    {
        gameStartManager = gameStartObject.GetComponent<GameStartManager>();
    }

    public void ActivateTimer()
    {
        //Debug.Log("TimerController script is now going to turn on timer component objects.");
        timerComponents.SetActive(true);
        //Debug.Log("Successfullly turned on timer component objects.");
        //Debug.Log("TimerController script is now going to switch canTimerCountdown bool to true.");
        canTimerCountdown = true;
        //Debug.Log("canTimerCountdown is now set to " + canTimerCountdown);
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
        //Debug.Log("gameTime is " + gameTime);
        gameTime -= Time.deltaTime;

        var updateTimerGraphicValue = gameTime / maxgameTime;

        timerGraphic.fillAmount = updateTimerGraphicValue;

        //update Player timeRemaining variable... hmmm... how to do this?
        //pass time to Game manager? or Player?
        GameManager.Instance.gameTime = gameTime;

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

        else if(gameTime <= 60f)
        {
            Debug.Log("Final Minute!");
            gameStartManager.FinalMinute();
        }
    }
}
