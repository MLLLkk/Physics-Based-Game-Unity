using UnityEngine;
using UnityEngine.SceneManagement;

public class UltimateMenuManager : MonoBehaviour
{
    [Header("اسم مشهد اللعبة الفعلي")]
    public string gameSceneName = "Level_Easy";

    void Awake()
    {
        // الكود هنا بينظف المشهد المنسوخ تلقائياً أول ما تشغل اللعبة!

        // 1. البحث عن لاعب الـ Rigidbody وإلغائه عشان ما يتحرك في المنيو
        GameObject player = GameObject.Find("RigidBodyFPSController");
        if (player != null)
        {
            // نعطل حركة اللاعب والماوس الخاص به
            player.SetActive(false);
            Debug.Log("SYSTEM: Player Controller disabled for Main Menu.");
        }

        // 2. إظهار الماوس على الشاشة عشان تقدر تضغط على الأزرار
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // دالة زر البدء - اربطها بزر Start Game
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    // دالة زر إعادة اللعب - اربطها بزر Restart في لوحة الفوز
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // دالة زر الخروج - اربطها بزر Exit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("SYSTEM: Game Closed.");
    }
}