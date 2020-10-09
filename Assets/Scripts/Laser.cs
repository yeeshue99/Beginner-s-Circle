using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int speed = 2;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(1);
        }
    }
}
