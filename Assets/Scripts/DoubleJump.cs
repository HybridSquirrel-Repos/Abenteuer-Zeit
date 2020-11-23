using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerMovement _moveScript;
    private Rigidbody2D _rigid;
    private bool once;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!GetComponent<PlayerMovement>().isGrounded && !once)
            {
                _rigid.AddForce(transform.up * 2);
                once = true;
            }
            
        }

        if (GetComponent<PlayerMovement>().isGrounded)
        {
            once = false;
        }
    }
}
