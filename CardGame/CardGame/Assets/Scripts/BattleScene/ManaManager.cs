using System.Collections.Generic;
using UnityEngine;


public class ManaManager : MonoBehaviour // Менеджер по добавлению маны в пул
{
   private List<ManaCardsPrefab> buildResurses = new List<ManaCardsPrefab>();
   private List<ManaCardsPrefab> humanResurses = new List<ManaCardsPrefab>();

   public void RegisterCards(ManaCardsPrefab prefabCard, ResursBuild resursBuildInfo)
   {
      if (resursBuildInfo.typeBuild == ResursBuild.TypeBuild.BuildMana)
      {
         buildResurses.Add(prefabCard);
      }
      else if (resursBuildInfo.typeBuild == ResursBuild.TypeBuild.HumanMana)
      {
         humanResurses.Add(prefabCard);
      }
   }

   public void AddManaToPool(int humanManaPool, int buildManaPool)
   {
      humanManaPool += buildResurses.Count;
      buildManaPool += humanResurses.Count;
   }
}
