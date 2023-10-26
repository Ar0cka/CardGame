
    using UnityEngine;

    public interface IPlayerBattleScene
    {
        int Health { get; set; }
        int Mana { get;}

        void UpgradeMana();
    }
