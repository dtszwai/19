using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Potion : ItemBase
    {
        protected override void Effect(Player player)
        {
            player.Heal();
        }
    }
}