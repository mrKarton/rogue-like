using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyFighting
{
    public class EnemyAbilty : ScriptableObject
    {
        public string title;
        public float cooldown;

        [HideInInspector]
        public float cooldownTimer; //Отвечает за определние, сколько времени осталось до того, как возможность можно использовать
        public bool usable
        {
            get
            {
                return cooldownTimer <= 0;
            }
        }

        public float startingTime;
        public float useTime;
        public float damage;

        [Range(0, 100)]
        public float chanceOfUse;

        public PlayerAvoidance[] avoidances;

        [Header("Объект, который будет двигаться/изменяться")]
        public Transform obj;


        public virtual async void startUse()
        {
            Debug.Log("No Start event");
        }

        public virtual async void use()
        {
            Debug.Log("No Use Events");
        }

        public virtual async void toStartCondition()
        {
            Debug.Log("No event to go to start condition");
        }

        public virtual async void ProcessAbility()
        {
            Debug.Log("Nothing processing");
        }
    }

    public enum PlayerAvoidance
    {
        GoDown, Jump, Block, StepBack, none
    }
}