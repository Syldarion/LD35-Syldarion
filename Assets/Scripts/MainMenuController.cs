using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup MainPanel;
    public CanvasGroup HowToPanel;

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("main");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("main"));
    }

    public void SwitchToMain()
    {
        HowToPanel.alpha = 0;
        HowToPanel.blocksRaycasts = false;
        HowToPanel.interactable = false;

        MainPanel.alpha = 1;
        MainPanel.blocksRaycasts = true;
        MainPanel.interactable = true;
    }

    public void SwitchToHowTo()
    {
        MainPanel.alpha = 0;
        MainPanel.blocksRaycasts = false;
        MainPanel.interactable = false;

        HowToPanel.alpha = 1;
        HowToPanel.blocksRaycasts = true;
        HowToPanel.interactable = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}