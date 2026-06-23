using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Attach to: MainMenuManager (empty GameObject in MainMenu scene)
public class MainMenuManager : MonoBehaviour
{
    // ── Panel references (drag from Hierarchy in Inspector) ──
    [Header("Panels")]
    public GameObject panelMain;        // the main button panel
    public GameObject panelAbout;       // the About panel
    public GameObject panelHowToPlay;   // the How to Play panel
    public GameObject panelLevelSelect; // the Level Select panel

    // ── Scene names (must match your Build Settings exactly) ──
    [Header("Scene Names")]
    public string sceneEasy = "Level_Easy";
    public string sceneMedium = "Level_Medium";
    public string sceneHard = "Level_Hard";
    public string sceneExpert = "Level_Expert";

    void Start()
    {
        ShowMain(); // always start on the main panel
    }

    // ── Called by the main-menu buttons ──────────────────────
    public void OnClickPlay() { ShowPanel(panelLevelSelect); }
    public void OnClickHowToPlay() { ShowPanel(panelHowToPlay); }
    public void OnClickAbout() { ShowPanel(panelAbout); }
    public void OnClickQuit() { Application.Quit(); }

    // ── Called by the Level Select buttons ───────────────────
    public void OnClickEasy() { LoadLevel(sceneEasy); }
    public void OnClickMedium() { LoadLevel(sceneMedium); }
    public void OnClickHard() { LoadLevel(sceneHard); }
    public void OnClickExpert() { LoadLevel(sceneExpert); }

    // ── Called by any Back button ─────────────────────────────
    public void OnClickBack() { ShowMain(); }

    // ── Helpers ──────────────────────────────────────────────
    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ShowMain()
    {
        ShowPanel(panelMain);
    }

    void ShowPanel(GameObject target)
    {
        // Hide all panels, then show only the requested one
        panelMain.SetActive(false);
        panelAbout.SetActive(false);
        panelHowToPlay.SetActive(false);
        panelLevelSelect.SetActive(false);
        target.SetActive(true);
    }
}
