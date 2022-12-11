using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveforward : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10.0f;
    public Vector3 mouvement;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody>();
        mouvement = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(0f, 0.025f), Random.Range(0.05f, 0.08f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        StartCoroutine("moveBird");
    }

    IEnumerator moveBird()
    {
        rb.AddForce(mouvement * speed);
        yield return new WaitForSeconds(0.22f);
    }
}
