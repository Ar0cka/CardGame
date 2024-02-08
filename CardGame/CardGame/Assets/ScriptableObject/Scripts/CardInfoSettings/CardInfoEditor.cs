using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CardInfo))]
public class CardInfoEditor : Editor
{
   public override void OnInspectorGUI()
   {
      CardInfo cardInfo = (CardInfo)target;

      DrawDefaultInspector();

      if (cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild)
      {
         cardInfo.defense = EditorGUILayout.IntField("Defense", cardInfo.defense);
         cardInfo.damage = EditorGUILayout.IntField("Damage", cardInfo.damage);
      }

      if (cardInfo.subtype == CardInfo.SubtypeCard.AuxiliaryBuild)
      {
         cardInfo.defense = EditorGUILayout.IntField("Defense", cardInfo.defense);
         cardInfo.heal = EditorGUILayout.IntField(("Heal"), cardInfo.heal);
      }
      
   }
}
