using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject optionsScreen, pauseScreen, loadingScreen,loadingIcon;
    public GameObject player;
    public string mainMenuScene = "Main Menu";
    private bool Paused;
    public Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if (!Paused)
        {
            player.GetComponent<PlayerControl>().UnlockCursor();
            pauseScreen.SetActive(true);
            Paused = true;
            Time.timeScale = 0f;
        }
        else
        {
            player.GetComponent<PlayerControl>().LockCursor();
            pauseScreen.SetActive(false);
            Paused = false;
            Time.timeScale = 1f;
        }

    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitToMain()
    {
        //SceneManager.LoadScene(mainMenuScene);
        //Time.timeScale = 1f;
        StartCoroutine(LoadMain());
    }

    public IEnumerator LoadMain()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= .9f)
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
    public void Restart()
    {
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(LoadCurrent());
    }
    public IEnumerator LoadCurrent()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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
