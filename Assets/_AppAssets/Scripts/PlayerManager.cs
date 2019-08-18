using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Camera Setting
    [Tooltip("speed of Camera rotation")]
    [Range(0, 10)]
    public float RotSpeed;

    [Tooltip("To allow lock (limit) camera rotaion around X axis")]
    public bool LockXRot;
    public float XRotLimit;

    [Tooltip("To allow lock (limit) camera rotaion around Y axis")]
    public bool LockYRot;
    public float YRotLimit;

    private Vector2 camPos;
    #endregion

    #region Shoot Setting
    public GameObject BallPrefab;
    public float BallSpeed;
    public float FireRate;

    private float nextFire;
    #endregion

    private void Update()
    {
        RotateCamera();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBall();
        }
    }

    private void RotateCamera()
    {
        Vector3 mousePos = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        mousePos *= RotSpeed;

        camPos = (transform.localRotation * Quaternion.Euler(mousePos)).eulerAngles;

        // Up and Down limits
        if (LockXRot)
        {
            if (camPos.x >= XRotLimit && camPos.x <= XRotLimit * 2)
            {
                camPos.x = XRotLimit;
            }
            else
            {
                camPos.x = Mathf.Clamp((camPos.x > XRotLimit) ? camPos.x - 360 : camPos.x, -XRotLimit, XRotLimit);
            }
        }

        // Right and Left limits
        if (LockYRot)
        {
            if (camPos.y >= YRotLimit && camPos.y <= YRotLimit * 2)
            {
                camPos.y = YRotLimit;
            }
            else
            {
                camPos.y = Mathf.Clamp((camPos.y > YRotLimit) ? camPos.y - 360 : camPos.y, -YRotLimit, YRotLimit);
            }
        }

        transform.eulerAngles = camPos;
    }

    private void ShootBall()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            GameObject instance = Instantiate(BallPrefab);
            instance.transform.position = transform.position;
            instance.GetComponent<Rigidbody>().velocity = transform.rotation * Vector3.forward * BallSpeed * Time.deltaTime;
        }
    }
}
