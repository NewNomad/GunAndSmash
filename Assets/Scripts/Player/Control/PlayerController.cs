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
            stateMachine.RegisterState(new MoveState());
            stateMachine.RegisterState(new FireState());
            stateMachine.RegisterState(new IdleState());

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

        private void Update()
        {
            stateMachine.Update();
        }
    }
}
