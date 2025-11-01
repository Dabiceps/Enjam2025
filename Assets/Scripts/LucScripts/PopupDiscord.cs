using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDiscord : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject button;
    [SerializeField] private string[] text;
    [SerializeField] private float waitingTime;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource discordNotif;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPopUp(int nbrText, bool activeButton)
    {
        if (activeButton == true)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
        discordNotif.Play();
        popup.SetActive(true);
        textMeshPro.text = text[nbrText];
        gameManager.popupNumber++;
    }

    public void closePopup()
    {
        popup.SetActive(false);
        gameManager.GameStart();   
    }

    public void destroyPopUp()
    {
        StartCoroutine(waitBeforeDestroy());
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(waitingTime);
        popup.SetActive(false);
    }
}
