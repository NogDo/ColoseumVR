using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float fPlyaer_HP;

    public GameObject objLion;
    public GameObject objLionHandR;
    public GameObject objLionHandL;
    public Slider slider_HP;

    public GameObject objDefeatCanvas;
    public TimelineController timelineController;

    public GameObject objSword;
    public GameObject objShield;
    public Canvas canvas_PlayerHp;

    public void GetHit()
    {
        fPlyaer_HP -= 5;
        slider_HP.value = fPlyaer_HP;

        if(fPlyaer_HP <= 0)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            objDefeatCanvas.SetActive(true);
            Invoke(nameof(GoToLobby), 5.0f);
        }
    }

    public void GoToLobby()
    {
        gameObject.GetComponent<CharacterController>().enabled = false; 

        timelineController.EndLionFight();
        objDefeatCanvas.SetActive(false);
        objLion.SetActive(false);
        objSword.SetActive(false);
        objShield.SetActive(false);
        canvas_PlayerHp.gameObject.SetActive(false);
    }
}
