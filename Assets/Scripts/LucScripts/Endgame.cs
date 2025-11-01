using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject ecranFinBeau;
    [SerializeField] private GameObject ecranFin;
    [SerializeField] private GameObject ecranFinDeguelasse;
    [SerializeField] private GameObject ecranFin1;
    [SerializeField] private GameObject ecranFin2;
    [SerializeField] private GameObject ecranFin3;
    [SerializeField] private GameObject popup2;
    [SerializeField] private AudioSource error;
    [SerializeField] private AudioSource musicEnd;
    [SerializeField] private BuildBar buildBar;
    [SerializeField] private int nbrErreur;
    [SerializeField] private GameObject openEndGame;
    [SerializeField] private AudioSource gameplayMusic;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void launchEndgame()
    {
        openEndGame.SetActive(true);
    }

    void calculateError()
    {
        for (int i = 0; i < buildBar.erreur.Length; i++)
        {
            if (buildBar.erreur[i] == false)
            {
                nbrErreur++;
            }
        }
        
        if (nbrErreur == 0)
        {
            ecranFinDeguelasse.SetActive(false);
            ecranFinBeau.SetActive(true);
            ecranFin1.SetActive(false);
            ecranFin2.SetActive(false);
            ecranFin3.SetActive(false);
        }

        if (nbrErreur == 1)
        {
            ecranFinDeguelasse.SetActive(false);
            ecranFinBeau.SetActive(false);
            ecranFin1.SetActive(true);
            ecranFin2.SetActive(false);
            ecranFin3.SetActive(false);
        }

        if (nbrErreur == 2)
        {
            ecranFinDeguelasse.SetActive(false);
            ecranFinBeau.SetActive(true);
            ecranFin1.SetActive(false);
            ecranFin2.SetActive(true);
            ecranFin3.SetActive(false);
        }

        if (nbrErreur == 3)
        {
            ecranFinDeguelasse.SetActive(false);
            ecranFinBeau.SetActive(false);
            ecranFin1.SetActive(false);
            ecranFin2.SetActive(false);
            ecranFin3.SetActive(true);
        }

        if (nbrErreur >= 4)
        {
            ecranFinDeguelasse.SetActive(true);
            ecranFinBeau.SetActive(false);
            ecranFin1.SetActive(false);
            ecranFin2.SetActive(false);
            ecranFin3.SetActive(false);
        }
    }

    public void CloseDiscordNotificationOpenBuild()
    {
        gameplayMusic.Stop();
        ecranFin.SetActive(true);
        calculateError();
        popup.SetActive(false);
    }

    public void PlayBuild()
    {
        error.Play();
        musicEnd.Stop();    
        popup2.SetActive(true);
    }

    public void reloadMap()
    {
        SceneManager.LoadScene(0);
    }
}
