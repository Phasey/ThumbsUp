using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SweeperScript : MonoBehaviour
{
    public Rigidbody SweeperBody;
    public XboxController Controller;

    public float movementSpeed = 10f;
    public float maxSpeed = 10f;

	private float prevRotateX;
	private float prevRotateZ;

    // Use this for initialization
    void Awake()
    {
        SweeperBody = GetComponent<Rigidbody>();

		prevRotateX = 0f;
		prevRotateZ = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSweeper();
        RotateSweeper();
    }

    private void MoveSweeper()
    {
        float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
        float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

        Vector3 movement = new Vector3(axisX, 0, axisZ);
        SweeperBody.MovePosition(SweeperBody.position + movement * movementSpeed * Time.deltaTime);
        SweeperBody.velocity = Vector3.zero;
    }

    private void RotateSweeper()
    {
        float rotateAxisX = XCI.GetAxisRaw(XboxAxis.RightStickX, Controller);
        float rotateAxisZ = XCI.GetAxisRaw(XboxAxis.RightStickY, Controller);


        if (rotateAxisX == 0f)
			rotateAxisX = prevRotateX;
		else
			prevRotateX = rotateAxisX;

		if (rotateAxisZ == 0f)
			rotateAxisZ = prevRotateZ;
		else
			prevRotateZ = rotateAxisZ;

        if (rotateAxisX != 0 || rotateAxisZ != 0)
        {
            Vector3 directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);
            transform.rotation = Quaternion.LookRotation(directionVector);
        }
        SweeperBody.angularVelocity = Vector3.zero;
    }
}
