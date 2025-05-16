using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonViewCamera : MonoBehaviour
{
    public Transform _camera;

    private float xRotate, yRotate, xRotateMove, yRotateMove;
    private CharacterController _character;

    public float rotateSpeed = 500.0f;
    public float moveSpeed = 1f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _character = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        //player move
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);
        _character.Move(this.transform.TransformDirection(move) * Time.deltaTime * moveSpeed);

        //this.transform.Translate(_camera.forward * Time.deltaTime * moveZ);
        //this.transform.Translate(_camera.right * Time.deltaTime * moveX);

        //if(Input.GetKey(KeyCode.Q))
        //{
        //    this.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        //}
        //else if(Input.GetKey(KeyCode.E))
        //{
        //    this.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        //}

        //camera Rotate
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        yRotate = this.transform.eulerAngles.y + yRotateMove;
        //xRotate = transform.eulerAngles.x + xRotateMove; 
        xRotate = xRotate + xRotateMove;

        xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

        //this.transform.eulerAngles = new Vector3(0, yRotate, 0);
        //_camera.transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        Quaternion camQuat = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
        _camera.transform.rotation
            = camQuat;

        Quaternion pQuat = Quaternion.Euler(new Vector3(0, yRotate, 0));
        this.transform.rotation
            = pQuat;


    }
}
