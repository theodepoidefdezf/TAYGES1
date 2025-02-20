
/*


public class Player_Script : NetworkBehaviour
{
    [SerializeField] public Camera c; // Caméra attachée au joueur

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner) // Si ce n'est pas le joueur local (l'instance propriétaire)
        {
            c.gameObject.SetActive(false);
            return;
        }

        c.gameObject.SetActive(true);
        c.tag = "MainCamera";
    }

    private void Update()
    {
        if (!IsOwner) 
        {
            return;
        }

        // Déplacement du joueur
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace Assets
{

    public class Player_Script : NetworkBehaviour
    {
        [SerializeField] public Camera c;
        [SerializeField] private Rigidbody rb;

        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float mouseSensitivity = 1f;
        public Transform cameraTarget;
        public Vector3 cameraOffset;
        public float maxLookAngle = 80f;
        private float verticalRotation = 0f;
        private Collider myCollider;

        private bool isInContact = false;

        private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

        private float lastJumpTime = -2f; // Initialise à -2 pour permettre un premier saut immédiatement
        private float jumpCooldown = 2f;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (!IsOwner)
            {
                c.enabled = false;
                return;
            }

            c.enabled = true;
            c.gameObject.SetActive(true);
            c.tag = "MainCamera";
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            myCollider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            HandleMovement();

            HandleCameraRotation();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.time - lastJumpTime >= jumpCooldown)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    lastJumpTime = Time.time; // Met à jour le temps du dernier saut
                    Debug.Log("Saut effectué !");
                }
                else
                {
                    Debug.Log("Saut refusé : cooldown actif !");
                }
            }

            UpdateUI();

            CheckGameConditions();
        }

        void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;
            if (!NewBehaviourScript.InPause)
            {
                transform.Translate(movement, Space.Self);
            }
        }

        void HandleCameraRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

         

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
            if (!NewBehaviourScript.InPause)
            {
                transform.Rotate(0, mouseX, 0);
                Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            }
        }

        void UpdateUI()
        {
            Debug.Log("Updating UI (placeholder)");
        }

        void CheckGameConditions()
        {
            if (transform.position.y < -10)
            {
                Debug.Log("Game Over: Player fell out of the world");
            }
        }

        bool Ground()
        {
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
        }
    }
}