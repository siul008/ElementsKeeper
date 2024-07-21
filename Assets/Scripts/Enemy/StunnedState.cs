using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StunnedState : EnemyState
{
    Vector3 basePos;
    float stunDuration;
    float bumpDist;
    float bumpSpeed;
    bool reachedTop;
    bool returnedToPos;
    float stunTime;

    public StunnedState(float _stunDuration, float _bumpDist, float _bumpSpeed)
    {
        stunDuration = _stunDuration;
        bumpDist = _bumpDist;
        bumpSpeed = _bumpSpeed;
        reachedTop = false;
        returnedToPos = false;
        stunTime = 0;
    }
    public override void Enter(Enemy enemy)
    {
        basePos = enemy.transform.position;
    }

    public override void Execute(Enemy enemy)
    {
        if (!reachedTop)
        {
            // Move up until reaching bumpDist
            if (enemy.transform.position.y < basePos.y + bumpDist)
            {
                enemy.transform.Translate(bumpSpeed * Time.deltaTime * Vector2.up);
            }
            else
            {
                reachedTop = true;
            }
        }
        else if (!returnedToPos)
        {
            // Move down back to the original position
            if (enemy.transform.position.y > basePos.y)
            {
                enemy.transform.Translate(bumpSpeed * Time.deltaTime * Vector2.down);
                if (enemy.transform.position.y < basePos.y)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x, basePos.y, enemy.transform.position.z);
                }
            }
            else
            {
                returnedToPos = true;
            }
        }
        else
        {
            //After landing is stunned until the stun duration is completed
            if (stunTime >= stunDuration)
            {
                enemy.ChangeState(new WalkingState());
            }
            else
            {
                stunTime += Time.deltaTime;
            }
        }
    }


    public override void Exit(Enemy enemy)
    {
        
    }
}
