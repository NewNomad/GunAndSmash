using Enemy.Combat;
using Enemy.Core;
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
        Fire fire;
        Health health;
        Stun stun;
        Charged charged;
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
            fire = GetComponent<Fire>();
            health = GetComponent<Health>();
            stun = GetComponent<Stun>();
            charged = GetComponent<Charged>();
            GetComponent<Health>().onStunned.AddListener(OnStunned);
        }

        private void InitializeStates()
        {
            stateMachine.RegisterState(new MoveState(stateMachine, mover));
            stateMachine.RegisterState(new FightState(stateMachine, fire));
            stateMachine.RegisterState(new StunState(stateMachine, health, stun, charged));
        }
        private void Update()
        {
            stateMachine.Update();
        }

        private void OnStunned()
        {
            stateMachine.ChangeState(StateID.Stun);
        }
    }
}
