using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Mask : ItemBase
    {
        protected override void Effect(Player player)
        {
            player.SetProtected();
        }
    }
}