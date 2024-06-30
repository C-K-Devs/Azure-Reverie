using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class SceneChanger : MonoBehaviour
{
    public RectTransform headingUiElement; // Assign the UI element in the inspector
    public Vector2 originalPosition;
    public Vector2 headingTargetPosition;
    public GameObject mainPanelShadow;
    public GameObject info;

    

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DisplayInfo()
    {
        if (headingUiElement != null)
        {
            headingUiElement.anchoredPosition = headingTargetPosition;
        }

        mainPanelShadow.SetActive(false);
        info.SetActive(true);

    }

    public void Back()
    {
        headingUiElement.anchoredPosition = originalPosition;
        mainPanelShadow.SetActive(true);
        info.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor, so we need to simulate a quit.
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // This will quit the game when running the built application.
            Application.Quit();
#endif
    }
}
