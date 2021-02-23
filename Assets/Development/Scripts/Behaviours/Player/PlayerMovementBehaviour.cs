using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
   [SerializeField] private float movementSpeed = 5;
   [SerializeField] private float jumpVelocity = 5;
   [SerializeField] private LayerMask mask;
   [SerializeField] private Rigidbody2D rb2d;
   [SerializeField] private GameObject player;
   [SerializeField] private float gravity = 1f;
   [SerializeField] private float fallMultiplier = 5F;
   [SerializeField] private float jumpDelay = 0.25f;
   [SerializeField] private Vector2 rayOffset;
   
   
   private float _jumpTimer;
   private float _horizontal;
   private int _isCollidingSide;
   private bool _isGrounded;
   private bool _isJumping;
   private Vector2 _playerSize;
   private Vector2 _movement;
   private Camera mainCamera;
   public void SetupBehaviour()
   {
      SetGameplayCamera();
   }

   void SetGameplayCamera()
   {
      mainCamera = CameraManager.Instance.GetGameplayCamera();
   }
   
   
   void Awake()
   {
      _playerSize = GetComponent<BoxCollider2D>().size;
      rb2d = GetComponent<Rigidbody2D>();
      mask = LayerMask.GetMask("Default");
      
   }

   public void UpdateMovementData(Vector2 value)
   {
      _horizontal = value.x;
   }

   public void UpdateJump()
   {

      _isJumping = true;
      if (_isJumping)
      {
         _jumpTimer = Time.time + jumpDelay;
      }

   }
   
   
   public void TwoWayUpdate()
   {
      StartCoroutine(Delay());
   }

   IEnumerator Delay()
   {
      for (int i = 0; i < 10; i++)
      {
         yield return _isGrounded = false;
      }
   }
   void Update()
   {

      
      
   }

   void FixedUpdate()
   {
      if (_isCollidingSide.Equals(1) && _horizontal > 0)
      {
         rb2d.velocity = new Vector2(_horizontal * movementSpeed, rb2d.velocity.y);

         
      }
      else if (_isCollidingSide.Equals(-1) && _horizontal < 0)
      {
         rb2d.velocity = new Vector2(_horizontal * movementSpeed, rb2d.velocity.y);

      }
      else if(_isCollidingSide.Equals(0))
      {
         rb2d.velocity = new Vector2(_horizontal * movementSpeed, rb2d.velocity.y);
         
      }
      
      
      if (_jumpTimer > Time.time && _isGrounded)
      {
         Jump();
      }
      
      if (_isGrounded)
      {
         rb2d.gravityScale = 0;
         _isJumping = false;
      }
      else
      {
         rb2d.gravityScale = gravity;
         if (rb2d.velocity.y < 0)
         {
            rb2d.gravityScale = gravity * fallMultiplier;
         }
         else if (rb2d.velocity.y > 0 && _isJumping) // need a value
         {
            rb2d.gravityScale = gravity * (fallMultiplier / 2);
         }
      }

      
      
      Vector2 ray = (Vector2) transform.position + Vector2.down * (_playerSize.y * 0.5f);
      _isGrounded = Physics2D.Raycast(ray + rayOffset, Vector2.down, 0.02f, mask) || 
                    Physics2D.Raycast(ray - rayOffset, Vector2.down, 0.02f, mask); 
      
   }

   void OnCollisionEnter2D(Collision2D collision)
   {

      if (collision.gameObject.CompareTag("MovingPlatform"))
      {
         gameObject.transform.parent = collision.gameObject.transform;
      }
      
      foreach (ContactPoint2D hitPos in collision.contacts)
      {
        
         //Fix collision with side objects
         if (hitPos.normal.x.Equals(1)) 
         {
            _isCollidingSide = 1;
         }
         else if(hitPos.normal.x.Equals(-1))
         {
            _isCollidingSide = -1;
         }
         
      }
   }
   
   void OnCollisionExit2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Platform"))
      {
         _isCollidingSide = 0;
      }

      if (collision.gameObject.CompareTag("TwoWayPlatform"))
      {
         _isCollidingSide = 0;
      }

      if (collision.gameObject.CompareTag("MovingPlatform"))
      {
         gameObject.transform.parent = null;
         _isCollidingSide = 0;
      }
      
   }

   
   void Jump()
   {
 
      rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
      rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
      _jumpTimer = 0;
      
      _isGrounded = false;
   }

}
