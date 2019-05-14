using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    private string gameSceneName;

    [SerializeField]
    private GameObject creditsMenuPanel;

    [SerializeField]
    private GameObject titleMenuPanel;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("InventoryMenuTest");
    }

    public void ShowCredits()
    {
        Application.OpenURL("https://github.com/RSand18/ages-fpn-demo/tree/master");
    }

    public void ShowTitle ()
    {
        titleMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
