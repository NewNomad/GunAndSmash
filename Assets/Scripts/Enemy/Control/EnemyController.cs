using UnityEngine;
namespace Enemy.Control
{
    public class EnemyController : MonoBehaviour
    {

        private StateMachine stateMachine = new StateMachine();
        private void Awake()
        {
            InitializeGetComponents();
            InitializeStates();
        }

        private void InitializeGetComponents()
        {
        }

        private void InitializeStates()
        {
            stateMachine.RegisterState(new MoveState(stateMachine));
            stateMachine.RegisterState(new FightState(stateMachine));
        }


    }
}
