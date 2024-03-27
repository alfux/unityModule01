using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
	private GameObject	bluePlate;
	private GameObject	yellowPlate;
	private GameObject	redPlate;
	private float		t = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.bluePlate = this.transform.GetChild(0).gameObject;
        this.yellowPlate = this.transform.GetChild(0).gameObject;
        this.redPlate = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
