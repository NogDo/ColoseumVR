using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAttack : MonoBehaviour
{
    public GameObject objPlayer;
    public GameObject objShield;
    public Shield shield;
    public PlayerInfo playerInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (shield.isBreak)
        {
            shield.SetDefend(false);
            if (other.gameObject == objPlayer)
            {
                Debug.Log("플레이어 피격 확인");
                playerInfo.GetHit();
            }
        }
        else
        {
            if (other.gameObject == objShield)
            {
                Debug.Log("방패 피격 확인");
                shield.SetDefend(true);
                shield.GetHit_To_Shield();
            }

            if (!shield.isDefend)
            {
                if (other.gameObject == objPlayer)
                {
                    Debug.Log("플레이어 피격 확인");
                    playerInfo.GetHit();
                }
            }
        }
    }

    public void EndAttack()
    {
        shield.SetDefend(false);
    }
}
