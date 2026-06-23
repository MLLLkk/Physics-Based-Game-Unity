using UnityEngine;
using UnityEngine.SceneManagement; // لإدارة المشاهد

public class MainMenuController : MonoBehaviour
{
    // هذه الدالة تربطها بزر الـ Start الجاهز
    public void PlayGame()
    {
        // سينقلك فوراً لمشهد اللعبة الأساسي
        SceneManager.LoadScene("Level_Easy");
    }

    // هذه الدالة تربطها بزر الـ Exit الجاهز
    public void QuitGame()
    {
        Application.Quit(); // يغلق اللعبة نهائياً
        Debug.Log("SYSTEM: Game Closed.");
    }
}