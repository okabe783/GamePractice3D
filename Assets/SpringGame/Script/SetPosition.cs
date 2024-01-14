using UnityEngine;

public class SetPosition : MonoBehaviour
{
    //初期位置
    private Vector3 startPosition;

    //目的地
    private Vector3 destination;

    void Start()
    {
        //初期位置を設定
        startPosition = transform.position;
        SetDestination(transform.position);
    }

    //目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }
}
