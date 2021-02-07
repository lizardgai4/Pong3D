using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallBehavior : MonoBehaviour
{
    public float vertical = -1f;
    public float horizontal = 1f;
    public int leftScore = 0;
    public int rightScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //bounce off the top and bottom walls
        if (transform.localPosition.z > 4.8f || transform.localPosition.z < -4.8f)
            vertical *= -1;

        //left scores
        if (transform.localPosition.x > 7.8f) {
            Reset(true);
        }
        
        //right scores
        if (transform.localPosition.x < -7.8f) {
            Reset(false);
        }

        //move the ball
        transform.Translate(Vector3.forward * vertical / 100f);
        transform.Translate(Vector3.right * horizontal / 100f);
    }

    private void Reset(bool leftWin)
    {
        transform.localPosition = new Vector3(0, 1, 0);
        vertical = -1f;

        //left scores
        if (leftWin) {
            leftScore++;
            horizontal = 1f;
            Debug.Log("Left scores!  Score is " + leftScore + " left, " + rightScore + " right");

            if (leftScore >= 11) {
                Debug.Log("Left wins!  Game over");
                leftScore = rightScore = 0;
            }

            return;
        }

        //right scores
        rightScore++;
        horizontal = -1f;
        Debug.Log("Right scores!  Score is " + leftScore + " left, " + rightScore + " right");

        if (rightScore >= 11)
        {
            Debug.Log("Right wins!  Game over");
            leftScore = rightScore = 0;
        }
    }

    //When the ball hits a paddle
    private void OnTriggerEnter(Collider other)
    {
        //Have it bounce at different angles depending on where on the paddle it's hit
        vertical = transform.position.z - other.transform.position.z;
        vertical *= 2f;

        //make the ball go faster
        horizontal *= -1.1f ;

        //make sure the vertical speed accelerates proportionally to the horizontal speed
        vertical *= Math.Abs(horizontal);
    }
}
