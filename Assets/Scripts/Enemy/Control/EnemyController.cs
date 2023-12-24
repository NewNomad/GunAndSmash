using Enemy.Movement;
using Game.Control;
using UnityEngine;
namespace Enemy.Control
{
    public class EnemyController : MonoBehaviour
    {

        private StateMachine stateMachine = new StateMachine();
        PlayerController playerController;
        Mover mover;
        private void Awake()
        {
            InitializeGetComponents();
            InitializeStates();

            stateMachine.ChangeState(StateID.Move);
        }

        private void InitializeGetComponents()
        {
            playerController = FindObjectOfType<PlayerController>();
            mover = GetComponent<Mover>();
        }

        private void InitializeStates()
        {
            stateMachine.RegisterState(new MoveState(stateMachine, mover));
            stateMachine.RegisterState(new FightState(stateMachine));
        }
        private void Update()
        {
            stateMachine.Update();
        }
    }
}
