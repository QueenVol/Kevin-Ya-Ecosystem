using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SupermanBehavior : MonoBehaviour
{
    private enum State
    {
        Flying,
        Speedy,
        Return,
        Wondering,
        Bye
    }
    private State currentState = State.Flying;
    public float speed;
    public float radius;
    public float dir;
    public float rotationSpeed;
    public float maxRadius;
    private bool re = true;
    private bool canReturn = true;
    private int countdown;
    public AudioSource SupermanFlies;
    public AudioSource SupermanSpeeds;
    public AudioSource SupermanReturns;
    public AudioSource SupermanByes;
    private bool flyPlays = true;
    private bool speedPlays = true;
    private bool returnPlays = true;
    private bool byePlays = true;

    // Start is called before the first frame update
    void Start()
    {
        countdown = Random.Range(10, 500);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Flying:
                flying();
                if (returnPlays)
                {
                    SupermanReturns.Play();
                    returnPlays = false;
                }
                break;

            case State.Speedy:
                speedy();
                if (speedPlays)
                {
                    SupermanFlies.Stop();
                    SupermanSpeeds.Play();
                    speedPlays = false;
                }
                break;

            case State.Return:
                theReturn();
                speedPlays = true;
                flyPlays = true;
                break;
                
            case State.Wondering:
                wondering();
                if (flyPlays)
                {
                    SupermanFlies.Play();
                    flyPlays = false;
                }
                break;

            case State.Bye:
                if (byePlays)
                {
                    SupermanFlies.Stop();
                    SupermanByes.Play();
                    byePlays=false;
                }
                if(SupermanByes.isPlaying == false)
                {
                    Destroy(this.gameObject);
                }
                break;
        }

        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, -1));
        if (distance > maxRadius && canReturn)
        {
            canReturn = false;
            currentState = State.Return;
        }
        if(countdown <= 0)
        {
            currentState = State.Bye;
        }
    }

    private void flying()
    {
        float x = Mathf.Cos(dir) * radius;
        float y = Mathf.Sin(dir) * radius;
        this.transform.position = new Vector3(x, y, -1);
        dir += speed * Time.deltaTime;
        this.transform.Rotate(0, 0, 10f * Time.deltaTime * rotationSpeed);
    }

    private void speedy()
    {
        transform.position += transform.up * (Time.deltaTime * speed * 2);
        canReturn = true ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "The Rock")
        {
            currentState = State.Speedy;
        }
        if (collision.gameObject.tag == "Aquaman")
        {
            currentState = State.Bye;
        }
    }

    private void theReturn()
    {
        if (re)
        {
            transform.Rotate(0, 0, Random.Range(0f, 360f));
            countdown--;
            re = false;
        }
        currentState = State.Wondering;
    }

    private void wondering()
    {
        transform.position += transform.up * (Time.deltaTime * speed);
        re = true;
        canReturn = true;
    }
}
