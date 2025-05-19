using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject correctPanel;
    public GameObject wrongPanel;
    private Slider scoreProgressBar;

    public int score = 0;
    public int maxScore = 10;
    public int wrongAnswers = 0; 


    // Start is called before the first frame update
    void Start()
    {
        if (correctPanel != null)
            correctPanel.SetActive(false);

        if (wrongPanel != null)
            wrongPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; 

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPanels(GameObject correct, GameObject wrong)
    {
        correctPanel = correct;
        wrongPanel = wrong;

        if (correctPanel != null)
            correctPanel.SetActive(false);
        if (wrongPanel != null)
            wrongPanel.SetActive(false);
    }
    public void RegisterProgressBar(Slider slider)
    {
        scoreProgressBar = slider;
        if (scoreProgressBar != null)
        {
            scoreProgressBar.maxValue = maxScore;
            scoreProgressBar.value = score;
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        score = Mathf.Clamp(score, 0, maxScore);
        StartCoroutine(UpdateProgressBarWithDelay(0.5f));
    }

    private IEnumerator UpdateProgressBarWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (scoreProgressBar != null)
        {
            scoreProgressBar.value = score;
        }
    }
    public void ShowCorrect()
    {
        AddScore(1);
        Debug.Log("Correct! Score is now: " + score);
        StartCoroutine(ShowPanelWithDelay(correctPanel, 0.5f));

    }

    public void ShowWrong()
    {
        AddScore(0);
        wrongAnswers++;
        Debug.Log("Wrong! Score is now: " + score);
        StartCoroutine(ShowPanelWithDelay(wrongPanel, 0.5f));

    }
    private IEnumerator ShowPanelWithDelay(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (panel != null)
            panel.SetActive(true);
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int totalScenes = SceneManager.sceneCountInBuildSettings;


        if (nextSceneIndex == totalScenes - 1) 
        {
            SceneManager.LoadScene("EndScene");
        }
        else if (nextSceneIndex < totalScenes)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
    public void GoToMainMenu()
    {

        SceneManager.LoadScene("Menu");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level 1")
        {
            score = 0;
            wrongAnswers = 0;

            if (scoreProgressBar != null)
                scoreProgressBar.value = 0;

            Debug.Log("GameManager: Score and wrongAnswers reset because Level1 loaded.");
        }
        GameObject[] nextButtons = GameObject.FindGameObjectsWithTag("NextButton");

        foreach (GameObject btnObj in nextButtons)
        {
            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(LoadNextScene);
            }
        }

        GameObject[] menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        foreach (GameObject btnObj in menuButtons)
        {
            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(GoToMainMenu);
            }
        }
        if (correctPanel != null)
            correctPanel.SetActive(false);
        if (wrongPanel != null)
            wrongPanel.SetActive(false);

        GameObject card = GameObject.FindWithTag("Card");
        if (card != null && card.TryGetComponent(out CardAnimation entrance))
        {
            entrance.AnimateEntrance();
        }

    }
}