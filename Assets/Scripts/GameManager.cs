using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Decla variables serialisées
    [SerializeField] private GameObject FZ; // La zone de drop


    // Variables de coroutine
    public int MiniGameTime;
    private int FixTime;
    private int MiniGames = 1;

    // Décla des classes
    FileDropZone fz;

    // Autres
    bool CheckGood;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fz = FZ.GetComponent<FileDropZone>();
        GameStart();
    }

    public void GameStart()
    {
        Debug.Log("Début du jeu");

        // Apparition bouton discord



        // Quand on le clique, on bouge le build, on attend 2sec, on affiche le premier pop-up et on attend x sec
        // Affichage du timer
        //Start coroutine + start premier mini-jeu


        StartCoroutine(CountdownCoroutine());
        fz.StartDragDropGame();
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
        if (MiniGames == 1)
        {
            MiniGames++;
            CheckGood = fz.EndDragDropGame();
            if (CheckGood)
            {
                Debug.Log("Mini-jeu réussi!");
            }
            else
            {
                Debug.Log("Mini-jeu échoué!");
            }
            yield return new WaitForSeconds(2f);
            StartCoroutine(CountdownCoroutine());
            fz.StartDragDropGame();
        }
        if (MiniGames == 2)
        {
            Debug.Log("Début du mini-jeu 2");
        }
    }
}
