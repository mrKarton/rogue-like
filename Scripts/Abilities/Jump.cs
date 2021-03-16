using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyFighting;

[CreateAssetMenu(menuName ="Abils/Jump")]
public class Jump : EnemyAbilty
{
    private Vector3 reqPos;
    private float speed;
    public override void startUse()
    {
        reqPos = obj.position;
        reqPos += obj.up * 2;
        speed = 2 / startingTime;
    }

    public override void use()
    {
        reqPos += obj.up * -3;
        speed = 3 / useTime;
    }

    public override void toStartCondition()
    {
        reqPos += obj.up;
        speed = 10;
    }

    public override void ProcessAbility()
    {
        obj.position = Vector3.Lerp(obj.position, reqPos, speed * Time.deltaTime);
    }
}
