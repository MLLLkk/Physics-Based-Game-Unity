using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("HUD Text References")]
    public Text textAttempts;
    public Text textScore;
    public Text textTimer;
    public Text textFeedback;

    public float feedbackDuration = 2.5f;
    private float feedbackTimer = 0f;

    void Update()
    {
        if (feedbackTimer > 0f)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f && textFeedback != null)
                textFeedback.text = "";
        }
    }

    public void Refresh(int attempts, int score, float time, string feedback)
    {
        if (textAttempts != null) textAttempts.text = "Attempts: " + attempts;
        if (textScore != null) textScore.text = "Score: " + score;
        UpdateTimer(time);
        if (!string.IsNullOrEmpty(feedback)) ShowFeedback(feedback);
    }

    public void UpdateTimer(float seconds)
    {
        if (textTimer == null) return;
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        textTimer.text = string.Format("Time: {0:00}:{1:00}", m, s);
    }

    public void ShowFeedback(string msg)
    {
        if (textFeedback == null) return;
        textFeedback.text = msg;
        feedbackTimer = feedbackDuration;
    }
}