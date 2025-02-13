using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BackFromCredits : MonoBehaviour
{
    public void BackClicked()
    {
        SceneManager.LoadScene("Main-Menu");
    }
}
