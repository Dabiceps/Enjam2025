using System;
using System.Collections;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI textTapingScore;
    [SerializeField] private AudioSource tapingSound;
    private static bool win = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.Pause();
        tapingSound.Play();
        tapingSound.Pause();
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

            textTapingScore.text = inputTouched.ToString();
            if (inputTouched >= maxForWinning)
            {
                Debug.Log("Bravo !!!");
                win = true;
                End_TaperClavier();
            }

            if (gameManager.MiniGameTime <= 0.1f)
            {
                win = false;
                End_TaperClavier();
                inputTouched = 0;
            }
            
        }
    }

    public void Start_TaperClavier(int difficulty)
    {
        tapingSound.Pause();
        inputTouched = 0;
        StopAllCoroutines();
        StartCoroutine(waitUntilPopDisapear());
        switch (difficulty)
        {
            case 1:
                maxForWinning = 15;
                break;
            case 2:
                maxForWinning = 25;
                break;
            case 3:
                maxForWinning = 30;
                break;
        }
    }

    IEnumerator waitUntilPopDisapear()
    {
        yield return new WaitForSeconds(gameManager.readTime + 2f);
        started = true;
        videoPlayer.Stop();
        videoPlayer.Pause();
        tapingSound.Pause();
    }

    
    
    IEnumerator WaitVideo()
    {
        oneTime = true;
        lastInput = Input.inputString;
        inputTouched++;
        //Debug.Log("Taper Clavier : " + Input.inputString);
        tapingSound.UnPause();
        videoPlayer.Play();
        yield return new WaitForSeconds(seconds);
        videoPlayer.Pause();
        tapingSound.Pause();
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
