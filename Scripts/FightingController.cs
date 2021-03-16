using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyFighting;

public class FightingController : MonoBehaviour
{
    public Controller controller;
    public EnemyFighter enemy;
    public FightPhase phase;
    public float attackTime;
    public CameraController camera;
    public GameCommentator commentator;

    public void MakeEnemyMotion(EnemyAbilty ability)
    {
        if (!new HashSet<PlayerAvoidance>(ability.avoidances).Contains(controller.position)) 
        {
            Debug.Log("Уклонение не удалось");
            commentator.SayBad("Промах");
            camera.ZoomTo(5);
        }
        else
        {
            Debug.Log("Удачно уклонился");
            camera.ZoomTo(5);
            commentator.SayGood("Уклон!");
        }
    }

    public void StopEnemyMotion()
    {
        controller.reqPos = controller.originPos;
        controller.position = PlayerAvoidance.none;
    }

    public void StartDefending()
    {
        phase = FightPhase.Attack;
        controller.position = PlayerAvoidance.none;
        controller.reqPos = controller.originPos;
        StartCoroutine(Attacking());
        camera.ZoomTo(2);
        commentator.SayGood("АТАКУЙ!");
    }

    public void Attack()
    {
        Debug.Log("Игрок Атакует!");
        commentator.SayStat("АТАКА! \n -1");
    }

    public IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackTime);
        phase = FightPhase.Defend;
        controller.reqPos = controller.originPos;
        enemy.MakeMotion();
        camera.ZoomTo(5);
        commentator.SayBad("Защищайся!");
    }
}

public enum FightPhase
{
    Defend, Attack
}