using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRUN : PlayerFSMState {

    public Transform marker;
    public float moveSpeed = 3.0f;

    private void Start()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, marker.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, marker.position) < 0.01f)
        {
            GetComponent<FSMManager>().StateModify(PlayerState.IDLE);
        }
    }
}
