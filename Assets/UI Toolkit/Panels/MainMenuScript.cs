using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class MainMenuScript : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button startButton = root.Q<Button>("Start");
        Button creditsButton = root.Q<Button>("Credits");
        Button quitButton = root.Q<Button>("Quit");

        startButton.RegisterCallback<ClickEvent>(StartClicked);
        creditsButton.RegisterCallback<ClickEvent>(CreditsClicked);
        quitButton.RegisterCallback<ClickEvent>(QuitClicked);
    }

    public void StartClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void CreditsClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}
