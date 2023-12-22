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
            // stateMachine.RegisterState(new IdleState(this));
        }


    }

}
