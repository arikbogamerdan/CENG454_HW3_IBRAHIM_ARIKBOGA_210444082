using UnityEngine;

namespace CoreBreach.Player
{
   
    public class PlayerInputReader : MonoBehaviour
    {
        public Vector2 MoveAxis { get; private set; }
        public bool FireHeld { get; private set; }
        public bool FirePressed { get; private set; }
        private void Update()
        {
           
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector2 raw = new Vector2(h, v);
            MoveAxis = raw.sqrMagnitude > 1f ? raw.normalized : raw;
            FireHeld    = Input.GetMouseButton(0)     || Input.GetKey(KeyCode.Space);
            FirePressed = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
        }
    }
}