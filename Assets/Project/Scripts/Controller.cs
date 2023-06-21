using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const int remainingPuzzle = 10;
    [SerializeField]
    private GameDataSO dataSO;
    [SerializeField]
    private TMP_Text score;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private TMP_Text winText;
    [SerializeField]
    private ParticleSystem endEffect;
    public GameDataSO DataSO { get { return dataSO; } }
    public static Controller instance;
    private void Start()
    {
        instance = this;
        score.text = $"{dataSO.playerWin}:{dataSO.rabbitWin}";
    }
    public void PlayerWin()
    {
        winText.text = "Player Win!";
        winPanel.SetActive(true);
        endEffect.Play();
    }
    public void RabbitWin()
    {
        winText.text = "Rabbit Win!";
        winPanel.SetActive(true);
    }
}
