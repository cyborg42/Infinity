using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private LevelManager levelScript;
    private void Awake()
    {
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        levelScript.healthPoint--;
    }
}
