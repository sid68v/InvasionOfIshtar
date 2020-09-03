using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextLevel", 4f);

    }
    void LoadNextLevel()
    {
        Debug.Log("Next Level Loaded.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
