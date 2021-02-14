using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BallBehavior : MonoBehaviour
{
    public float vertical = -1f;
    public float horizontal = 1f;
    public int leftScore = 0;
    public int rightScore = 0;
    public AudioClip wall;
    public AudioClip goal;
    public AudioClip win;
    public AudioClip paddle;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.pitch = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //bounce off the top and bottom walls
        if ((transform.localPosition.z > 4.8f && vertical > 0) || (transform.localPosition.z < -4.8f && vertical < 0)) {
            vertical *= -1;
            source.PlayOneShot(wall, Math.Abs(vertical));
        }
        //left scores
        if (transform.localPosition.x > 7.8f) {
            Reset(true);
        }
        
        //right scores
        if (transform.localPosition.x < -7.8f) {
            Reset(false);
        }

        //move the ball
        transform.Translate(Vector3.forward * vertical * Time.deltaTime * 2);
        transform.Translate(Vector3.right * horizontal * Time.deltaTime * 2);
    }

    private void Reset(bool leftWin)
    {
        //Reset all objects

        //Reset left paddle
        if (GameObject.Find("PaddleLeft").transform.localScale.z > 3f) {
            GameObject.Find("PaddleLeft").transform.localScale -= new Vector3(0, 0, 2);
        }

        //Reset right paddle
        if (GameObject.Find("PaddleRight").transform.localScale.z > 3f) {
            GameObject.Find("PaddleRight").transform.localScale -= new Vector3(0, 0, 2);
        }

        if (GameObject.Find("PowerUpOne").transform.position.y < 0.5f)
        {
            GameObject.Find("PowerUpOne").transform.Translate(Vector3.up * 2);
        }

        transform.localPosition = new Vector3(0, 1, 0);
        vertical = -1f;

        //left scores
        if (leftWin) {
            leftScore++;
            horizontal = 1f;

            //displayLeftScore.text = "" + leftScore;

            Debug.Log("Left scores!  Score is " + leftScore + " left, " + rightScore + " right");

            if (leftScore >= 11)
            {
                source.PlayOneShot(win, 1f);
                Debug.Log("Left wins!  Game over");
                leftScore = rightScore = 0;
            }
            else
            {
                source.PlayOneShot(goal, 1f);
            }

            //displayRightScore.text = rightScore.ToString();
            GameObject.Find("Scoreboard").GetComponent<Scoreboard>().Score(leftWin, leftScore);

            return;
        }

        //right scores
        rightScore++;
        horizontal = -1f;

        Debug.Log("Right scores!  Score is " + leftScore + " left, " + rightScore + " right");

        if (rightScore >= 11)
        {
            source.PlayOneShot(win, 1f);
            Debug.Log("Right wins!  Game over");
            leftScore = rightScore = 0;
        }
        else
        {
            source.PlayOneShot(goal, 1f);
        }

        //displayRightScore.text = rightScore.ToString();
        GameObject.Find("Scoreboard").GetComponent<Scoreboard>().Score(false, rightScore);
    }

    //When the ball hits a paddle
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Paddle"))
        {
            float hello = source.pitch;

            //Have it bounce at different angles depending on where on the paddle it's hit
            vertical = transform.position.z - other.transform.position.z;
            vertical *= 4f;
            vertical /= other.transform.localScale.z;

            //play sound, the more extreme angles make higher pitches
            source.pitch = (Math.Abs(vertical) / 2) + 1;
            //play sound
            source.PlayOneShot(paddle, 1f);

            //make the ball go faster
            horizontal *= -1.1f;

            //make sure the vertical speed accelerates proportionally to the horizontal speed
            vertical *= Math.Abs(horizontal);

            //reset pitch
            //source.pitch = hello;
        }
        else
        {
            other.transform.Translate(Vector3.down * 2);

            if (horizontal > 0)
            {
                GameObject.Find("PaddleLeft").transform.localScale += new Vector3(0, 0, 2);
            }
            else
            {
                GameObject.Find("PaddleRight").transform.localScale += new Vector3(0, 0, 2);
            }
        }
    }
}
