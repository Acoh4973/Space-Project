using UnityEngine;
using UnityEngine.AI;

public class EnemyAiSureShot : MonoBehaviour , iDamage
{
    [SerializeField] int HP;
    [SerializeField] int Rank;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform AimOffset1;
    [SerializeField] Transform AimOffset2;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] int FOV;
    [SerializeField] GameObject[] Powerups;

    float angleToPlayer;
    Vector3 playerDir;
    float shootTimer;
    float moveDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnManager.instance.aliveEnemies++;
        Agent.SetDestination(GameManager.instance.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        movement();
        shoot();
    }

    void shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= 3 - (0.5 * Rank))
        {
            shootTimer = 0;
            if (Rank < 3) Instantiate(Bullet, transform.position, transform.rotation);
            if (Rank == 3)
            {
                Vector3 directionToOff1 = AimOffset1.position - transform.position;
                Vector3 directionToOff2 = AimOffset2.position - transform.position;
                Quaternion offset1 = Quaternion.LookRotation(directionToOff1);
                Quaternion offset2 = Quaternion.LookRotation(directionToOff2);
                Instantiate(Bullet, transform.position, offset1);
                Instantiate(Bullet, transform.position, transform.rotation);
                Instantiate(Bullet, transform.position, offset2);
            }
        }
    }

    void movement()
    {
        moveDelay += Time.deltaTime;
        if (moveDelay >= 10 - Rank)
        {
            moveDelay = 0;
            Agent.SetDestination(GameManager.instance.player.transform.position);
        }

    }

    void lookAtPlayer()
    {
        playerDir = GameManager.instance.player.transform.position - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);
        Quaternion Target = Quaternion.LookRotation(playerDir);
        transform.rotation = Target;
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            SpawnManager.instance.aliveEnemies--;
            GameManager.instance.XP += 1;
            dropPowerup();
            Destroy(gameObject);
        }
    }

    void dropPowerup()
    {
        int Chance = Random.Range(0, 5);
        if (Chance <= 2)
        {
            Instantiate(Powerups[Chance], transform.position, transform.rotation);
        }
    }
}

