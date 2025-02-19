using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator chefAnimator;
    [SerializeField] private Animator toxicAnimator;
    [SerializeField] private Animator spaghettiAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine(PlayCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(11f);
        Debug.Log("Time");
        SceneManager.LoadScene("Boss-fight");
    }
}
