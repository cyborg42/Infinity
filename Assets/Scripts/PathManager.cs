using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private int[,] distance;
    private int rows, colums;
    private const int inf = 0x3f3f3f3f;
    private int[,] dirs = new int[8, 2]
    {
        {0,1},{1,0},{0,-1},{-1,0},{1,1},{1,-1},{-1,1},{-1,-1}
    };
    private class position
    {
        public int x, y;
        public position(int first, int secound)
        {
            this.x = first;
            this.y = secound;
        }
    }

    private LevelManager levelScript;
    void Awake()
    {
        levelScript = GetComponent<LevelManager>();
        rows = levelScript.rows;
        colums = levelScript.columns;
        distance = new int[rows,colums];
    }
    public void FindPath()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < colums; y++)
            {
                distance[x, y] = inf;
                levelScript.direction[x, y, 0] = 0;
                levelScript.direction[x, y, 1] = 0;
            }
        }
        if (levelScript.land[rows - 2, colums / 2] < 0)
        {
            return;
        }
        levelScript.direction[0, colums / 2, 0] = 1;
        levelScript.direction[0, colums / 2, 1] = 0;
        levelScript.direction[rows - 2, colums / 2, 0] = 1;
        levelScript.direction[rows - 2, colums / 2, 1] = 0;
        distance[rows - 2, colums / 2] = 1;
        dfs();
        printPath();
    }
    void dfs()
    {
        Queue q = new Queue();
        q.Enqueue(new position(rows - 2, colums / 2));

     
        while (q.Count > 0)
        {
            position cur = q.Dequeue() as position;
            for (int i = 0; i < 8; i++)
            {
                int dx = dirs[i, 0];
                int dy = dirs[i, 1];

                if (levelScript.land[cur.x + dx, cur.y + dy] < 0 || distance[cur.x + dx, cur.y + dy] < inf) continue;
                if (i >= 4 && (levelScript.land[cur.x + dx, cur.y] < 0 || levelScript.land[cur.x, cur.y + dy] < 0)) continue;
                distance[cur.x + dx, cur.y + dy] = distance[cur.x, cur.y] + 1;
                levelScript.direction[cur.x + dx, cur.y + dy, 0] = -dx;
                levelScript.direction[cur.x + dx, cur.y + dy, 1] = -dy;
                if(cur.x+dx == rows - 2 && cur.y + dy == colums / 2) { Debug.Log("aaaaa"); }
                q.Enqueue(new position(cur.x + dx, cur.y + dy));
            }
        }
    }
    public bool isLegal()
    {
        bool legal = true;
        for (int x = 1; x < rows - 1; x++)
        {
            for (int y = 1; y < colums - 1; y++)
            {
                if(distance[x,y]==inf&&levelScript.land[x,y]>0)
                {
                    legal = false;
                }
            }
        }
        return legal;
    }
    void printPath()
    {

        string tmp="";
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < colums; y++)
            {
                tmp = tmp + levelScript.direction[x, y, 0] + " " + levelScript.direction[x, y, 1] + "   ";
            }
            tmp = tmp + "\n";
        }
        Debug.Log(tmp);
        tmp = "";
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < colums; y++)
            {
                tmp = tmp + distance[x, y] + " "; 
            }
            tmp = tmp + "\n";
        }
        Debug.Log(tmp);
    }
}
