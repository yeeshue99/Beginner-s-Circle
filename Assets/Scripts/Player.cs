using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal = 0;
    private float vertical = 0;

    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthbar;
    public SceneChanger sc;

    PlayerInput input;
    public Animator animator;
    public Text ammoText;
    public int ammo = 20;
    public Animator ammoAnimator;
    public GameObject pauseMenu;


    [SerializeField]
    private float attackCooldown = 0.25f;
    private enum Weapon { melee, ranged};
    private Weapon currentWeapon = Weapon.ranged;
    private float lastAttack = 0;
    public GameObject laser;
    public ScoreHandler scoreHandler;
    [SerializeField]
    private GameObject meleeHitbox;

    [SerializeField]
    private float runSpeed = 5f;
    // Start is called before the first frame update

    private void Awake()
    {
        input = new PlayerInput();
        //input.Player.Attack.performed += ctx => Attack();
        input.Player.ChangeWeapon.performed += ctx => ChangeWeapon();
        input.Player.Pause.performed += ctx => PauseMenu();
        
    }
    public void OnEnable()
    {
        input.Enable();
    }

    public void OnDisable()
    {
        input.Disable();
    }


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
        if(currentHealth <= 0)
        {
            sc.ScreenLoader("Game Over");
        }
        healthbar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //var gamepad = Gamepad.current;
        MoveCharacter();
        TurnCharacter();
        Attack();
        ammoText.text = "x " + ammo.ToString("00");
    }


    private void TurnCharacter()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 diff = mouse - transform.position;
        diff.Normalize();
        Vector3 temp = new Vector3(diff.x / Mathf.Abs(diff.x), diff.y / Mathf.Abs(diff.y), diff.z / Mathf.Abs(diff.z));
        if (diff.x < 0.25 && diff.x > -0.25)
        {
            animator.SetInteger("Horizontal", 0);
        }
        else
        {
            animator.SetInteger("Horizontal", (int)temp.x);
        }
        if (diff.y < 0.25 && diff.y > -0.25)
        {
            animator.SetInteger("Vertical", 0);
        }
        else
        {
            animator.SetInteger("Vertical", (int)temp.y);
        }
        if(horizontal == 0 && vertical == 0)
        {
            animator.SetInteger("Horizontal", 0);
            animator.SetInteger("Vertical", 0);
        }

        
        
        // Get Angle in Radians
        //float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        // Rotate Object
        //transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        //healthbar.transform.parent.transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveCharacter()
    {
        horizontal = input.Player.HorizontalAxis.ReadValue<float>();
        vertical = input.Player.VerticalAxis.ReadValue<float>();

        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    private void ChangeWeapon()
    {
        Debug.Log(input.Player.ChangeWeapon.ReadValue<float>());
        uint temp = (uint)currentWeapon;
        temp += 1;//(uint)Input.GetAxis("SwapWeapon");
        temp %= (uint)System.Enum.GetValues(typeof(Weapon)).Length;
        currentWeapon = (Weapon)temp;
    }

    private void Attack()
    {
        if (input.Player.Attack.ReadValue<float>() == 1 && Time.time - lastAttack > attackCooldown)
        {

            switch (currentWeapon)
            {
                case Weapon.melee:
                    StartCoroutine(MeleeAttack());
                    break;
                case Weapon.ranged:
                    Fire();
                    break;
                default:
                    break;
            }
        }

    }

    private IEnumerator MeleeAttack()
    {
        meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(.1f);
        meleeHitbox.SetActive(false);
        yield break;
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 0.02f;
        }

    }

    private void Fire()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 diff = mouse - transform.position;
        diff.Normalize();
        mouse.z = 0;
        // Get Angle in Radians
        float angle = Mathf.Atan2(animator.GetInteger("Horizontal"), animator.GetInteger("Vertical")) * Mathf.Rad2Deg;

        if (Time.time - lastAttack > attackCooldown)
        {
            ammoAnimator.ResetTrigger("Reload");
            GameObject temp = Instantiate(laser, transform.position, Quaternion.Euler(new Vector3(0, 0, -angle)));
            //temp.GetComponent<SendScore>().;
            temp.transform.parent = null;
            temp.transform.localScale = Vector3.one * 2;
            lastAttack = Time.time;
            ammo--;
            if(ammo <= 0)
            {
                ammo = 20;
                lastAttack += .5f;
                ammoAnimator.SetTrigger("Reload");
            }
        }
    }
}
