using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 Dir;
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    public bool torchwoodCreate;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Dir * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            collision.GetComponent<ZombieNormal>().ChangeHealth(Damage);
            DestroyBullet();
        }

    }
    public virtual void DestroyBullet()
    {
        GameObject.Destroy(gameObject);
    }
}
