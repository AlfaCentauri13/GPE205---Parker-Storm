using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayer1Score : MonoBehaviour
{
    // create text reference
    public Text text;
    private bool hasConditionRun = false;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameOver)
        {
            if(!hasConditionRun)
            {
                GameOver();
            }
            
        }
    }

    // Game Over handler. This will display the score to the screen
    private void GameOver()
    {
        hasConditionRun = true;
        TankPawn pawn = GameManager.instance.playerTanks[0].GetComponent<TankPawn>();
        if (pawn != null)
        {
            TankController tankController = GameManager.instance.players.Find(pc => pc.gameObject.name == "PlayerController1").GetComponent<TankController>();
            if (tankController != null)
            {
                int? score = tankController.TankScore;
                if (score != null)
                {
                    text.text = score.ToString();
                }
            }
        }
    }
}
