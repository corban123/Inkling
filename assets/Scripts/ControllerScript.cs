using UnityEngine;
using System.Collections;
namespace GameProject
{
    [RequireComponent(typeof(PlayerScript))]
    public class ControllerScript : MonoBehaviour
    {
        private bool jumped;
        PlayerScript player;
        // Use this for initialization
        void Start()
        {
            
            player = GetComponent<PlayerScript>();
        }
        void Update()
        {
            if (!jumped)
            {
                jumped = Input.GetButtonDown("Jump");
            }
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            float horiz = Input.GetAxis("Horizontal");
            bool flip = Input.GetKeyDown("left shift");
            player.Move(horiz, flip, jumped);
            jumped = false;
        }
    }
}