using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject objLionHandR;
    public GameObject objLionHandL;
    public PlayerInfo playerInfo;

    public float fShieldHp;
    public bool isBreak = false;
    public bool isDefend = false;

    public void GetHit_To_Shield()
    {
        fShieldHp -= 10.0f;
        if(fShieldHp <= 0)
        {
            BreakShield();
        }
    }

    public void BreakShield()
    {
        isBreak = true;
        gameObject.transform.parent = null;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void SetDefend(bool isDefend)
    {
        this.isDefend = isDefend;
    }
}
