using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Agent : Unity.MLAgents.Agent
{
    public Axis moveAlong = Axis.X;
    public float movementRange = 10.0f;
    public float movementSpeed = 1.0f;
    public TextMeshPro tmp;
    private Rigidbody rb;
    private Vector3 agentStartPosition;
    public BallSpawner spawner;

    private bool canJump = true;

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = agentStartPosition;
        
        spawner.ClearBalls();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agentStartPosition = this.transform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        tmp.text = GetCumulativeReward().ToString();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actionsOutDiscrete = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.LeftArrow))
            actionsOutDiscrete[0] = 1;
        
        if (Input.GetKey(KeyCode.RightArrow))
            actionsOutDiscrete[0] = 2;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionsDiscrete = actions.DiscreteActions;
        if (actionsDiscrete[0] == 1)
        {
            switch (moveAlong)
            {
                case Axis.X:
                    transform.position -= movementSpeed * Time.deltaTime;
            }
        }
        else if (actionsDiscrete[0] == 2)
        {
            
        }
    }
}