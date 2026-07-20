using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover2D : MonoBehaviour
{
    public bool isSideScroller = true;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Player controls;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Player();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        moveInput = controls.PlayerControls.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (isSideScroller)
        {
            rb.linearVelocity = new Vector2(
                moveInput.x * moveSpeed,
                rb.linearVelocity.y
            );
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}