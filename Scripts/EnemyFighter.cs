using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyFighting
{
    public class EnemyFighter : MonoBehaviour
    {
        public EnemyAbilty[] abilities;
        EnemyAbilty currentAbility;
        public float cooldown;

        public FightingController controller;

        private void Start()
        {
            controller = GameObject.FindObjectOfType<FightingController>();
            foreach(EnemyAbilty ability in abilities)
            {
                if(ability.title != "")
                {
                    ability.obj = transform;
                }
            }
            MakeMotion();
        }

        public List<EnemyAbilty> GetMotionList(int motionsNum)
        {
            List<EnemyAbilty> list = new List<EnemyAbilty>();
            for(int i = 0; i < motionsNum; i++)
            {
                if (list.Count >= motionsNum)
                {
                    break;
                }
                foreach (EnemyAbilty data in abilities)
                {
                    if(data.title == "")
                    {
                        break;
                    }
                    if(list.Count >= motionsNum)
                    {
                        break;
                    }
                    if(data.usable)
                    {
                        
                        if(Random.Range(0, 100) * i * 10 > 100 - data.chanceOfUse)
                        {
                            data.cooldownTimer = data.cooldown;
                            list.Add(data);
                        }
                    }
                }
            }

            return list;
        }

        public void MakeMotion()
        {
            List<EnemyAbilty> abilities = GetMotionList(3);

            StartCoroutine(MakeMotions(abilities));
        }

        public IEnumerator MakeMotions(List<EnemyAbilty> list)
        {
            for(int i =0; i < list.Count; i++)
            {
                

                currentAbility = list[i];

                list[i].startUse();
                yield return new WaitForSeconds(list[i].startingTime);

                list[i].use();
                controller.MakeEnemyMotion(currentAbility);
                yield return new WaitForSeconds(list[i].useTime);

                list[i].toStartCondition();

                list[i].cooldownTimer = cooldown;
                controller.StopEnemyMotion();
                yield return new WaitForSeconds(cooldown);
            }

            controller.StartDefending();
            Debug.Log("Motion Ended");
        }

        public void Update()
        {
            foreach(EnemyAbilty data in abilities)
            {
                if (data != null)
                {
                    if (!data.usable)
                    {
                        data.cooldownTimer -= Time.deltaTime;
                    }
                }
            }

            if(currentAbility != null)
            {
                if(currentAbility.title != "")
                {
                    currentAbility.ProcessAbility();
                }
            }
           
        }
    }
}