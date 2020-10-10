using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    private int speed = 7;
    public Text counter;
    public int kill=0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject.transform.parent.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(1);
        }
        kill = kill + 1;
        UnityEngine.Debug.Log(kill);
    }
}
