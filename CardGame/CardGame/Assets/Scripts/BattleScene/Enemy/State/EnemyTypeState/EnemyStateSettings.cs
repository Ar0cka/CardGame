using UnityEngine;


public class EnemyStateSettings
{

    public void SelectEnemyAction(ref bool isAttack, ref bool isBuff, ref bool isDefense)
    {
        int random = Random.Range(1, 3);

        switch (random)
        {
            case 1:
                isAttack = true;
                isBuff = false;
                isDefense = false;
                break;
            case 2:
                isAttack = false;
                isBuff = false;
                isDefense = true;
                break;
            case 3:
                isAttack = false;
                isBuff = true;
                isDefense = false;
                break;
        }
    }
    
}
