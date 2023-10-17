using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainBoard : MonoBehaviour
{
    public GameObject[] obj_ExplainBoard;

    public InfoLion infoLion;

    public GameObject objSword;
    public GameObject objSheild;

    public void NextBoard(int nIndex)
    {
        obj_ExplainBoard[nIndex].SetActive(false);
        obj_ExplainBoard[nIndex + 1].SetActive(true);

        if(nIndex == 2)
        {
            objSword.SetActive(true);
        }

        if(nIndex == 3)
        {
            objSheild.SetActive(true);
        }
    }

    public void Cancle()
    {
        infoLion.StartLionAi();
    }

    public void LionGameStart()
    {
        infoLion.StartLionAi();
    }
}
