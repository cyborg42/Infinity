using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
  
    private LevelManager levelScript; 

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
    }
    void Start()
    {
        InitGame();
    }
    void InitGame()
    {
        levelScript.SetupScene(0);
    }
 
}
