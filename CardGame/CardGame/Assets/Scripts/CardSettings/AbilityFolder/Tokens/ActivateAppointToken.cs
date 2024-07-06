using CardSettings.AbilityFolder.Settings;
using CardSettings.Appoint;
using UnityEngine;

namespace CardSettings.AbilityFolder.Tokens
{
    public class ActivateAppointToken : MonoBehaviour, ITokenEffect
    {
        [SerializeField] private AppointToken _appointToken;

        public void ActivatingAppoint()
        {
            _appointToken.BeginAppointToken();
        }
    }
}