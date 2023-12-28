using Game.UI;
using UnityEngine;
namespace Game.Core
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] int maxStamina = 1000;
        private int currentStamina;
        [SerializeField] float staminaRecoveryTime = 1f;
        float staminaRecoveryTimer = Mathf.Infinity;
        [SerializeField] int staminaRecoveryAmount = 1;
        [SerializeField] int useStaminaOnFire = 1;
        [SerializeField] int useStaminaOnMove = 1;
        [SerializeField] float staminaTimeOnUse = 1;
        [SerializeField] StaminaUI staminaUI;


        private void Awake()
        {
            currentStamina = maxStamina;
        }

        private void Update()
        {
            staminaRecoveryTimer += Time.deltaTime;
            if (staminaRecoveryTimer > staminaRecoveryTime)
            {
                currentStamina += staminaRecoveryAmount;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                staminaRecoveryTimer = 0f;
                staminaUI.SetStaminaPercentage(GetStaminaPercentage());
            }
        }

        public void UseStamina(int amount)
        {
            currentStamina -= amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            staminaRecoveryTimer = -staminaTimeOnUse;
            staminaUI.SetStaminaPercentage(GetStaminaPercentage());
        }

        public void UseStaminaOnFire()
        {
            UseStamina(useStaminaOnFire);
        }

        public void UseStaminaOnMove()
        {
            UseStamina(useStaminaOnMove);
        }

        public float GetStaminaPercentage()
        {
            return (float)currentStamina / (float)maxStamina;
        }

        public bool IsStaminaFull() => currentStamina == maxStamina;

        public bool IsStaminaEmpty() => currentStamina == 0;

        public void SetStamina(int amount)
        {
            currentStamina = amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            staminaRecoveryTimer = 0f;
        }
    }
}
