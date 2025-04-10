using UnityEngine;

namespace AE
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance;

        public SwordPickup heldSword;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public bool IsHoldingSword => heldSword != null;

        public void PickUpSword(SwordPickup sword)
        {
            heldSword = sword;
            Debug.Log("Sword picked up");
        }

        public void ClearSword()
        {
            heldSword = null;
        }
    }
}
