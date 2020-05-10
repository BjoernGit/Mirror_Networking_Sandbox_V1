using UnityEngine;

namespace Mirror.Examples.NetworkRoom
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NetworkTransform))]
    public class PlayerController : NetworkBehaviour
    {
        public CharacterController characterController;
        [SerializeField] private GameObject camPos;
        private Camera cam;

        void OnValidate()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            cam = Camera.main;
            cam.orthographic = false;
            cam.transform.SetParent(transform);
            //cam.transform.localPosition = new Vector3(0f, 3f, -8f);
            //cam.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
        }

        void OnDisable()
        {
            if (isLocalPlayer && Camera.main != null)
            {
                cam = Camera.main;
                cam.orthographic = true;
                cam.transform.SetParent(null);
                cam.transform.localPosition = new Vector3(0f, 70f, 0f);
                //cam.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                //cam.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            }
        }

        [Header("Movement Settings")]
        public float moveSpeed = 8f;
        public float turnSensitivity = 5f;
        public float maxTurnSpeed = 150f;
        //new from björn(me)
        public float rotateSpeed = 10f;

        [Header("Diagnostics")]
        public float horizontal;
        public float vertical;
        public float turn;
        public float jumpSpeed;
        public bool isGrounded = true;
        public bool isFalling;
        public Vector3 velocity;

        void Update()
        {
            if (!isLocalPlayer)
                return;

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (isGrounded)
                isFalling = false;

            if ((isGrounded || !isFalling) && jumpSpeed < 1f && Input.GetKey(KeyCode.Space))
                jumpSpeed = Mathf.Lerp(jumpSpeed, 1f, 0.5f);

            else if (!isGrounded)
            {
                isFalling = true;
                jumpSpeed = 0;
            }


        }

        void FixedUpdate()
        {
            if (!isLocalPlayer || characterController == null)
                return;

            transform.Rotate(0f, turn * Time.fixedDeltaTime, 0f);

            Vector3 direction = new Vector3(horizontal, jumpSpeed, vertical);
            direction = Vector3.ClampMagnitude(direction, 1f);
            //direction = transform.TransformDirection(direction);
            direction *= moveSpeed;

            if (jumpSpeed > 0)
                characterController.Move(direction * Time.fixedDeltaTime);
            else
                characterController.SimpleMove(direction);

            isGrounded = characterController.isGrounded;
            velocity = characterController.velocity;

            RaycastHit hit;
            Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit);

            Vector3 targetDirection = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
            float singleStep = rotateSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);

            cam = Camera.main;
            cam.transform.position = camPos.transform.position;
            cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
