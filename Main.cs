using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public string road = "The road";
    public string river = "The river";
    public string forest = "The forest";
    public string newScene;
    public Vector3 SpotInScene;
    public Vector3 RotationInScene;
    Collider other;
    Scene scene;
    public GameObject loadingScreen, loadingIcon, player;
    public Text loadingText;
    public GameObject choiceScreen;
    public int time = 15;
    public Text countText;
    float random;
    public bool isDeer = false;
    public AudioSource crash;


    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0f, 1f);
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeer)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                choiceScreen.SetActive(false);
                player.GetComponent<PlayerControl>().LockCursor();
                player.GetComponent<PlayerControl>().SetInjured(false);
                player.GetComponent<PlayerControl>().SetCrashSite(river);
                StartCoroutine(LoadRiver());
                crash.Play();
                //countText.text = "Key pressed";
                isDeer = false;
                //break;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                choiceScreen.SetActive(false);
                player.GetComponent<PlayerControl>().LockCursor();
                player.GetComponent<PlayerControl>().SetInjured(false);
                player.GetComponent<PlayerControl>().SetCrashSite(forest);
                StartCoroutine(LoadForest());
                crash.Play();
                //countText.text = "KeyPressed";
                isDeer = false;
                //break;
            }

        }
    }

   IEnumerator DeerChoise()
    {
        while (time > 0)
        {
            //player.GetComponent<PlayerControl>().DisableMovement();

            player.GetComponent<PlayerControl>().DisableMovement();
            //checking the players choise
            //countText.text = time.ToString();
            isDeer = true;

            yield return new WaitForSeconds(1f);
            time--;
        }
        if (time <= 0)
        {
            isDeer = false;
            countText.text = "";
            player.GetComponent<PlayerControl>().LockCursor();
            if (random == 1)
            {
                choiceScreen.SetActive(false);
                player.GetComponent<PlayerControl>().LockCursor();
                player.GetComponent<PlayerControl>().SetInjured(true);
                player.GetComponent<PlayerControl>().SetCrashSite(river);
                StartCoroutine(LoadRiver());


            }
            else
            {
                choiceScreen.SetActive(false);
                player.GetComponent<PlayerControl>().LockCursor();
                player.GetComponent<PlayerControl>().SetInjured(true);
                player.GetComponent<PlayerControl>().SetCrashSite(forest);
                StartCoroutine(LoadForest());

            }
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //checks if road because it has only one trigger
            if (string.Equals(road, scene.name))
        {
                isDeer = true;
                StartCoroutine(DeerChoise());
                player.GetComponent<PlayerControl>().UnlockCursor();
                choiceScreen.SetActive(true);
                
        }
        else
        {
            StartCoroutine(LoadOther());
        }
    }
    }

    //loands river scene with loading screen
    public IEnumerator LoadRiver()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(river);
        asyncLoad.allowSceneActivation = false;
        //PositionHandler();
        GameManager.Instance.SpawnLocation = new Vector3(838.5f, 202.6f, 548.9f);
        GameManager.Instance.SpawnRotation = new Vector3(0, -93.9f, 0);
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

    //loads forest scene with loading screen
    public IEnumerator LoadForest()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(forest);
        asyncLoad.allowSceneActivation = false;
        //PositionHandler();
        GameManager.Instance.SpawnLocation = new Vector3(431, 7.65f, 456);
        GameManager.Instance.SpawnRotation = new Vector3(0, -89, 4);
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

    //loads specified scene with loading screen
    public IEnumerator LoadOther()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene);
        asyncLoad.allowSceneActivation = false;
        PositionHandler();
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

    public void PositionHandler()
    {
        GameManager.Instance.SpawnLocation = SpotInScene;
        GameManager.Instance.SpawnRotation = RotationInScene;
    }

    public void DodgeLeft()
    {
        choiceScreen.SetActive(false);
        player.GetComponent<PlayerControl>().LockCursor();
        player.GetComponent<PlayerControl>().SetInjured(false);
        StartCoroutine(LoadRiver());
        crash.Play();

    }

    public void DodgeRight()
    {
        choiceScreen.SetActive(false);
        player.GetComponent<PlayerControl>().LockCursor();
        player.GetComponent<PlayerControl>().SetInjured(false);
        StartCoroutine(LoadForest());
        crash.Play();

    }

}
