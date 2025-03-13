using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBasics : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField]  private int speed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void Attack(EntityBasics target)
    {
        target.TakeDamage(attack);
    }
}
