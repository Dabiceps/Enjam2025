using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private float time;
    [SerializeField] private TextMeshProUGUI textMesh;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        time = gameManager.FixTime;
    }

    void Update()
    {
        if (gameManager.isActive) 
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }

            if (Mathf.FloorToInt(time) != gameManager.FixTime)
            {
                time = gameManager.FixTime;
            }

            int seconds = Mathf.FloorToInt(time);
            int milliseconds = Mathf.FloorToInt((time - seconds) * 100);

            //Debug.Log($"{seconds:00}:{milliseconds:00}");
            textMesh.text = $"{seconds:00}:{milliseconds:00}";
        }
        else
        {
            time = 5;
        }
    }
}