using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Decla variables serialisées
    [SerializeField] private GameObject FZ; // La zone de drop
    [SerializeField] private GameObject TC; // GameObject taper clavier
    [SerializeField] private GameObject DragDrop; // Prefab dragdrop
    [SerializeField] private GameObject TaperClavier; // Prefab TaperClavier
    [SerializeField] private GameObject PointClick; // Prefab pointclick


    // Variables de coroutine
    public int MiniGameTime;
    private int FixTime;
    private int MiniGames = 0;

    // Décla des classes
    FileDropZone fz;
    TaperClavier tc;

    // Autres
    bool win;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fz = FZ.GetComponent<FileDropZone>();
        tc = TC.GetComponent<TaperClavier>();
        GameStart();
    }

    public void GameStart()
    {
        Debug.Log("Début du jeu");

        // Apparition bouton discord



        // Quand on le clique, on bouge le build, on attend 2sec, on affiche le premier pop-up et on attend x sec
        // Affichage du timer
        //Start coroutine + start premier mini-jeu
        StartCoroutine(GameStartCoroutine());
    }

    public IEnumerator CountdownCoroutine()
    {
        Debug.Log("Coroutine start");
        FixTime = MiniGameTime;
        while (FixTime > 0)
        {
            yield return new WaitForSeconds(1);
            FixTime--;
            Debug.Log("Temps restant: " + FixTime + " secondes");
        }
        
        if (MiniGames == 0)
        {
            // truc de fin mini jeu point n click
            tc.Start_TaperClavier();
            SetPrefab(TaperClavier, DragDrop, PointClick);
            StartCoroutine(CountdownCoroutine());
            Debug.Log("fin PnC");
        }
        if (MiniGames == 1)
        {
            // truc de fin de mini jeu mashing clavier
            fz.StartDragDropGame();
            SetPrefab(DragDrop, TaperClavier, PointClick);
            StartCoroutine(CountdownCoroutine());
            Debug.Log("fin TC");
        }
        if (MiniGames == 2)
        {
            //truc de fin de mini jeu drag drop
            //Méthode start mini jeu point n click
            Debug.Log("Fin DnD");
            MiniGames = 0;
            SetPrefab(PointClick, TaperClavier, DragDrop);
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
        // Reduire l'écran de build 
        yield return new WaitForSeconds(2f);

        // Start point n click minigame
        SetPrefab(PointClick, TaperClavier, DragDrop);
        StartCoroutine(CountdownCoroutine());

    }

    public void SetPrefab (GameObject ActivePrefab, GameObject NonActivePrefab1, GameObject NonActivePrefab2)
    {
        ActivePrefab.SetActive(true);
        NonActivePrefab1.SetActive(false);
        NonActivePrefab2.SetActive(false);
    }

}
