using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask ignorelayer;
    [SerializeField] CharacterController controller;

    [SerializeField] int shipSpeed;
    int maxHp;
    [SerializeField] int HP;

    Vector3 moveDir;
    Vector3 playerVel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHp = HP;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveDir = Input.GetAxis("Horizontal") * Vector3.right + 
            Input.GetAxis("Vertical") * Vector3.forward;
        controller.Move(moveDir * shipSpeed * Time.deltaTime);
    }

}
