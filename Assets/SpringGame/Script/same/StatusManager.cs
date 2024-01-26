using UnityEngine;

public abstract class StatusManager : MonoBehaviour
{
    protected enum State
    {
        Normal,
        Attack
    }

    public bool IsMove => State.Normal == state;
    public bool IsAttack => State.Normal == state;
    protected Animator animator;
    protected State state = State.Normal;

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }
}