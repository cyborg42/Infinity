﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager levelScript; 
    public static GameManager instance = null;
  

    void Awake()
    {
        if (instance == null)
            instance = this;
        if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        levelScript = GetComponent<LevelManager>();
        InitGame();
    }
    void InitGame()
    {
        levelScript.SetupScene(0);
    }
 
}
