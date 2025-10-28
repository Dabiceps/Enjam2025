using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Video;

public class TaperClavier : MonoBehaviour
{
    [SerializeField] private bool started = false;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float seconds;
    [SerializeField] private bool oneTime = false;
    [SerializeField] private string lastInput = "";
    [SerializeField] private int inputTouched = 0;
    [SerializeField] private int maxForWinning = 15;
    [SerializeField] private GameManager gameManager;
    private static bool win = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.Pause();
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started == true)
        {
            if (Input.anyKeyDown && oneTime == false)
            {
                if(Enum.TryParse(lastInput.ToUpper(),out KeyCode key))
                {
                    //Debug.Log("Dernière touche : " + key);
                    if (Input.GetKeyDown(key))
                    {
                        //Debug.Log(key + " a déjà été pressé !");
                    }
                    else
                    {
                        StartCoroutine(WaitVideo());
                    }
                }
                else
                {
                    StartCoroutine(WaitVideo());
                }
            }

            if (inputTouched >= maxForWinning)
            {
                Debug.Log("Bravo !!!");
                win = true;
                End_TaperClavier();
            }

            if (gameManager.MiniGameTime <= 0)
            {
                win = false;
                End_TaperClavier();
            }
            
        }
    }

    public void Start_TaperClavier()
    {
        started = true;
        videoPlayer.Stop();
        videoPlayer.Pause();
    }

    
    
    IEnumerator WaitVideo()
    {
        oneTime = true;
        lastInput = Input.inputString;
        inputTouched++;
        //Debug.Log("Taper Clavier : " + Input.inputString);
        videoPlayer.Play();
        yield return new WaitForSeconds(seconds);
        videoPlayer.Pause();
        oneTime = false;
        Debug.Log("End Wait");
    }

    public bool End_TaperClavier()
    {
        if (!gameManager.isActive)
        {
            started = false;
            oneTime = false;
            return win;
        }
        else
        {
            return win;
        }
    }
}
