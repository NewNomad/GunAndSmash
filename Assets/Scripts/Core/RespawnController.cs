namespace Game.Core
{
    using UnityEngine;

    public class RespawnController : MonoBehaviour
    {
        GameObject[] respawnPoints;

        private void Awake()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                respawnPoints[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
