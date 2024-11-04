using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject TheRock;
    public GameObject Superman;
    public GameObject Aquaman;
    private float TheRockX;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Instantiate(Superman, new Vector3(0, 0, -1), Quaternion.identity);
        }
        if (GameObject.FindGameObjectsWithTag("Aquaman").Length == 0)
        {
            Instantiate(Aquaman, new Vector3(-4.2f, -4.5f, -1), Quaternion.identity);
        }
    }
}
