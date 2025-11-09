using UnityEngine;
using UnityEngine.AI;

public class EnemyAiStriker : MonoBehaviour, iDamage
{
    [SerializeField] int HP;
    [SerializeField] int Rank;
    float SpeedOrig;
    float chargeSpeed;
    float turnSpeedOrig;
    float turnSpeedCharge;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject DeathBullet;
    [SerializeField] GameObject[] Powerups;

    [SerializeField] Transform AimOffset1;
    [SerializeField] Transform AimOffset2;
    [SerializeField] Transform AimOffset3;
    [SerializeField] Transform AimOffset4;
    [SerializeField] Transform AimOffset5;
    [SerializeField] Transform AimOffset6;
    [SerializeField] Transform AimOffset7;
    [SerializeField] Transform AimOffset8;

    [SerializeField] NavMeshAgent Agent;
    [SerializeField] int Fov;

    float chargeTime;
    float angleToPlayer;
    Vector3 playerDir;
    float shootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnManager.instance.aliveEnemies++;
        SpeedOrig = Agent.speed;
        chargeSpeed = Agent.speed * 3;
        turnSpeedOrig = Agent.angularSpeed;
        turnSpeedCharge = Agent.angularSpeed / 3;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        Agent.SetDestination(GameManager.instance.player.transform.position);
        if (canSeePlayer())
        {
            chargeTime += Time.deltaTime;
            if (chargeTime >= 3)
            {
                Agent.speed = chargeSpeed;
                Agent.angularSpeed = turnSpeedCharge;
                if (Rank >= 2)
                {
                    chargeBullets();
                }
            }
        }
        else
        {
            chargeTime -= Time.deltaTime;
            Agent.speed = SpeedOrig;
            Agent.angularSpeed = turnSpeedOrig;
        }
    }

    bool canSeePlayer()
    {
        playerDir = GameManager.instance.player.transform.position - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        Debug.DrawRay(transform.position, playerDir);
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, playerDir, out Hit))
        {
            if (angleToPlayer <= Fov && Hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            if (Rank == 3) deathShot();
            GameManager.instance.XP += 1;
            GameManager.instance.score += Rank;
            SpawnManager.instance.aliveEnemies--;
            dropPowerup();
            Destroy(gameObject);
        }
    }

    void deathShot()
    {
        Vector3 directionToOff1 = AimOffset1.position - transform.position;
        Vector3 directionToOff2 = AimOffset2.position - transform.position;
        Vector3 directionToOff3 = AimOffset3.position - transform.position;
        Vector3 directionToOff4 = AimOffset4.position - transform.position;
        Vector3 directionToOff5 = AimOffset5.position - transform.position;
        Vector3 directionToOff6 = AimOffset6.position - transform.position;
        Vector3 directionToOff7 = AimOffset7.position - transform.position;
        Vector3 directionToOff8 = AimOffset8.position - transform.position;

        Quaternion offset1 = Quaternion.LookRotation(directionToOff1);
        Quaternion offset2 = Quaternion.LookRotation(directionToOff2);
        Quaternion offset3 = Quaternion.LookRotation(directionToOff3);
        Quaternion offset4 = Quaternion.LookRotation(directionToOff4);
        Quaternion offset5 = Quaternion.LookRotation(directionToOff5);
        Quaternion offset6 = Quaternion.LookRotation(directionToOff6);
        Quaternion offset7 = Quaternion.LookRotation(directionToOff7);
        Quaternion offset8 = Quaternion.LookRotation(directionToOff8);

        Instantiate(DeathBullet, transform.position, offset1);
        Instantiate(DeathBullet, transform.position, offset2);
        Instantiate(DeathBullet, transform.position, offset3);
        Instantiate(DeathBullet, transform.position, offset4);
        Instantiate(DeathBullet, transform.position, offset5);
        Instantiate(DeathBullet, transform.position, offset6);
        Instantiate(DeathBullet, transform.position, offset7);
        Instantiate(DeathBullet, transform.position, offset8);

    }

    void chargeBullets()
    {
        if (shootTimer >= (4 - Rank))
        {
            shootTimer = 0;
            Instantiate(Bullet, transform.position, transform.rotation);
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
