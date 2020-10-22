using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birthdayshow : MonoBehaviour
{
    public Animator animator;
    public void ShowBirthday()
    {
        animator.SetTrigger("Birthday");
        Debug.Log("Button");
    }
}
