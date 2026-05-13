using UnityEngine;

namespace CoreBreach.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (inputReader == null) return;
            rb.linearVelocity = inputReader.MoveAxis * moveSpeed;
        }
    }
}