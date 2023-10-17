using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController 
{
   public int AttackPlayer(int hpPlayer);
   public int AttackEnemy(int damagePlayer);

   public int Defense(int defense);
}
