using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private LevelManager levelScript;
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
    private void Awake()
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
            }
        }
        distance[rows - 1, colums / 2] = 0;
        dfs();
    }
    void dfs()
    {
        Queue q = new Queue();
        q.Enqueue(new position(rows - 1, colums / 2));

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
}
