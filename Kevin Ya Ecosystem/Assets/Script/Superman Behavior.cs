using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermanBehavior : MonoBehaviour
{
    private enum State
    {
        Rotate,
        Speedy,
        Bye
    }
    private State currentState = State.Rotate;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Rotate:
                rotate();
                break;

            case State.Speedy:
                speedy();
                break;

            case State.Bye:

                break;
        }
    }

    private void rotate()
    {
        this.transform.Rotate(Time.deltaTime * Vector3.forward * Random.Range(0f, 360f));
        transform.position += transform.up * (Time.deltaTime * speed);
    }

    private void speedy()
    {
        transform.position += transform.up * (Time.deltaTime * speed * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "The Rock")
        {
            currentState = State.Speedy;
        }
    }
}
