using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Decla variables serialis�es
    [Header("Pr�fabs des mini-jeux")]
    [SerializeField] private GameObject DragDrop; // Prefab dragdrop
    [SerializeField] private GameObject TaperClavier; // Prefab TaperClavier
    [SerializeField] private GameObject TitleScreen; // Prefab TitleScreen

    [Header("Prefabs PointClick")]
    [SerializeField] private GameObject PointClick1; // Prefab pointclick
    [SerializeField] private GameObject PointClick2; // Prefab pointclick
    [SerializeField] private GameObject PointClick3; // Prefab pointclick
    [SerializeField] private List<GameObject> ImagesBelles;
    [SerializeField] private List<GameObject> ImagesMoches;
    private GameObject PointClick; // Prefab pointclick
    private int numero = 0;

    [Header("Scripts")]
    [SerializeField] private FileDropZone fz; // La zone de drop
    [SerializeField] private TaperClavier tc; // Le taper clavier
    [SerializeField] private PopupDiscord popupDiscord; // Popup discord
    [SerializeField] private BuildBar buildBar;



    // Variables de coroutine
    [Header("Variables de jeu")]
    public int MiniGameTime;
    public int FixTime;
    public bool isActive;
    private int MiniGames = 0;
    private int difficulty = 1;
    private int totalGames = 0;
    private float readTime = 9f;


    // Autres
    bool win;
    public int popupNumber = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointClick = PointClick1;
        DragDrop.SetActive(false);
        TaperClavier.SetActive(false);
        PointClick.SetActive(false);
        TitleScreen.SetActive(true);
        
        // Apparition bouton discord
        popupDiscord.createPopUp(popupNumber, true);
    }

    public void GameStart()
    {
        Debug.Log("D�but du jeu");

        // Quand on le clique, on bouge le build, on attend 2sec, on affiche le premier pop-up et on attend x sec
        // Affichage du timer
        //Start coroutine + start premier mini-jeu
        StartCoroutine(GameStartCoroutine());
    }

    public IEnumerator CountdownCoroutine()
    {
        
        if (difficulty == 4)
        {
            StartCoroutine(EndGameCoroutine());
            yield break;
        }

        popupDiscord.createPopUp(popupNumber, false);
        yield return new WaitForSeconds(readTime);
        popupDiscord.createPopUp(popupNumber, false);
        popupDiscord.destroyPopUp();
        yield return new WaitForSeconds(2f);

        Debug.Log("Coroutine start");
        isActive = true;
        FixTime = MiniGameTime;
        while (FixTime >= 0)
        {
            yield return new WaitForSeconds(1);
            FixTime--;
            Debug.Log("Temps restant: " + FixTime + " secondes");
        }
        isActive = false;
        
        if (MiniGames == 0)
        {
            win = PointClickVictory();
            AfficherResultat();
            yield return new WaitForSeconds(2f);
            EffacerImage();
            buildBar.buildBar(totalGames, win);
            totalGames++;
            Debug.Log(win.ToString());
            SetPrefab(TaperClavier, DragDrop, PointClick);
            
            tc.Start_TaperClavier(difficulty);
            StartCoroutine(CountdownCoroutine());
        }
        if (MiniGames == 1)
        {
            // truc de fin de mini jeu mashing clavier
            win = tc.End_TaperClavier();
            buildBar.buildBar(totalGames, win);
            totalGames++;

            SetPrefab(DragDrop, TaperClavier, PointClick);
            
            fz.StartDragDropGame(difficulty);
            StartCoroutine(CountdownCoroutine());
        }
        if (MiniGames == 2)
        {
            //truc de fin de mini jeu drag drop
            win = fz.EndDragDropGame();
            buildBar.buildBar(totalGames, win);
            totalGames++;
            //M�thode start mini jeu point n click
            MiniGames = 0;
            
            difficulty++;
            readTime = 4f;
            SetPrefab(PointClick, TaperClavier, DragDrop, difficulty);
            StartCoroutine(CountdownCoroutine());
            yield break;
        }
        MiniGames++;
        
    }

    public IEnumerator GameStartCoroutine()
    {
        Debug.Log("Debut de la gamestart coroutine");
        buildBar.buildBar(totalGames, win, true);
        yield return new WaitForSeconds(3f);
        buildBar.closeBuildFullBar();

        // Start point n click minigame
        TitleScreen.SetActive(false);
        SetPrefab(PointClick, TaperClavier, DragDrop, difficulty);
        
        StartCoroutine(CountdownCoroutine());

    }

    public IEnumerator EndGameCoroutine()
    {
        Debug.Log("Fin de partie");
        yield return null;
    }



    public void SetPrefab(GameObject ActivePrefab, GameObject NonActivePrefab1, GameObject NonActivePrefab2, int difficulty = 0)
    {
        ActivePrefab.SetActive(true);
        NonActivePrefab1.SetActive(false);
        NonActivePrefab2.SetActive(false);

        switch (difficulty)
        {
            case 1:
                PointClick1.SetActive(true);
                PointClick2.SetActive(false);
                PointClick3.SetActive(false);
                break;
            case 2:
                PointClick1.SetActive(false);
                PointClick2.SetActive(true);
                PointClick3.SetActive(false);
                break;
            case 3:
                PointClick1.SetActive(false);
                PointClick2.SetActive(false);
                PointClick3.SetActive(true);
                break;
            default:
                PointClick1.SetActive(false);
                PointClick2.SetActive(false);
                PointClick3.SetActive(false);
                break;

        }
    }

    private bool PointClickVictory()
    {
        bool win = false;
        switch (difficulty)
        {
            case 1:
                PointAndClickController1 pc1 = PointClick1.GetComponent<PointAndClickController1>();
                win = pc1.pointAndClickVictory; break;
            case 2:
                PointAndClickController2 pc2 = PointClick2.GetComponent<PointAndClickController2>();
                win = pc2.pointAndClickVictory; break;
            case 3:
                PointAndClickController3 pc3 = PointClick3.GetComponent<PointAndClickController3>();
                win = pc3.pointAndClickVictory; break;
            default: break;
        }
        return win;
    }

    private void AfficherResultat()
    {
        if (win)
        {
            switch (numero)
            {
                case 0:
                    ImagesBelles[0].SetActive(true);
                    break;
                case 1:
                    ImagesBelles[1].SetActive(true);
                    break;
                case 2:
                    ImagesBelles[2].SetActive(true);
                    break;
            }
        }
        else
        {
            Debug.Log("test");
            switch (numero)
            {
                case 0:
                    ImagesMoches[0].SetActive(true);
                    Debug.Log("test2");
                    break;
                case 1:
                    ImagesMoches[1].SetActive(true);
                    break;
                case 2:
                    ImagesMoches[2].SetActive(true);
                    break;
            }
        }
        numero++;
    }

    private void EffacerImage()
    {
        for (int i = 0; i < ImagesBelles.Count; i++)
        {
            ImagesBelles[i].SetActive(false);
            ImagesMoches[i].SetActive(false);
        }
    }


}
