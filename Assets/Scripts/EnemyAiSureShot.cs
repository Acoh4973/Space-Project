using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] Renderer model;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip damageSFX;

    float angleToPlayer;
    Vector3 playerDir;
    Color colorOrig;
    float shootTimer;
    float moveDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        Agent.SetDestination(GameManager.instance.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnManager.instance.aliveEnemies = true;
        lookAtPlayer();
        movement();
        shoot();
        inboundWarp();
    }

    void shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= 3 - (0.5 * Rank))
        {
            shootTimer = 0;
            if (Rank < 3)
            {
                Instantiate(Bullet, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(shootSFX);
            }
            if (Rank == 3)
            {
                Vector3 directionToOff1 = AimOffset1.position - transform.position;
                Vector3 directionToOff2 = AimOffset2.position - transform.position;
                Quaternion offset1 = Quaternion.LookRotation(directionToOff1);
                Quaternion offset2 = Quaternion.LookRotation(directionToOff2);
                Instantiate(Bullet, transform.position, offset1);
                Instantiate(Bullet, transform.position, transform.rotation);
                Instantiate(Bullet, transform.position, offset2);
                AudioManager.instance.PlaySFX(shootSFX);
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
        AudioManager.instance.PlaySFX(damageSFX);
        if (HP <= 0)
        {
            GameManager.instance.XP += 1;
            GameManager.instance.score += Rank;
            dropPowerup();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashRed());
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

    void inboundWarp()
    {
        if (transform.position.x > 95) transform.position = new Vector3(-90, 2, transform.position.z);
        if (transform.position.x < -95) transform.position = new Vector3(90, 2, transform.position.z);
        if (transform.position.z > 95) transform.position = new Vector3(transform.position.x, 2, -90);
        if (transform.position.z < -95) transform.position = new Vector3(transform.position.x, 2, 90);
    }
    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }
}

