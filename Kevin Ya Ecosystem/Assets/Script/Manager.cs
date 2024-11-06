using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject TheRock;
    public GameObject Superman;
    public GameObject Aquaman;
    private float TheRockX;
    public float superTime;
    private float superTimer;
    public float aquaTime;
    private float aquaTimer;

    // Start is called before the first frame update
    void Start()
    {
        superTimer = superTime;
        aquaTimer = aquaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("The Rock").Length == 0)
        {
            TheRockX = Random.Range(-4.6f, 4.6f);
            Instantiate(TheRock, new Vector3(TheRockX, 5.5f, -1), Quaternion.identity);
        }
        if (GameObject.FindGameObjectsWithTag("Superman").Length == 0)
        {
            superTimer += Time.deltaTime;
            if (superTimer >= superTime)
            {
                Instantiate(Superman, new Vector3(0, 0, -1), Quaternion.identity);
                superTimer = 0;
            }
        }
        if (GameObject.FindGameObjectsWithTag("Aquaman").Length == 0)
        {
            aquaTimer += Time.deltaTime;
            if (aquaTimer >= aquaTime)
            {
                Instantiate(Aquaman, new Vector3(-4.2f, -4.5f, -1), Quaternion.identity);
                aquaTimer = 0;
            }
        }
    }
}
