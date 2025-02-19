using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CutsceneScript : MonoBehaviour
{
    private VisualElement background;
    private VisualElement plate;
    private VisualElement spaghetti;
    private VisualElement chef;
    private VisualElement toxic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        background = root.Q<VisualElement>("Background");
        plate = root.Q<VisualElement>("Plate");
        spaghetti = root.Q<VisualElement>("Spaghetti");
        chef = root.Q<VisualElement>("Chef");
        toxic = root.Q<VisualElement>("Toxic");

        StartCoroutine(Play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Play()
    {
        float size = 600f;
        float endSize = 2000f;

        DOTween.To(() => 600f, x => 
            {
                size = x;
                plate.style.width = size;
                plate.style.height = size;
            }, endSize, 1.5f);

        yield return null;
    }
}
