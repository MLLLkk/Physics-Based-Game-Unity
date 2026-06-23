using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Force & Charge Settings")]
    public float minForce = 15f;
    public float maxForce = 50f;
    public float maxChargeTime = 2.0f;

    [Header("Game Stats")]
    public int score = 0;
    public int totalAttempts = 5;
    public float timeLimit = 60f;

    [Header("Level & Puzzle Settings")]
    public int totalTargetsInLevel;
    private int targetsDestroyed = 0;
    public GameObject goldCoin;

    [Header("Difficulty & Transitions")]
    public string nextSceneName;

    [HideInInspector] public int currentAttempts;
    private float timeRemaining;
    private bool isGameActive = true;

    [Header("UI References")]
    public Text attemptsText;
    public Text scoreText;
    public Text timerText;
    public Text statusText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        currentAttempts = totalAttempts;
        timeRemaining = timeLimit;

        if (goldCoin != null) goldCoin.SetActive(false);
        if (statusText != null) statusText.text = "";

        if (totalTargetsInLevel <= 0)
        {
            totalTargetsInLevel = GameObject.FindGameObjectsWithTag("Target").Length;
        }

        UpdateUI();
    }

    void Update()
    {
        if (isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateUI();
            }
            else
            {
                timeRemaining = 0;
                UpdateUI();
                StartCoroutine(LevelFailedRoutine("YOU LOST! TRY AGAIN"));
            }
        }
    }

    public void UseAttempt()
    {
        if (currentAttempts > 0 && isGameActive)
        {
            currentAttempts--;
            UpdateUI();
        }

        if (currentAttempts <= 0 && targetsDestroyed < totalTargetsInLevel && isGameActive)
        {
            isGameActive = false;
            StartCoroutine(LevelFailedRoutine("YOU LOST! OUT OF ARROWS"));
        }
    }

    public void OnTargetHit()
    {
        if (!isGameActive) return;

        targetsDestroyed++;
        RegisterHit();

        if (targetsDestroyed >= totalTargetsInLevel)
        {
            if (goldCoin != null)
            {
                goldCoin.SetActive(true);
            }
        }
    }

    public void RegisterHit()
    {
        score++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (attemptsText != null) attemptsText.text = "Attempts: " + currentAttempts;
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (timerText != null) timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    // هنا الخسارة ثابتة على 5 ثوانٍ فقط كما طلبت
    System.Collections.IEnumerator LevelFailedRoutine(string message)
    {
        isGameActive = false;
        if (statusText != null)
        {
            statusText.color = Color.red;
            statusText.text = message;
        }

        yield return new WaitForSeconds(5f); // 5 ثوانٍ للإعادة
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // هنا الفوز ثابت على 20 ثانية كاملة للاحتفال
    public System.Collections.IEnumerator LevelWonRoutine()
    {
        isGameActive = false;

        if (statusText != null)
        {
            statusText.color = Color.green;
            statusText.text = "YOU WIN! CELEBRATING VICTORY...";
        }

        yield return new WaitForSeconds(20f); // 20 ثانية للاحتفال بالـ Fireworks ثم الانتقال

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            if (statusText != null) statusText.text = "ALL LEVELS COMPLETED!";
        }
    }
}