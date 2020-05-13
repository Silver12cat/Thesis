using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel = "The Road";
    public GameObject optionScreen, loadingScreen, loadingIcon, creditScreen;
    public Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        //SceneManager.LoadScene(firstLevel);
        StartCoroutine(LoadFirst());
    }

    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        creditScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        creditScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadFirst()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(firstLevel);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f)
            {
                loadingIcon.SetActive(false);
                loadingText.text = "Press any key to continue";
            }
            if (Input.anyKeyDown)
            {
                asyncLoad.allowSceneActivation = true;

                Time.timeScale = 1f;
            }
            yield return null;
        }
    }
}
