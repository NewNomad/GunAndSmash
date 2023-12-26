using UnityEngine;
namespace Game.Combat
{
    public class Attack : MonoBehaviour
    {
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }



    }
}
