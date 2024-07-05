using UnityEngine;

namespace CardSettings.CardPrefabSettings
{
    public class TokenInCardSystem : MonoBehaviour, ITokenInCardSystem
    {
        [SerializeField] private bool canAttack = false;
        
        #region FearTokenSettings
        
        public bool _canAttack => canAttack;
        
        public void CardHaveFearToken()
        {
            canAttack = false;
        }

        public void DeleteFearToken()
        {
            canAttack = true;
        }

        #endregion
    }
}
