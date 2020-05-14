using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameObject loadingScreen, loadingIcon, player;
    public Text loadingText, endText;
    public string mainMenuScene = "Main Menu";
    Collider other;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {

        //checks if road because it has only one trigger
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            StartCoroutine(LoadMain());

        }
    }

    public IEnumerator LoadMain()
    {
        loadingScreen.SetActive(true);
        loadingIcon.SetActive(false);
        //endText.text = "End 2: The Village";
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);
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
        //endText.text = " ";
    }
}