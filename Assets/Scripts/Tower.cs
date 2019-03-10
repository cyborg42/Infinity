using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = (float)5;
    private List<GameObject> enemys = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemys.Add(collision.gameObject);
        Debug.Log("enter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemys.Remove(collision.gameObject);
        Debug.Log("exit");
    }

    void Update()
    {
        if (enemys.Count == 0)
        {
            return;
        }
        while(!enemys[0])
        {
            enemys.RemoveAt(0);
        }
        if (enemys.Count == 0)
        {
            return;
        }
        Debug.Log(enemys[0].name);
    }
}
