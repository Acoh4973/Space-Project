using System.Collections;
using UnityEngine;

public class playerdamage : MonoBehaviour
{
    enum damageType { moving, stationary, DOT, homing };
    [SerializeField] damageType type;
    [SerializeField] Rigidbody rb;
    [SerializeField] int BulletType;
    [SerializeField] int damageAmount;
    [SerializeField] int damageRate;
    [SerializeField] int speed;
    [SerializeField] float destroyTime;

    bool isDamaging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        adjustDamage();
        if (type == damageType.moving || type == damageType.homing)
        {
            Destroy(gameObject, destroyTime);
            if (type == damageType.moving)
            {
                rb.linearVelocity = transform.forward * speed;
            }
        }
    }
    void homing()
    {
        rb.linearVelocity = (GameManager.instance.player.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (type == damageType.homing)
        {
            homing();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        iDamage dmg = other.GetComponent<iDamage>();
        if (dmg != null && (type == damageType.moving || type == damageType.stationary || type == damageType.homing))
        {
            Debug.Log("hit object: " + other.name);
            dmg.takeDamage(damageAmount);

        }
        if (type == damageType.homing || type == damageType.moving)
        {

            Destroy(gameObject);

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
            return;

        iDamage dmg = other.gameObject.GetComponent<iDamage>();

        if (dmg != null && type == damageType.DOT)

        {

            if (!isDamaging)

            {

                StartCoroutine(damageOther(dmg));
            }

        }

    }

    void adjustDamage()
    {
        switch (BulletType)
        {
            case 1: //Basic or Rapid
                damageAmount = UpgradeManager.instance.rapidFireDmg;
                break;
            case 2: // Spread
                damageAmount = UpgradeManager.instance.spreadFireDmg;
                break;
            case 3: // Railgun
                damageAmount = UpgradeManager.instance.RailgunDmg;
                break;
            default:
                damageAmount = 5;
                break;
        }

    }
    IEnumerator damageOther(iDamage d)
    {
        isDamaging = true;
        d.takeDamage(damageAmount);
        yield return new WaitForSeconds(damageRate);
        isDamaging = false;


    }

}
