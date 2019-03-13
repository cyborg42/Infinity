using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private LevelManager levelScript;
    private PathManager pathScript;
    private GameObject tower;
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

        if (levelScript.land[x, y] > 0) return;
        if (levelScript.land[x, y] < 0)
        {
            if(tower)
            {
                Destroy(tower);
            }
            levelScript.land[x, y] = 0;
            pathScript.FindPath();
            return;
        }
        levelScript.land[x, y] = -1;
        pathScript.FindPath();
        if(pathScript.IsLegal())
        {
            tower = Instantiate(levelScript.towers[0], transform);
        }
        else
        {
            levelScript.land[x, y] = 0;
            pathScript.FindPath();
        }
    }
}
