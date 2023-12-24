using Game.Control;
using UnityEngine;
namespace Enemy.Control
{
    public class EnemyController : MonoBehaviour
    {

        private StateMachine stateMachine = new StateMachine();
        PlayerController playerController;
        private void Awake()
        {
            InitializeGetComponents();
            InitializeStates();
        }

        private void InitializeGetComponents()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        private void InitializeStates()
        {
            stateMachine.RegisterState(new MoveState(stateMachine));
            stateMachine.RegisterState(new FightState(stateMachine));
        }


    }
}
