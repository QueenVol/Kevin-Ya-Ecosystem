using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheRockBehavior : MonoBehaviour
{
    private enum State
    {
        Falling,
        Explode,
        Duplicate,
        Destroy
    }
    private State currentState;
    public float speed;
    private Vector3 vel;
    private float timer;
    public float explosionTime;
    public float scale;
    private bool ifMax = false;
    private float x1;
    private float x2;
    private bool canDup = true;
    public GameObject TheRock;

    // Start is called before the first frame update
    void Start()
    {
        vel = this.transform.position;
        currentState = State.Falling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Falling:
                falling();
                break;

            case State.Explode:
                StartCoroutine(explode());
                break;

            case State.Duplicate:
                duplicate();
                break;

            case State.Destroy:
                Destroy(this.gameObject);
                break;
        }

        if (vel.y < -5.5f || vel.x < -4.7 || vel.x > 4.7)
        {
            currentState = State.Destroy;
        }

        if (ifMax)
        {
            currentState = State.Destroy;
        }
    }

    private void falling()
    {
        vel += new Vector3(0, -speed, 0);
        this.transform.position = vel;
    }

    private IEnumerator explode()
    {
        Vector2 startSize = this.transform.localScale;
        Vector2 maxSize = new Vector2(scale, scale);
        do
        {
            transform.localScale = Vector3.Lerp(startSize, maxSize, timer / explosionTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < explosionTime);
        ifMax = true;
    }

    private void duplicate()
    {
        x1 = this.transform.position.x + 1.5f;
        x2 = this.transform.position.x - 1.5f;
        if (canDup)
        {
            Instantiate(TheRock, new Vector3(x1, this.transform.position.y, -1), Quaternion.identity);
            Instantiate(TheRock, new Vector3(x2, this.transform.position.y, -1), Quaternion.identity);
            canDup = false;
        }
        currentState = State.Destroy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Superman")
        {
            currentState = State.Explode;
        }
        if(collision.gameObject.tag == "Aquaman")
        {
            currentState = State.Duplicate;
        }
    }
}
