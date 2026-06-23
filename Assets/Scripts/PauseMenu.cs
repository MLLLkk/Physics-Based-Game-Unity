using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[Header("UI")]
	public GameObject exitPanel;

	private bool isOpen = false;

	void Start()
	{
		exitPanel.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isOpen)
			{
				ContinueGame();
			}
			else
			{
				OpenExitPanel();
			}
		}
	}

	public void OpenExitPanel()
	{
		exitPanel.SetActive(true);
		Time.timeScale = 0f;
		isOpen = true;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void ContinueGame()
	{
		exitPanel.SetActive(false);
		Time.timeScale = 1f;
		isOpen = false;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1f;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		SceneManager.LoadScene("MainMenu");
	}
}