using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] protected float health;
    protected float currentHealth;

    protected bool start;
    protected Animator m_animator;
    protected BoxCollider2D boxCollider2D;
    protected virtual void Start()
    {
        currentHealth = health;
        start = false;
        m_animator = transform.GetComponent<Animator>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        m_animator.speed = 0;
        boxCollider2D.enabled = false;
    }

    public virtual float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth - num, 0, health);
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
    public virtual void SetPlantStart()
    {
        start = true;
        m_animator.speed = 1;
        boxCollider2D.enabled = true;
    }
}