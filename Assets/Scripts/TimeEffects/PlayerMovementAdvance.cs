using QInventory;
using UnityEngine;

namespace RichiGames
{
    public class PlayerMovementAdvance : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        [Header("Movement")]
        private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;

        public float groundDrag;

        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Rolling")]
        public float RollForce;
        public float RollCooldown;
        public float RollDistance;
        public float InvinsibleTime;
        public bool readyToRoll;

        [Header("Keybinds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode rollKey = KeyCode.LeftControl;
        public KeyCode attackKey = KeyCode.Mouse0;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance;
        public float playerHeight;
        public LayerMask whatIsGround;
        public bool grounded;

        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;

        [Header("Animation")]
        public Animator animator;
        public float animBlendSpeed;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        public Rigidbody rb;

        public Vector3 MovementDirection => moveDirection;

        public MovementState state;
        public enum MovementState
        {
            walking,
            sprinting,
            attacking,
            air
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            readyToJump = true;
            readyToRoll = true;
        }

        private void Update()
        {
            if (TimeController.Instance.IsRewinding == true) return;

            if (state == MovementState.attacking) return;

            // ground check
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

            // grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();
            UpdateAnimationParameters();

            // handle drag
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        private void FixedUpdate()
        {
            if (state == MovementState.attacking) return;
            MovePlayer();
        }

        private void MyInput()
        {
            if (readyToRoll != true) return;
            
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // when to jump
            if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }

            // when to roll
            if (Input.GetKeyDown(rollKey) && readyToRoll)
            {
                readyToRoll = false;

                Roll();

                Invoke(nameof(ResetRoll), RollCooldown);
            }
        }

        private void StateHandler()
        {
            // Mode - Sprinting
            if (grounded && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }

            // Mode - Walking
            else if (grounded)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }

            // Mode - Air
            else
            {
                state = MovementState.air;
            }
        }

        private void MovePlayer()
        {
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // on slope
            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            // on ground
            else if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // turn gravity off while on slope
            rb.useGravity = !OnSlope();
        }

        private void SpeedControl()
        {
            // limiting speed on slope
            if (OnSlope() && !exitingSlope)
            {
                if (rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }

            // limiting speed on ground or in air
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                // limit velocity if needed
                if (flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
        }

        private void Jump()
        {
            exitingSlope = true;

            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            animator.SetTrigger("Jump");
        }
        private void ResetJump()
        {
            readyToJump = true;

            exitingSlope = false;
        }

        private void Roll()
        {
            //rb.AddForce(moveDirection * RollForce, ForceMode.Impulse);
            //moveSpeed = RollForce;
            rb.isKinematic = true;
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            gameObject.transform.position = gameObject.transform.position + moveDirection * RollDistance;
            //rb.velocity = ((moveDirection * RollDistance) - transform.position) * (1 / Time.fixedDeltaTime);

            rb.isKinematic = false;
            animator.SetTrigger("Roll");

        }
        private void ResetRoll()
        {
            readyToRoll = true;
        }
        
        private void UpdateAnimationParameters()
        {
            animator.SetBool("IsGrounded", grounded);

            float targetSpeed = moveSpeed / sprintSpeed * 2;

            float currentMoveX = animator.GetFloat("MoveX");
            float currentMoveY = animator.GetFloat("MoveY");

            float smoothedMoveX = Mathf.MoveTowards(currentMoveX, horizontalInput * targetSpeed, Time.deltaTime * animBlendSpeed);
            float smoothedMoveY = Mathf.MoveTowards(currentMoveY, verticalInput * targetSpeed, Time.deltaTime * animBlendSpeed);

            animator.SetFloat("MoveX", smoothedMoveX);
            animator.SetFloat("MoveY", smoothedMoveY);
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        }
    }
}
