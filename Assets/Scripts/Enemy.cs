using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health = 1;
    private int damage = 1;
    private int speed = 2;

    private enum enemyType
    {
        DEFAULT,
        melee,
        flying,
        mage,
        bruiser
    };
    [SerializeField]
    private enemyType type;

    [SerializeField]
    private Base playerBase;

    public ScoreHandler scoreHandler;

    // AI Section
    private float distanceFromBase = 0.00f;

    private void Awake()
    {
        if(type == enemyType.DEFAULT)
        {
            type = (enemyType)UnityEngine.Random.Range(1, (uint)System.Enum.GetValues(typeof(enemyType)).Length);
        }

        switch (type)
        {
            case enemyType.melee:
                break;
            case enemyType.flying:
                distanceFromBase = 3.0f;
                break;
            case enemyType.mage:
                distanceFromBase = 3.0f;
                break;
            case enemyType.bruiser:
                distanceFromBase = 3.0f;
                break;
            default:
                break;
        }
    }

    void Start()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveTowardsBase();
        if(Vector3.Distance(transform.position, playerBase.transform.position) <= distanceFromBase)
        {
            //DealDamageToBase();
            DoAction();
        }
    }

    private void DoAction()
    {
        //throw new NotImplementedException();
        switch (type)
        {
            case enemyType.melee:
                break;
            case enemyType.flying:

                break;
            case enemyType.mage:

                break;
            case enemyType.bruiser:

                break;
            default:
                break;
        }
    }

    private void MoveTowardsBase()
    {
        Vector3 directionTowardsBase = Vector3.Normalize(playerBase.transform.position - transform.position);
        transform.Translate(directionTowardsBase * Time.deltaTime * speed);
    }

    private void DealDamageToBase()
    {
        playerBase.takeDamage(damage);
        Destroy(gameObject);
    }

    public void TakeDamage(int damageToDeal)
    {
        health -= damageToDeal;
        if (health == 0)
        {
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
        if (collision.gameObject.CompareTag("Base"))
        {
            DealDamageToBase();
        }
    }
}
