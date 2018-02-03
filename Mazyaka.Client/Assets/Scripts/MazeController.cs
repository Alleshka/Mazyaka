using UnityEngine;
using System.Collections;
using UnityEditor;

public class MazeController : MonoBehaviour {

    [SerializeField]
    private float speed = 1f; //how fast the object should rotate

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float rotX = Input.GetAxis("Mouse X") * speed;
            float rotY = Input.GetAxis("Mouse Y") * speed;
            //transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
            //transform.Rotate((Input.GetAxis("Mouse Y") * speed * Time.deltaTime), (Input.GetAxis("Mouse X") * speed * Time.deltaTime), 0, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);
             transform.Rotate(Vector3.down, rotX, Space.World);
             
        }
    }
}
