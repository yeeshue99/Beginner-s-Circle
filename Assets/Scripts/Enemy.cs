﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health = 1;
    private int damage = 1;
    private int speed = 2;

    public AudioSource death;

    [SerializeField]
    private Base playerBase;

    public ScoreHandler scoreHandler;
    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTowardsBase();
        if(Vector3.Distance(transform.position, playerBase.transform.position) <= 0.5)
        {
            dealDamageToBase();
        }
    }

    private void moveTowardsBase()
    {
        Vector3 directionTowardsBase = Vector3.Normalize(playerBase.transform.position - transform.position);
        transform.Translate(directionTowardsBase * Time.deltaTime * speed);
    }

    private void dealDamageToBase()
    {
        playerBase.takeDamage(damage);
        Destroy(gameObject);
    }

    public void takeDamage(int damageToDeal)
    {
        health -= damageToDeal;
        if (health == 0)
        {
            death.Play();
            scoreHandler.changeScore(1);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
