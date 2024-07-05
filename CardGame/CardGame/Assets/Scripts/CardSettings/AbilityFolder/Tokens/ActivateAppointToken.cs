using CardSettings.Appoint;
using UnityEngine;

namespace CardSettings.AbilityFolder.Tokens
{
    public class ActivateAppointToken : MonoBehaviour
    {
        [SerializeField] private AppointToken _appointToken;

        public void ActivatingAppoint()
        {
            _appointToken.BeginAppointToken();
        }
    }
}