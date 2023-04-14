using UnityEngine;
using UnityEngine.UI;


public class DisplayPlayer2Score : MonoBehaviour
{
    public Text text;
    private bool hasConditionRun = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            if (!hasConditionRun)
            {
                GameOver();
            }

        }
    }

    private void GameOver()
    {
        if(GameManager.instance.numberOfPlayers == 2)
        {
            hasConditionRun = true;
            TankController tankController = GameManager.instance.players.Find(pc => pc.gameObject.name == "PlayerController2").GetComponent<TankController>();
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
