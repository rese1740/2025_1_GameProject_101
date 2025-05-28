using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    public static bool canPlay = true;
    public static bool anyBallMoving = false;


    private void Update()
    {
        CheckAllBalls();

        if(!anyBallMoving && !canPlay)
        {
            canPlay = true ;
        }
    }

    void CheckAllBalls()
    {
        BallController[] allBalls = FindObjectsOfType<BallController>();
        anyBallMoving = false;

        foreach (BallController ball in allBalls)
        {
            if (ball.isMoving())
            {
                anyBallMoving = true;
                break;
            }

        }
    }

    public static void OnBallHit()
    {
        canPlay = false;
        Debug.Log("턴 시작! 공이 멈출 때까지 기다리세요");
    }
}
