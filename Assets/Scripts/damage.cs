using System.Collections;
using UnityEngine;

public class damage : MonoBehaviour
{
    enum damageType { moving, stationary, DOT, homing };
    [SerializeField] damageType type;
    [SerializeField] Rigidbody rb;
    [SerializeField] int damageAmount;
    [SerializeField] int damageRate;
    [SerializeField] int speed;
    [SerializeField] float destroyTime;
    float homingDelay;

    bool isDamaging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == damageType.moving || type == damageType.homing)
        {
            Destroy(gameObject, destroyTime);
            if (type == damageType.moving || type == damageType.homing)
            {
                rb.linearVelocity = transform.forward * speed;
            }
        }
    }
    void homing()
    {
        homingDelay += Time.deltaTime;
        if (homingDelay >= 1)
        {
            homingDelay = 0;
            Vector3 playerDir = GameManager.instance.player.transform.position - transform.position;
            rb.linearVelocity = playerDir;
        }
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

    IEnumerator damageOther(iDamage d)
    {
        isDamaging = true;
        d.takeDamage(damageAmount);
        yield return new WaitForSeconds(damageRate);
        isDamaging = false;


    }

}
