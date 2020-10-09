using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private int health = 1;

    private GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameHandler.setGameover(true);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
