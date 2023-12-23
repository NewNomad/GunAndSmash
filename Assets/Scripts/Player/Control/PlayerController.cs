using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Core;
using Game.Move;
using UnityEngine;

namespace Game.Control
{
    [RequireComponent(typeof(PlayerInputController), typeof(Mover), typeof(Stop))]
    [RequireComponent(typeof(Fire), typeof(WallCheck))]
    public class PlayerController : MonoBehaviour
    {
        private StateMachine stateMachine = new StateMachine();
        static public PlayerController instance;
        Mover mover;
        Stop stop;
        Fire fire;
        WallCheck wallCheck;

        private void Awake()
        {
            Instance();
            InitializeInput();
            InitializeGetComponents();
            InitializeStates();


            // 初期State
            stateMachine.ChangeState(StateID.Idle);
        }

        private void Instance()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void InitializeStates()
        {
            stateMachine.RegisterState(new MoveState(stateMachine));
            stateMachine.RegisterState(new FireState(stateMachine, mover, fire));
            stateMachine.RegisterState(new IdleState(stateMachine));
            stateMachine.RegisterState(new StopState(stateMachine, stop));
        }

        private void InitializeInput()
        {
            GetComponent<PlayerInputController>().onFireEvent.AddListener(OnFire);
            GetComponent<PlayerInputController>().onMoveEvent.AddListener(OnMove);
            GetComponent<PlayerInputController>().onStop.AddListener(OnStop);
        }

        private void InitializeGetComponents()
        {
            mover = GetComponent<Mover>();
            stop = GetComponent<Stop>();
            fire = GetComponent<Fire>();
        }

        private void OnDisable()
        {
            GetComponent<PlayerInputController>().onFireEvent.RemoveListener(OnFire);
            GetComponent<PlayerInputController>().onMoveEvent.RemoveListener(OnMove);
            GetComponent<PlayerInputController>().onStop.RemoveListener(OnStop);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void OnFire()
        {
            print("OnFire");
            stateMachine.ChangeState(StateID.Fire);
        }

        private void OnMove(Vector2 moveDirection)
        {
            mover.Move(moveDirection);
            stateMachine.ChangeState(StateID.Move);
        }

        private void OnStop()
        {
            stateMachine.ChangeState(StateID.Stop);
        }
        // TODO: EnhancedActionの実装
    }
}
