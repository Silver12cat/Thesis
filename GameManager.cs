using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 SpawnLocation = new Vector3(549.71f, 7.65f, 352.27f);
    public Vector3 SpawnRotation = new Vector3(0, -89.46f, 0);

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
