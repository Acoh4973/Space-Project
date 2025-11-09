using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int WeaponType;

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
