using UnityEngine;
using System.Collections;
namespace GameProject
{
    public class PlayerScript : MonoBehaviour
    {
        private float maxSpeed = 10;
        private Transform GroundCheck;
        const float GroundedRadius = .1f;
        private bool isGrounded;
        private Animator Anim;
        private Rigidbody2D rigidBody;
        private bool FacingRight = true;
        private LayerMask whatIsGround;
        private float jumpSpeed = 10f;
        public Camera curCamera;
        void Awake()
        {
            Physics.IgnoreLayerCollision(0, 8, true);

            
            whatIsGround = this.gameObject.layer;
            rigidBody = GetComponent<Rigidbody2D>();
            GroundCheck = transform.Find("GroundCheck");
            Anim = GetComponent<Animator>();
			curCamera.cullingMask = 1;
            
        }
        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate()
        {

            isGrounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, whatIsGround + 1);

            for (int i = 0; i< colliders.Length; i++)
            {
                if(colliders[i].gameObject != this.gameObject)
                {
                    isGrounded = true;
                }
                Anim.SetBool("Grounded", isGrounded);
            }
        }
        public void Move(float horiz, bool flip, bool jump)
        {

            Debug.Log(LayerMask.NameToLayer("Default"));

            if (flip && this.gameObject.layer ==0)
            {

                this.gameObject.layer = 8;
                whatIsGround = 8;
				GroundCheck.gameObject.layer = 8;
                curCamera.cullingMask = (1 << LayerMask.NameToLayer("OtherRealm"));


            }
            else if(flip && this.gameObject.layer == 8)
            {

                this.gameObject.layer = 0;
                whatIsGround = 0;
				GroundCheck.gameObject.layer = 0;
                curCamera.cullingMask = 1;
            }
            if (isGrounded)
            {
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
            }

            if (jump)
            {
                Debug.Log(isGrounded);
                Debug.Log("JUMPED " + jumpSpeed);
                isGrounded = false;
                Anim.SetBool("Grounded", false);
                rigidBody.velocity = (new Vector2(0f, jumpSpeed));
            }
        }
        private void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}   
