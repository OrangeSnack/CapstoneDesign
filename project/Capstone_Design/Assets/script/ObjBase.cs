﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase : MonoBehaviour
{
    //오브젝트 설정
    public GameObject obj;
    public float degreeSize = 90f;
    public float rotateSpeed = 1f;
    public float moveLength = 5f;
    public float moveSpeed = 1f;

    private Vector3 rotation;
    private Transform objTransform;
    private Vector3 previousPos;
    private float degree = 0f;
    private bool isRotate = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        if(obj == null)
        {
            Debug.Log("오브젝트가 설정되지 않았습니다!");
        }
        else
        {
            rotation = Vector3.zero;
            if(!Mathf.Approximately(obj.transform.localRotation.y % 90,0))
            {
                obj.transform.localRotation = Quaternion.identity;
                Debug.Log(obj.name + "오브젝트의 회전각이 맞지않아 0으로 초기화함");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //오브젝트 회전
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, Quaternion.Euler(rotation), rotateSpeed*Time.deltaTime);
        //obj.transform.localEulerAngles = Vector3.Lerp(obj.transform.localEulerAngles, rotation, rotatespeed*Time.deltaTime);

        //오브젝트 이동
        previousPos = obj.transform.position;
        obj.transform.position = Vector3.Lerp(obj.transform.position, objTransform.position, moveSpeed);
    }

    //돌아가는 중인지 확인
    /// <summary>
    /// 다 돌아갔으면 false, 돌아가는 중이면 true 출력
    /// </summary>
    void IsRotate()
    {
        if (Mathf.Approximately(Mathf.Round(obj.transform.localEulerAngles.y), degree) || Mathf.Approximately(Mathf.Round(obj.transform.localEulerAngles.y), 360f))
            isRotate = false;
        else
            isRotate = true;

        if (Mathf.Approximately(Mathf.Round(obj.transform.localEulerAngles.y), 360f) || Mathf.Approximately(Mathf.Round(obj.transform.localEulerAngles.y), 0f))
            obj.transform.localRotation = Quaternion.identity;
    }

    //오브젝트 이동중인지 확인
    /// <summary>
    /// 다 움직이면 false, 움직이는 중이면 true 출력
    /// </summary>
    void IsMoving()
    {
        if (previousPos != obj.transform.position)
            isMoving = true;
        else
            isMoving = false;
    }

    //오브젝트 이동
    /// <summary>
    /// 오브젝트를 Vec3만큼 이동
    /// </summary>
    /// <param name="tf"></param>
    void ObjMove(Vector3 addPos)
    {
        IsRotate();
        IsMoving();

        if(!isRotate && !isMoving)
        {
           objTransform.position = obj.transform.position + addPos * moveLength;
        }
    }

    //오브젝트 회전
    /// <summary>
    /// 로컬Y좌표로 degree 만큼 회전
    /// </summary>
    /// <param name="degree"></param>
    void ObjRotation()
    {
        IsRotate();
        IsMoving();

        if (!isRotate && !isMoving)
        {
            degree += degreeSize;
            if (degree >= 360)
            {
                degree = 0;
                //obj.transform.localRotation = Quaternion.identity;
            }
            rotation = new Vector3(obj.transform.localEulerAngles.x, obj.transform.localEulerAngles.y + degreeSize, obj.transform.localEulerAngles.z);
        }
        else
            Debug.Log("물체가 다 돌아갈때까지 기다리세요!");
    }
}
