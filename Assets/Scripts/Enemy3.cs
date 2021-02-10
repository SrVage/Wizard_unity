using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy3 : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int hp = 10; //здоровье врага
    private Rigidbody rb;
    
    private void FixedUpdate()
    {
       
    }
    
    public void TakeDamage (int damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
    }

    public void Force(GameObject bomb)
    {
    }

    private void Death()
    {
        Destroy(gameObject);
        Application.Quit();
    }
  }