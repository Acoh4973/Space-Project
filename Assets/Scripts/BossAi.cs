using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour , iDamage
{
    [SerializeField] int HP;
    [SerializeField] int Rank;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject HomingBullet;
    [SerializeField] Transform AimOffset1;
    [SerializeField] Transform AimOffset2;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] int FOV;

    float SpeedOrig;
    float chargeSpeed;
    float turnSpeedOrig;
    float turnSpeedCharge;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip damageSFX;

    float stateTimer;
    bool ChargeState;
    float shootTimer;
    float moveDelay = 6;
    float angleToPlayer;
    Vector3 playerDir;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpeedOrig = Agent.speed;
        chargeSpeed = Agent.speed * 2;
        turnSpeedOrig = Agent.angularSpeed;
        turnSpeedCharge = Agent.angularSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnManager.instance.aliveEnemies = true;
        shoot();
        stateToggle();
        movement();
        inboundWarp();
    }

    void stateToggle()
    {
        if (!ChargeState) stateTimer += Time.deltaTime;
        if (ChargeState) stateTimer -= Time.deltaTime;

        if (stateTimer >= 10) ChargeState = true;
        if (stateTimer <= 0) ChargeState = false;
    }

    void movement()
    {
        if (!ChargeState)
        {
            Agent.speed = SpeedOrig;
            Agent.angularSpeed = turnSpeedOrig;
            moveDelay += Time.deltaTime;
            if (moveDelay >= 5)
            {
                moveDelay = 0;
                Agent.SetDestination(GameManager.instance.player.transform.position);
            }
        }
        else if (ChargeState)
        {
            Agent.speed = chargeSpeed;
            Agent.angularSpeed = turnSpeedCharge;
            Agent.SetDestination(GameManager.instance.player.transform.position);
        }
    }

    void shoot()
    {
        shootTimer += Time.deltaTime;
        if (!ChargeState)
        {
            lookAtPlayer();
            if (shootTimer >= 3)
            {
                shootTimer = 0;
                Vector3 directionToOff1 = AimOffset1.position - transform.position;
                Vector3 directionToOff2 = AimOffset2.position - transform.position;
                Quaternion offset1 = Quaternion.LookRotation(directionToOff1);
                Quaternion offset2 = Quaternion.LookRotation(directionToOff2);
                Instantiate(Bullet, transform.position, offset1);
                Instantiate(HomingBullet, transform.position, transform.rotation);
                Instantiate(Bullet, transform.position, offset2);
            }
        }
        if (ChargeState)
        {
            if (Rank == 2)
            {
                if (shootTimer == 2)
                {
                    Instantiate(HomingBullet, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(shootSFX);
                }
            }
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
        AudioManager.instance.PlaySFX(damageSFX);
        if (HP <= 0)
        {
            GameManager.instance.XP += 10;
            GameManager.instance.score += (Rank * 10);
            Destroy(gameObject);
        }
    }

    void inboundWarp()
    {
        if (transform.position.x > 95) transform.position = new Vector3(-90, 2, transform.position.z);
        if (transform.position.x < -95) transform.position = new Vector3(90, 2, transform.position.z);
        if (transform.position.z > 95) transform.position = new Vector3(transform.position.x, 2, -90);
        if (transform.position.z < -95) transform.position = new Vector3(transform.position.x, 2, 90);
    }

}
