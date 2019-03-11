using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = (float)5;
    private List<GameObject> enemys = new List<GameObject>();

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemys.Remove(collision.gameObject);
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
        GameObject enemy = enemys[0];
      
        float angle = Vector3.Angle(Vector3.up, enemy.transform.position - transform.position);
        if (enemy.transform.position.x > transform.position.x) angle = -angle;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
