using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnemyFighting;

public class Controller : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Transform player;

    public PlayerAvoidance position;
    
    private FightingController controller;
    public CameraController camera;

    public float moveSpeed;

    public AnimationCurve curve;

    public Vector3 reqPos;
    [HideInInspector]
    public Vector3 originPos;

    private Coroutine moveCoroutine;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if(eventData.delta.x < 0)
            {
                if (controller.phase == FightPhase.Defend)
                {
                    Debug.Log("Left");
                    reqPos = originPos - transform.right;
                    Move(originPos - transform.right);
                    position = PlayerAvoidance.StepBack;
                    camera.ZoomTo(7);
                }
            }
            else
            {
                if (controller.phase == FightPhase.Attack)
                {
                    controller.Attack();
                    //reqPos = originPos + transform.right * 2;
                    Move(originPos + transform.right * 2, 3);
                }
            }
        }
        else
        {
            if (controller.phase == FightPhase.Defend)
            {
                if (eventData.delta.y > 0)
                {
                    Debug.Log("Up");
                    //reqPos = originPos + transform.up;
                    Move(originPos + transform.up);
                    position = PlayerAvoidance.Jump;
                    camera.ZoomTo(7);
                }
                else
                {
                    Debug.Log("Down");
                    //reqPos = originPos - transform.up;
                    Move(originPos - transform.up);
                    position = PlayerAvoidance.GoDown;
                    camera.ZoomTo(7);
                }
            }
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //None
    }

    private void Awake()
    {
        controller = GameObject.FindObjectOfType<FightingController>();
        originPos = player.position;
    }

    private void Update()
    {
        //player.position = Vector3.Lerp(player.position, reqPos, Time.deltaTime * moveSpeed);
    }

    public void Move(Vector3 pos)
    {
        Move(pos, moveSpeed);
    }

    public void Move(Vector3 pos, float freezeTime)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveProcess(pos, freezeTime));
    }

    public IEnumerator MoveProcess(Vector3 pos, float speed)
    {
        for (float i = 0; i < curve.length; i += Time.deltaTime * speed)
        {
            Debug.Log(Vector3.Lerp(originPos, pos, curve.Evaluate(i)));
            player.position = Vector3.Lerp(originPos, pos, curve.Evaluate(i));
            yield return new WaitForEndOfFrame();
        }
        position = PlayerAvoidance.none;
    }
    
}