using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private LevelManager levelScript;
    private PathManager pathScript;
    private int x, y;
    private void Awake()
    {
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
        pathScript = GameObject.Find("GameManager").GetComponent<PathManager>();
    }
    private void Start()
    {
        x = System.Convert.ToInt32(transform.position.x);
        y = System.Convert.ToInt32(transform.position.y);
    }
    
    public void MouseDown()
    {
;
        print(levelScript.land[x, y]);
        if (levelScript.land[x, y] != 0) return;
        levelScript.land[x, y] = -1;
        pathScript.FindPath();
        if(pathScript.IsLegal())
        {
            Instantiate(levelScript.towers[0], transform.position, Quaternion.identity);
        }
        else
        {
            levelScript.land[x, y] = 0;
            pathScript.FindPath();
        }
    }
}
