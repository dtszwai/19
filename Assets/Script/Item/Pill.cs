using UnityEngine;

namespace Shmup
{
    public class Pill : ItemBase
    {
        [SerializeField] GameObject Aura;
        [SerializeField] int duration = 5;

        protected override void Effect(Player player)
        {
            GameObject AuraInstance = Instantiate(Aura, new Vector3(0, 0, 0), Quaternion.identity);
            AuraInstance.transform.SetParent(player.gameObject.transform, false);
            player.SetInvincible(duration);
            Destroy(AuraInstance, duration);
        }
    }
}