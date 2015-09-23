using UnityEngine;
using System.Collections;
namespace GameProject
{
    public class PlayerScript : MonoBehaviour
    {
        private float maxSpeed = 5;
        private Transform GroundCheck;
        const float GroundedRadius = .1f;
        public bool isGrounded;
        private Animator Anim;
        private Rigidbody2D rigidBody;
        private bool FacingRight = true;
        public LayerMask whatIsGround;
        private float jumpSpeed = 12f;
        public Camera curCamera;
        void Awake()
        {
            Physics.IgnoreLayerCollision(0, 8, true);


            whatIsGround = 1<<0 | 1 << 2;
            rigidBody = GetComponent<Rigidbody2D>();
            GroundCheck = transform.Find("GroundCheck");
            Anim = GetComponent<Animator>();
			curCamera.cullingMask = 1 | 1<<2;
        }
        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate()
        {
           

            isGrounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius,whatIsGround);
            for (int i = 0; i< colliders.Length; i++)
            {
                if(colliders[i].gameObject != this.gameObject)
                {
                    isGrounded = true;
                }
                Anim.SetBool("Grounded", isGrounded);
            }
            Anim.SetFloat("vSpeed", rigidBody.velocity.y);
        }
        public void Move(float horiz, bool flip, bool jump)
        {


            if (flip && this.gameObject.layer ==0)
            {

                this.gameObject.layer = 8;
                whatIsGround = 1<<8 | 1 << 2;
				GroundCheck.gameObject.layer = 8;
                curCamera.cullingMask = (1 << LayerMask.NameToLayer("OtherRealm") | 1<<2);


            }
            else if(flip && this.gameObject.layer == 8)
            {

                this.gameObject.layer = 0;
                whatIsGround = 1 | 1 << 2;
				GroundCheck.gameObject.layer = 0;
                curCamera.cullingMask = 1 | 1<<2;
            }
            
                Anim.SetFloat("Speed", Mathf.Abs(horiz));
                rigidBody.velocity = new Vector2(horiz * maxSpeed, rigidBody.velocity.y);
                if (horiz > 0 && !FacingRight)
                {
                    Flip();
                }
                else if (horiz < 0 && FacingRight)
                {
                    Flip();
                }
            

            if (jump && isGrounded)
            {
                isGrounded = false;
                Anim.SetBool("Jumping", true);
                Anim.SetBool("Grounded", false);
            }
        }
        private void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        private void Jump()
        {
            if (Anim.GetBool("Jumping"))
            {
                Anim.SetBool("Jumping", false);
                rigidBody.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            if(collider.gameObject.tag == "moveableObject")
            {

            }
        }
    }
}   
