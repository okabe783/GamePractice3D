using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class NavMeshCollisionDetector : MonoBehaviour
{
    //OnTriggerStayイベント時に実行したい関数を登録する変数(引数にcolをとる)
    [SerializeField] private UnityEvent<Collider> onTriggerStayEvent = new UnityEvent<Collider>();

    //  OnTriggerExitイベント時に実行したい関数を登録する変数（引数にColliderを取る）
    [SerializeField] private UnityEvent<Collider> onTriggerExitEvent = new UnityEvent<Collider>();

    //IsTriggerがONで他のGameObjectがcol内にいるときに呼ばれ続ける
    private void OnTriggerStay(Collider other)
    {
        // InspectorのonTriggerStayEventで指定された処理を実行する
        onTriggerStayEvent.Invoke(other);
    }

    //IsTriggerがONで他のGameObjectがcolから出たときに呼ばれる
    private void OnTriggerExit(Collider other)
    {
        //InspectorのonTriggerExitEventで指定された処理を実行する
        onTriggerExitEvent.Invoke(other);
    }
}