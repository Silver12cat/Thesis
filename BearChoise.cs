using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BearChoise : MonoBehaviour
{
    public GameObject  choiseScreen, player, loadingScreen, loadingIcon, deadScreen, endScreen;
    public Vector3 SpotInScene1;
    public Vector3 RotationInScene1;
    Scene scene;
    public Text loadingText, deadText;
    public AudioSource End, Dead, gun, Main, Desicion, bear;
    public bool isBear = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBear)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                GameManager.Instance.SpawnLocation = SpotInScene1;
                GameManager.Instance.SpawnRotation = RotationInScene1;
                choiseScreen.SetActive(false);
                Desicion.Stop();
                Main.Play();
                isBear = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (player.GetComponent<PlayerControl>().GetInjured())
                {
                    choiseScreen.SetActive(false);
                    Desicion.Stop();
                    Dead.Play();
                    StartCoroutine(LoadMain());
                    isBear = false;
                }
                else
                {
                    choiseScreen.SetActive(false);
                    Desicion.Stop();
                    gun.Play();
                    End.Play();
                    bear.Stop();
                    StartCoroutine(LoadEnd());
                    isBear = false;
                }
            }
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            choiseScreen.SetActive(true);
            Main.Stop();
            Desicion.Play();
            player.GetComponent<PlayerControl>().UnlockCursor();
            isBear = true;
        }
    }

    public IEnumerator LoadEnd()
    {
        endScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");
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
    public IEnumerator LoadMain()
    {
        deadScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f)
            {
                
                loadingIcon.SetActive(false);
                deadText.text = "Press any key to continue";
            }

            if (Input.anyKeyDown)
            {
                asyncLoad.allowSceneActivation = true;

                Time.timeScale = 1f;
            }
            yield return null;
        }

    }
    public void Flee()
    {
        GameManager.Instance.SpawnLocation = SpotInScene1;
        GameManager.Instance.SpawnRotation = RotationInScene1;
        choiseScreen.SetActive(false);
        Main.Play();
        Desicion.Stop();
        isBear = false;

    }

    public void Encounter()
    {
        isBear = false;
        if (player.GetComponent<PlayerControl>().GetInjured())
        {
            choiseScreen.SetActive(false);
            StartCoroutine(LoadMain());
            Desicion.Stop();
            Dead.Play();

        }
        else
        {
            choiseScreen.SetActive(false);
            Desicion.Stop();
            gun.Play();
            End.Play();
            bear.Stop();
            StartCoroutine(LoadEnd());
        }
    }
}
