using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector_Lion;
    public PlayableDirector playableDirector_Camera;
    public PlayableDirector playableDirector_Circus;
    public PlayableDirector playableDirector_Circus2;
    public PlayableDirector playableDirector_GlradiatorFight;
    public PlayableDirector playableDirector_GlradiatorBoxing;

    public GameObject objLineRenderer;
    public Canvas canvas_Chapter;
    public GameObject objChapter;
    public InfoLion infoLion;
    public GameObject objPlayer;
    public GameObject objPlayerTransform;
    public GameObject objCameraCanvas;

    public Vector3 vector_Chapture;
    public Vector3 vector_King;
    public Vector3 vector_Audience;
    public Vector3 vector_Colosseum;


    private void Start()
    {
        vector_Chapture = objPlayerTransform.transform.position;
        vector_King = new Vector3(27.61f, 4.8f, 0.012f);
        vector_Audience = new Vector3(-29.727f, 4.445f, 2.47f);
        vector_Colosseum = new Vector3(0, 10, 5);
    }

    public void MovePoseKing()
    {
        objPlayerTransform.transform.position = vector_King;
        objPlayerTransform.transform.rotation = Quaternion.Euler(new Vector3(0, -90f, 0));
    }

    public void MovePoseAudience()
    {
        objPlayerTransform.transform.position = vector_Audience;
    }

    public void MovePoseColosseum()
    {
        objPlayerTransform.transform.position = vector_Colosseum;
    }

    public void MovePoseChapture()
    {
        objPlayerTransform.transform.position = vector_Chapture;
        objPlayerTransform.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void MovePoseMiddle()
    {
        objPlayerTransform.transform.position = new Vector3(0, 1.3f, 0);
    }

    public void InitTimeline()
    {
        ScSoundManager.gSoundManager.InitSounds(); // 사운드추가

        canvas_Chapter.gameObject.SetActive(false);
        objChapter.SetActive(false);
        objLineRenderer.GetComponent<LineRenderer>().enabled = false;
    }

    public void EndTimeLine()
    {
        canvas_Chapter.gameObject.SetActive(true);
        objChapter.SetActive(true);
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
    }

    public void StartLionGame()
    {
        InitTimeline();
        objPlayer.GetComponent<CharacterController>().enabled = false;
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
        playableDirector_Lion.gameObject.SetActive(true);
        playableDirector_Lion.Play();

        ScSoundManager.gSoundManager.audiosBgms[1].Play(); // 사운드추가
        ScSoundManager.gSoundManager.audiosBgms[2].Play(); // 사운드추가
    }

    public void StartColosseumCamera()
    {
        InitTimeline();
        canvas_Chapter.gameObject.SetActive(true);
        objChapter.SetActive(true);
        playableDirector_Camera.gameObject.SetActive(true);
        objPlayer.GetComponent<CharacterController>().enabled = false;
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
        playableDirector_Camera.Play();

        ScSoundManager.gSoundManager.audiosBgms[1].Play(); // 사운드추가
    }

    public void StartCircus()
    {
        InitTimeline();
        objPlayer.GetComponent<CharacterController>().enabled = false;
        playableDirector_Circus.gameObject.SetActive(true);
        playableDirector_Circus.Play();
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
        objCameraCanvas.SetActive(true);

        ScSoundManager.gSoundManager.audiosBgms[1].Play(); // 사운드추가
    }

    public void StartCircus2()
    {
        playableDirector_Circus.gameObject.SetActive(false);
        playableDirector_Circus2.gameObject.SetActive(true);
        playableDirector_Circus2.Play();
    }

    public void StartGlradiatorFight()
    {
        InitTimeline();
        playableDirector_GlradiatorFight.gameObject.SetActive(true);
        objPlayer.GetComponent<CharacterController>().enabled = false;
        playableDirector_GlradiatorFight.Play();
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
        objCameraCanvas.SetActive(true);

        ScSoundManager.gSoundManager.audiosBgms[1].Play(); // 사운드추가
    }
    
    public void StartGlradiatorBoxing()
    {
        InitTimeline();
        playableDirector_GlradiatorBoxing.gameObject.SetActive(true);
        objPlayer.GetComponent<CharacterController>().enabled = false;
        playableDirector_GlradiatorBoxing.Play();
        objPlayerTransform.transform.position = vector_King;
        objPlayerTransform.transform.rotation = Quaternion.Euler(new Vector3(0, -90f, 0));
        objLineRenderer.GetComponent<LineRenderer>().enabled = true;
        objCameraCanvas.SetActive(true);

        ScSoundManager.gSoundManager.audiosBgms[1].Play(); // 사운드추가
    }

    public void EndTimelineLion()
    {
        Debug.Log("Lion Timeline 종료이벤트 확인");
        playableDirector_Lion.gameObject.SetActive(false);
        objPlayer.GetComponent<CharacterController>().enabled = true;
    }

    public void EndTimelineColosseum()
    {
        Debug.Log("Colosseum Timeline 종료이벤트 확인");
        EndTimeLine();
        playableDirector_Camera.gameObject.SetActive(false);
        objPlayer.SetActive(true);
    }

    public void EndCircus()
    {
        Debug.Log("Circus Timeline 종료이벤트 확인");
        EndTimeLine();
        playableDirector_Circus.gameObject.SetActive(false);
        objCameraCanvas.SetActive(false);

        ScSoundManager.gSoundManager.InitSounds(); // 사운드추가
        ScSoundManager.gSoundManager.audiosBgms[0].Play(); // 사운드추가
    }

    public void EndGlradiatorFight()
    {
        EndTimeLine();
        playableDirector_GlradiatorFight.gameObject.SetActive(false);
        objCameraCanvas.SetActive(false);

        ScSoundManager.gSoundManager.InitSounds(); // 사운드추가
        ScSoundManager.gSoundManager.audiosBgms[0].Play(); // 사운드추가
    }

    public void EndGlradiatorBoxing()
    {
        EndTimeLine();
        playableDirector_GlradiatorBoxing.gameObject.SetActive(false);
        objPlayerTransform.transform.position = vector_Chapture;
        objPlayerTransform.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        objCameraCanvas.SetActive(false);

        ScSoundManager.gSoundManager.InitSounds(); // 사운드추가
        ScSoundManager.gSoundManager.audiosBgms[0].Play(); // 사운드추가
    }

    public void EndLionFight()
    {
        EndTimeLine();

        objPlayer.transform.localPosition = Vector3.zero; // 위치초기화 문제발생 이것도 초기화 어디서 해당 수치가 변경되는것인지?
        objPlayerTransform.transform.position = vector_Chapture;
        objPlayerTransform.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        objCameraCanvas.SetActive(false);

        ScSoundManager.gSoundManager.InitSounds(); // 사운드추가
        ScSoundManager.gSoundManager.audiosBgms[0].Play(); // 사운드추가
    }
}
