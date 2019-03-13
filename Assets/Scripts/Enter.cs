using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    private LevelManager levelScript;
    public float waveTime = 10f, enemyTime = 0.3f;
    private float waveTimer, enemyTimer;
    private int wave,enemyNum;
    private GameObject curEnemy;

    void Awake()
    {
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }
    private void Start()
    {
        wave = 0;
        waveTimer = waveTime;
        enemyTimer = enemyTime;
        enemyNum = 10;
        curEnemy = levelScript.enemys[0];
    }
    private void Update()
    {
        if(enemyNum>0)
        {
            enemyTimer -= Time.deltaTime;
            if(enemyTimer<=0f)
            {
                enemyNum--;
                enemyTimer = enemyTime;
                Instantiate(curEnemy, transform);
            }
        }
        else
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                wave++;
                curEnemy = levelScript.enemys[wave % levelScript.enemys.Length];
                enemyNum = 10 + wave;
                waveTimer = waveTime;
            }
        }
    }

}
