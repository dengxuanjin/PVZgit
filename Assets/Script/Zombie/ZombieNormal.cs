using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1, 0, 0);
    public float speed = 1;
    private bool isWalk;
    private Animator animator;

    public float damage;
    public float damageInterval = 0.5f;
    private float damageTimer=0;
    public float health = 50;
    private float currentHealth;
    private float lostHeadHealth =  50;
    private GameObject head;
    private bool lostHead;
    private bool isDie;

    private void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
        currentHealth = health;
        head = transform.Find("head").gameObject;
        lostHead = false;
        isDie = false;
    }
    private void Update()
    {
        if (isDie) return;
        Move();
    }
    private void Move()
    {
        if (isWalk)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void DieAniOver()
    {
        animator.enabled = false;
        GameObject.Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie) return;
        if (collision.tag == "Plant")
        {
            isWalk = false;
            animator.SetBool("Walk", false);
        }
    }
    /*
    项目里做个简单的地刺效果，发现站在地刺上前几秒会一直触发trigger，但是不动的情况下之后就再也不触发，看了官方文档也说会一直触发，很迷，搜了发现有点小坑。
    https://answers.unity.com/questions/973943/ontriggerstay2d-stops-working-randomly.html
    在projectSetting->Physic2dSettings里有个timetosleep
    默认值为0.5，在不满足下面两个线速度和角速度的下限情况下，为了节约性能就sleep了。改值即可。
     
     */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie) return;

        if (collision.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
            {
                damageTimer = 0;
                float newHealth = collision.gameObject.transform.GetComponentInParent<Plant>().ChangeHealth(damage);
                if (newHealth <= 0)
                {
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDie) return;
        if (collision.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk", true);
        }
    }

    public void ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth - num, 0, health);
        if(currentHealth <= lostHeadHealth && !lostHead)
        {
            lostHead = true;
            animator.SetBool("LostHead", true);
            head.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            GameManager.Instance.ZombieDied(gameObject);
            isDie = true;
        }
    }

}
