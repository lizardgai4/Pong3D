using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text displayLeftScore;
    public Text displayRightScore;

    private int leftScore = 0;
    private int rightScore = 0;

    public void Score(bool side, int newScore) {
        if (side)
        {
            leftScore = newScore;
            displayLeftScore.text = newScore.ToString();
            float moreRed = 1f - (leftScore / 12f);
            displayLeftScore.color = new Color(1f, moreRed, moreRed);
        }
        else
        {
            rightScore = newScore;
            displayRightScore.text = newScore.ToString();
            float moreBlue = 1f - (rightScore / 12f);
            displayRightScore.color = new Color(moreBlue, moreBlue, 1f);
        }
    }
}
