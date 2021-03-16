using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyFighting;

[CreateAssetMenu(menuName = "Abils/FPunch")]
public class ForcedPunch : EnemyAbilty
{
    private Vector3 reqPos;
    private float speed;
    public override void startUse()
    {
        reqPos = obj.position;
        reqPos += obj.right * 2;
        speed = 2 / startingTime;
    }

    public override void use()
    {
        reqPos += obj.right * -3;
        speed = 3 / useTime;
    }

    public override void toStartCondition()
    {
        reqPos += obj.right;
        speed = 10;
    }

    public override void ProcessAbility()
    {
        obj.position = Vector3.Lerp(obj.position, reqPos, speed * Time.deltaTime);
    }
}
