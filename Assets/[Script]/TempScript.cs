using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var y = Input.GetAxisRaw("Vertical");
        var x = Input.GetAxisRaw("Horizontal");

        gameObject.transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
}
