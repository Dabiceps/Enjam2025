using TMPro;
using UnityEngine;

public class PopupDiscord : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPopUp(string text)
    {
        popup.SetActive(true);
        textMeshPro.text = text;
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }
}
