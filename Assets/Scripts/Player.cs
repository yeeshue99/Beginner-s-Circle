using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal = 0;
    private float vertical = 0;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    [SerializeField]
    private float attackCooldown = 20.0f;
    private float lastAttack = 0;
    public GameObject laser;
    [SerializeField]
    private GameObject meleeHitbox;

    [SerializeField]
    private float runSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeHitbox.SetActive(false);
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCharacter();
        TurnCharacter();
        Fire();
        Attack();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }


    private void TurnCharacter()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mouse - transform.position;
        diff.Normalize();
        mouse.z = 0;
        // Get Angle in Radians
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void MoveCharacter()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    private void Fire()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mouse - transform.position;
        diff.Normalize();
        mouse.z = 0;
        // Get Angle in Radians
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (Input.GetButton("Fire1") && Time.time - lastAttack > attackCooldown)
        {
            GameObject temp = Instantiate(laser, transform.position, transform.rotation, transform);
            temp.transform.parent = null;
            temp.transform.localScale = Vector3.one;
            lastAttack = Time.time;
        }
    }

    private void Attack()
    {
        if (Input.GetButton("Fire2") && Time.time - lastAttack > attackCooldown)
        {
            StartCoroutine(MeleeAttack());
        }

    }

    private IEnumerator MeleeAttack()
    {
        meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(.1f);
        meleeHitbox.SetActive(false);
    }

}
