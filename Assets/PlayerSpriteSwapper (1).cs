using UnityEngine;

public class PlayerSpriteSwapper : MonoBehaviour
{
    public bool isSideScroller;
    public Sprite goingLeft;
    public Sprite goingUp;
    public Sprite goingDown;
    private Player controls;
    SpriteRenderer playerSprite;
    
    void Awake(){
        controls = new Player();
        playerSprite = GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable(){
        controls.Enable();
    }
    
    private void OnDisable(){
        controls.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = controls.PlayerControls.Move.ReadValue<Vector2>();
        if (moveInput.x > 0){
            playerSprite.sprite = goingLeft;
            playerSprite.flipX = true;
        }
        else if (moveInput.x < 0){
            playerSprite.sprite = goingLeft;
            playerSprite.flipX = false;
        }
        if (!isSideScroller){
        if (moveInput.y > 0 && moveInput.x == 0){
            playerSprite.sprite = goingUp;
        }
        else if (moveInput.y < 0 && moveInput.x == 0){
            playerSprite.sprite = goingDown;
        }
        }
    }
}
