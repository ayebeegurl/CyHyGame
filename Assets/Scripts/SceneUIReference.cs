using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneUIReference : MonoBehaviour
{
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public Slider scoreProgressBar;


    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPanels(correctPanel, wrongPanel);
            GameManager.Instance.RegisterProgressBar(scoreProgressBar);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
