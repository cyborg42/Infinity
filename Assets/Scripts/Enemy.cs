using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    public float blood = 100;
    private LevelManager levelScript;
    private bool lck;
    private int x, y;
    private int lastx, lasty;
    private int dirx, diry;
    private int lastdirx, lastdiry;
    private float randomx, randomy;
    private void Awake()
    {
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }
    private void Start()
    {
        lck = true;
        lastx = System.Convert.ToInt32(transform.position.x);
        lasty = System.Convert.ToInt32(transform.position.y);

    }
    private void FixedUpdate()
    {
        x = System.Convert.ToInt32(transform.position.x);
        y = System.Convert.ToInt32(transform.position.y);
        dirx = levelScript.direction[x, y, 0];
        diry = levelScript.direction[x, y, 1];
        if (x == lastx && y == lasty)
        {
            if(lastdirx!=dirx||lastdiry!=diry)
            {
                lck = false;
            }
            if (lck)
            {
                Vector3 dir = new Vector3(x + dirx - transform.position.x, y + diry - transform.position.y, 0);
                transform.Translate(dir.normalized * speed * Time.fixedDeltaTime);
            }
            else
            {
                Vector3 dir = new Vector3(x - transform.position.x, y - transform.position.y, 0);
                transform.Translate(dir.normalized * speed * Time.fixedDeltaTime);
                if (dir.magnitude < 0.05)
                {
                    transform.position = new Vector3(x, y, 0);    
                    lck = true;
                }
            }
        }
        else
        {
            lck = false;
        }
        lastx = x;
        lasty = y;
        lastdirx = dirx;
        lastdiry = diry;
    }

}
