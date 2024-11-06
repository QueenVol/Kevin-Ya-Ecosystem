using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamanBehavior : MonoBehaviour
{
    private enum State
    {
        Swim,
        Jump,
        Walk,
        Dive,
        Born,
        Death
    }
    private State curretState = State.Swim;
    public float speed;
    public float rotateSpeed;
    private bool toLeft = false;
    private bool canRotate = true;
    public GameObject Aquaman;
    private State preState;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 270);
    }

    // Update is called once per frame
    void Update()
    {
        switch (curretState)
        {
            case State.Swim:
                swim();
                preState = State.Swim;
                break;

            case State.Jump:
                jump();
                preState= State.Jump;
                break;

            case State.Walk:
                walk();
                preState= State.Walk;
                break;

            case State.Dive:
                dive();
                preState= State.Dive;
                break;

            case State.Born:
                born();
                break;

            case State.Death:
                Destroy(this.gameObject);
                break;
        }
        if (this.transform.position.y < -5.5f)
        {
            curretState = State.Death;
        }
    }

    private void swim()
    {
        canRotate = true;
        transform.position += transform.up * (Time.deltaTime * speed );
        if(transform.position.x < -4.2f)
        {
            transform.Rotate(0, 0, 180);
            transform.position = new Vector3(-4.2f, transform.position.y, transform.position.z);
        }
        if(transform.position.x > -0.3f)
        {
            curretState = State.Jump;
        }
    }

    private void jump()
    {
        if(canRotate)
        {
            transform.Rotate(0, 0, 90);
            canRotate = false;
        }
        transform.position += transform.up * (Time.deltaTime * speed * 2.1f);
        transform.position += transform.right * (Time.deltaTime * speed * 3f);
        if(transform.position.x > 1)
        {
            curretState = State.Walk;
        }
    }

    private void walk()
    {
        canRotate = true;
        if (!toLeft)
        {
            transform.position += transform.up * (Time.deltaTime * speed * 0.15f);
            transform.position += transform.right * (Time.deltaTime * speed);
        }
        if (toLeft)
        {
            transform.position -= transform.up * (Time.deltaTime * speed * 0.15f);
            transform.position -= transform.right * (Time.deltaTime * speed);
        }
        if (transform.position.x > 4.7)
        {
            toLeft = true;
        }
        if(transform.position.x < 1)
        {
            curretState = State.Dive;
        }
    }

    private void dive()
    {
        toLeft = false;
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
            canRotate = false;
        }
        transform.position += transform.up * (Time.deltaTime * speed * 3f);
        transform.position -= transform.right * (Time.deltaTime * speed * 2.1f);
        if (transform.position.y <= -4.5f)
        {
            curretState = State.Swim;
        }
    }

    private void born()
    {
        Instantiate(Aquaman, new Vector3(-4.2f, -4.5f, -1), Quaternion.identity);
        curretState = preState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Superman")
        {
            curretState = State.Born;
        }
        if (collision.gameObject.tag == "The Rock")
        {
            curretState = State.Death;
        }
        if (collision.gameObject.tag == "Aquaman")
        {
            int i = Random.Range(0, 3);
            if (i == 0)
            {
                curretState = State.Death;
            }
        }
    }
}
