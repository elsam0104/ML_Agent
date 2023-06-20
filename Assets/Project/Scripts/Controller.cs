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
    public GameDataSO DataSO { get { return dataSO; } }
    public static Controller instance;
    private void Start()
    {
        instance = this;
        score.text = $"{dataSO.playerWin}:{dataSO.rabbitWin}";
    }
    public void PlayerWin()
    {

    }
    public void RabbitWin()
    {

    }
}
