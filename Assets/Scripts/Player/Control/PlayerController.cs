using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {
        private StateMachine stateMachine = new StateMachine();
        static public PlayerController instance;


        private void Awake()
        {
            Instance();
            InitializeInput();
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
            stateMachine.RegisterState(new MoveState());
            stateMachine.RegisterState(new FireState());
            stateMachine.RegisterState(new IdleState());
        }

        private void InitializeInput()
        {
            GetComponent<PlayerInputController>().onFireEvent.AddListener(OnFire);
            GetComponent<PlayerInputController>().onMoveEvent.AddListener(OnMove);
            GetComponent<PlayerInputController>().onStop.AddListener(OnStop);
            print("InitializeInput");
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
            stateMachine.ChangeState(StateID.Move);
        }

        private void OnStop()
        {
            stateMachine.ChangeState(StateID.Idle);
        }

    }
}
