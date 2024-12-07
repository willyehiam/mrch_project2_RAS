using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAndFlyingCircleObjects : MonoBehaviour
{
    public List<GameObject> objectsToMove; // 要移动的模型列表
    public Transform targetPosition;      // 圆心位置
    public float floatHeight = 2.0f;      // 漂浮的高度
    public float floatSpeed = 1.0f;       // 漂浮的速度
    public float circleRadius = 3.0f;     // 圆的半径
    public float flySpeed = 2.0f;         // 飞行的速度
    public float rotationSpeed = 30.0f;  // 顺时针旋转的速度（度/秒）
    private bool isTriggered = false;    // 是否触发
    private List<Vector3> initialPositions = new List<Vector3>(); // 模型初始位置
    private List<Vector3> circlePositions = new List<Vector3>();  // 圆形布局位置

    void Start()
    {
        // 存储每个对象的初始位置
        foreach (GameObject obj in objectsToMove)
        {
            initialPositions.Add(obj.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered) // 确保只触发一次
        {
            isTriggered = true;
            CalculateCirclePositions();
            StartCoroutine(MoveObjects());
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            // 让模型围绕目标位置顺时针旋转
            for (int i = 0; i < objectsToMove.Count; i++)
            {
                objectsToMove[i].transform.RotateAround(targetPosition.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void CalculateCirclePositions()
    {
        // 计算每个模型在圆形上的最终位置
        for (int i = 0; i < objectsToMove.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / objectsToMove.Count; // 每个模型的角度
            float x = targetPosition.position.x + Mathf.Cos(angle) * circleRadius;
            float z = targetPosition.position.z + Mathf.Sin(angle) * circleRadius;
            float y = targetPosition.position.y + floatHeight; // 圆的高度
            circlePositions.Add(new Vector3(x, y, z));
        }
    }

    IEnumerator MoveObjects()
    {
        // 第一阶段：漂浮
        float elapsedTime = 0;
        while (elapsedTime < 2.0f) // 漂浮持续2秒
        {
            for (int i = 0; i < objectsToMove.Count; i++)
            {
                // 模型上下漂浮
                Vector3 floatPosition = initialPositions[i] + Vector3.up * (Mathf.Sin(Time.time * floatSpeed) * floatHeight);
                objectsToMove[i].transform.position = Vector3.Lerp(objectsToMove[i].transform.position, floatPosition, Time.deltaTime * floatSpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 第二阶段：飞向目标位置的圆圈
        elapsedTime = 0;
        while (elapsedTime < 3.0f) // 飞行持续3秒
        {
            for (int i = 0; i < objectsToMove.Count; i++)
            {
                // 平滑移动到圆圈中的目标位置
                objectsToMove[i].transform.position = Vector3.Lerp(objectsToMove[i].transform.position, circlePositions[i], Time.deltaTime * flySpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
