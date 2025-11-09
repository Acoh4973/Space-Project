using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, iDamage , iPickup
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] CharacterController controller;

    [SerializeField] int shipSpeed;
    [SerializeField] int HP;
    [SerializeField] int regenRate;

    [SerializeField] int weaponType;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject RapidBullet;
    [SerializeField] GameObject SpreadBullet;
    [SerializeField] Transform AimOffset1;
    [SerializeField] Transform AimOffset2;
    [SerializeField] GameObject RailBullet;

    float regenTime;
    float shootTime;
    float powerupTime;
    Vector3 moveDir;
    Vector3 playerVel;
    Vector3 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            Movement();
            LookAtCursor();
            RegenHp();
            shoot();
            losePowerup();
        }
    }

    void Movement()
    {
        moveDir = Input.GetAxis("Horizontal") * Vector3.right + 
            Input.GetAxis("Vertical") * Vector3.forward;
        controller.Move(moveDir * shipSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
    }

    void LookAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 mouseWorldPosition = hit.point;
            Vector3 directionToMouse = mouseWorldPosition - transform.position;
            directionToMouse.y = 0;

            if (directionToMouse != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
                transform.rotation = targetRotation;
            }
        }
    }

    void shoot()
    {
        shootTime += Time.deltaTime;
        if(Input.GetButton("Fire1"))
        {
            switch (weaponType)
            {
                case 1: // Rapid Fire
                    if (shootTime >= 0.25)
                    {
                        shootTime = 0;
                        Instantiate(RapidBullet, transform.position, transform.rotation);
                    }
                    break;
                case 2: // Spread Shot
                    Vector3 directionToOff1 = AimOffset1.position - transform.position;
                    Vector3 directionToOff2 = AimOffset2.position - transform.position;
                    Quaternion offset1 = Quaternion.LookRotation(directionToOff1);
                    Quaternion offset2 = Quaternion.LookRotation(directionToOff2);
                    if (shootTime >= 1)
                    {
                        shootTime = 0;
                        Instantiate(SpreadBullet, transform.position, offset1);
                        Instantiate(SpreadBullet, transform.position, transform.rotation);
                        Instantiate(SpreadBullet, transform.position, offset2);
                    }

                    break;
                case 3: // Railgun
                    if (shootTime >= 2)
                    {
                        shootTime = 0;
                        Instantiate(RailBullet, transform.position, transform.rotation);
                    }

                    break;
                default: // Basic Shot
                    if (shootTime >= 1)
                    {
                        shootTime = 0;
                        Instantiate(Bullet, transform.position, transform.rotation);
                    }
                    break;
            }
        }
    }

    void RegenHp()
    {
        regenTime += Time.deltaTime;
        if (regenTime >= regenRate)
        {
            regenTime = 0;
            HP += UpgradeManager.instance.hpRegen;
            if(HP >= UpgradeManager.instance.maxHp) HP = UpgradeManager.instance.maxHp;
        }
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            GameManager.instance.endGame();
        }
    }

    public void getPowerup(int weapon)
    {
        powerupTime = 10 + UpgradeManager.instance.weaponUpgrades;
        weaponType = weapon;
    }

    void losePowerup()
    {
        powerupTime -= Time.deltaTime;
        if (powerupTime < 0) weaponType = 0;
    }
}
