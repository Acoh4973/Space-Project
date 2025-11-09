using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int WeaponType;
    float destroyTimer;

    private void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > 10) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        iPickup pickup = other.GetComponent<iPickup>();
        if (pickup != null)
        {
            pickup.getPowerup(WeaponType);

            Destroy(gameObject);
        }
    }
}
