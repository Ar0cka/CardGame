using UnityEditor;


[CustomEditor(typeof(CardInfo))]
public class CardInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CardInfo cardInfo = (CardInfo)target;

        DrawDefaultInspector();

        if (cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild || cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman)
      {
         cardInfo.defense = EditorGUILayout.IntField("Defense", cardInfo.defense);
         cardInfo.damage = EditorGUILayout.IntField("Damage", cardInfo.damage);
      }

      else if (cardInfo.subtype == CardInfo.SubtypeCard.AuxiliaryBuild)
      {
         cardInfo.defense = EditorGUILayout.IntField("Defense", cardInfo.defense);
         cardInfo.heal = EditorGUILayout.IntField(("Heal"), cardInfo.heal);
      }   
   }
}
