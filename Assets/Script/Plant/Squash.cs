using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquashState
{
    None,
    Down,
    Up,
    Over
}
public class Squash : Plant
{

    private Vector3 attackPos = Vector3.zero;
    private int line;
    [SerializeField] private float findZombieDistance;
    [SerializeField] private int Damage;
    private SquashState squashState = SquashState.None;
    protected override void Start()
    {
        base.Start();

    }
    public override float ChangeHealth(float num)
    {
        return health;

    }
    public override void SetPlantStart()
    {
        base.SetPlantStart();

        m_animator.speed = 1;
        boxCollider2D.enabled = false;
        line = GameManager.Instance.GetPlantLine(gameObject);
        
        InvokeRepeating("CheckZombieInRange", 1, 0.5f);


    }
    private void Update()
    {
        switch (squashState)
        {
            case SquashState.None:
                break;
            case SquashState.Down:
                break;
            case SquashState.Up:
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(attackPos.x, attackPos.y + 100), Time.deltaTime * 200);

                break;
            case SquashState.Over:
                transform.position = Vector2.MoveTowards(transform.position, attackPos, Time.deltaTime * 200);

                break;
            default:
                break;
        }
    }
    public void CheckZombieInRange()
    {

        if(attackPos != Vector3.zero)
        {
            return;
        }

        List<GameObject> zombies = GameManager.Instance.GetLineZombies(line);
        Debug.Log(zombies.Count);
        if (zombies.Count < 0) return;
        float minDis = findZombieDistance;
        GameObject nearZOmbie = null;
        for(int i=0; i<zombies.Count; i++)
        {
            GameObject zombie = zombies[i];
            float dis = Vector2.Distance(gameObject.transform.position, zombie.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearZOmbie = zombie;
            }
        }
        if(nearZOmbie == null)
        {
            return;

        }
        Debug.Log(nearZOmbie);
        attackPos = nearZOmbie.transform.position;
        DoSquashLook();
    }

    public void DoSquashLook()
    {
        string LookName = "Right";
        if (attackPos.x < transform.position.x)
        {
            LookName = "Left";
        }
        m_animator.SetTrigger(LookName);
    }
    public void SetAttackUp()
    {
        squashState = SquashState.Up;
        m_animator.SetTrigger("AttackUp");
        Debug.Log("tiao");

    }

    public void SetAttackOver()
    {
        squashState = SquashState.Over;
        m_animator.SetTrigger("AttackOver");
        Debug.Log("za");
    }
    public void DoReallyAttack()
    {
        boxCollider2D.enabled = true;
        GameObject.Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            collision.GetComponent<ZombieNormal>().ChangeHealth(Damage);
        }

    }
}
