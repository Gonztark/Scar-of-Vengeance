using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
	
	// Update is called once per frame
	void Start () {
        currentHealth = maxHealth;
		
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        animator.SetBool("IsDead", true);
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Access the Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // If you want the enemy to stop all physics interaction (freeze in place)
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        this.enabled = false;

    }
}
