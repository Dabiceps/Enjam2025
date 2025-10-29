using System.Collections;
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
    private GameObject PointClick; // Prefab pointclick

    [Header("Scripts")]
    [SerializeField] private FileDropZone fz; // La zone de drop
    [SerializeField] private TaperClavier tc; // Le taper clavier
    [SerializeField] private PopupDiscord popupDiscord; // Popup discord



    // Variables de coroutine
    [Header("Variables de jeu")]
    public int MiniGameTime;
    public int FixTime;
    public bool isActive;
    private int MiniGames = 0;
    private int difficulty = 1;

    


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
            // truc de fin mini jeu point n click
            SetPrefab(TaperClavier, DragDrop, PointClick);
            popupDiscord.createPopUp(popupNumber, false);
            yield return new WaitForSeconds(4f);
            popupDiscord.createPopUp(popupNumber, false);
            popupDiscord.destroyPopUp();
            yield return new WaitForSeconds(2f);
            tc.Start_TaperClavier(difficulty);
            StartCoroutine(CountdownCoroutine());
        }
        if (MiniGames == 1)
        {
            // truc de fin de mini jeu mashing clavier
            win = tc.End_TaperClavier();
            if (win)
            {
                Debug.Log("Vous avez gagn� le taperclavier !");
            }
            else
            {
                Debug.Log("Vous avez perdu le taperclavier !");
            }

            
            SetPrefab(DragDrop, TaperClavier, PointClick);
            popupDiscord.createPopUp(popupNumber, false);
            yield return new WaitForSeconds(4f);
            popupDiscord.createPopUp(popupNumber, false);
            popupDiscord.destroyPopUp();
            yield return new WaitForSeconds(2f);
            fz.StartDragDropGame(difficulty);
            StartCoroutine(CountdownCoroutine());
        }
        if (MiniGames == 2)
        {
            //truc de fin de mini jeu drag drop
            win = fz.EndDragDropGame();
            if (win) 
            {                 
                Debug.Log("Vous avez gagn� le dragdrop !");
            }
            else 
            {
                Debug.Log("Vous avez perdu le dragdrop !");
            }
            //M�thode start mini jeu point n click
            MiniGames = 0;
            
            difficulty++;
            SetPrefab(PointClick, TaperClavier, DragDrop, difficulty);
            StartCoroutine(CountdownCoroutine());
            yield break;
        }
        MiniGames++;
    }

    public IEnumerator GameStartCoroutine()
    {
        Debug.Log("Debut de la gamestart coroutine");
        // Faire apparaitre ecran de build
        yield return new WaitForSeconds(2f);
        // Reduire l'�cran de build 
        yield return new WaitForSeconds(2f);

        // Start point n click minigame
        TitleScreen.SetActive(false);
        SetPrefab(PointClick, TaperClavier, DragDrop, difficulty);
        popupDiscord.createPopUp(popupNumber, false);
        yield return new WaitForSeconds(4f);
        popupDiscord.createPopUp(popupNumber, false);
        popupDiscord.destroyPopUp();
        yield return new WaitForSeconds(2f);
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

}
