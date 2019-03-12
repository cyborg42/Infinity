using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int columns = 21;
    public int rows = 31;
    public int healthPoint = 100;
    public GameObject[] floors;
    public GameObject[] enemys;
    public GameObject[] towers;
    public GameObject wall;
    public GameObject enter;
    public GameObject exit;
    [HideInInspector]
    public int[,] land;
    [HideInInspector]
    public int[,,] direction;
    private GameObject[,] floorBlocks;
    private Transform boardHolder;

    private PathManager pathScript;
    void Awake()
    {
        pathScript = GetComponent<PathManager>();
    }
    
    void levelInit()
    {
        boardHolder = new GameObject ("Board").transform;
        land = new int[rows, columns];
        direction = new int[rows, columns, 2];
        floorBlocks = new GameObject[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                land[x, y] = -1;
            }
        }
        for (int x = 1; x < rows - 1; x++)
        {
            for (int y = 1; y < columns - 1; y++)
            {
                land[x, y] = 0;
            }
        }
        land[0, columns / 2] = 1;
        land[rows - 1, columns / 2] = -2;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject toInstantiate;
                switch (land[x, y])
                {
                    case -1:
                        toInstantiate = wall;
                        break;
                    case 0:
                        toInstantiate = floors[(x + y) % 2];
                        break;
                    case 1:
                        toInstantiate = enter;
                        break;
                    case -2:
                        toInstantiate = exit;
                        break;
                    default:
                        toInstantiate = enter;
                        break;
                }
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                floorBlocks[x, y] = instance;
                instance.transform.SetParent(boardHolder);
            }
        }
        land[0, columns / 2] = -1;
        land[1, columns / 2] = 1;
        land[rows - 1, columns / 2] = -1;
        pathScript.FindPath();
    }
    public void SetupScene(int level)
    {
        levelInit();
    }
    private void FixedUpdate()
    {
        
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = System.Convert.ToInt32(mouse.x);
            int y = System.Convert.ToInt32(mouse.y);
            if (x > 0 && x < rows - 1 && y > 0 && y < columns - 1)
            {
                floorBlocks[x, y].GetComponent<Floor>().MouseDown();
            }

        }
        
    }
}