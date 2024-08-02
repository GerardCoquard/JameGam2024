using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOE : Tower
{
    IEnumerator SetDmgToAbilitie(GameObject bullet)
    {
        yield return new WaitForSeconds(1.2f);
        //bullet.GetComponent<ActivateAreaDamage>().SetVariables(damage, this);
    }
    IEnumerator FollowEnemy(Transform bullet,Transform enemy)
    {
        float time = 0;
        MoveEnemy enemyMove = enemy.GetComponent<MoveEnemy>();
        while (time < 3)
        {
            if(enemy != null)
            {
                time += Time.deltaTime;
                bullet.position = enemyMove.publicPos;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
