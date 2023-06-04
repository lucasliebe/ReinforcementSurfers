using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;

public class PlayerAgent : Agent
{
    private GroundController _groundController;

    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _groundController = _playerController.transform.parent.Find("Ground").GetComponent<GroundController>();
    }

    public override void OnEpisodeBegin()
    {
        _groundController.Cleanup();
        _groundController.Setup();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        for (int i = 0; i < (int)_groundController.lanes; i++)
        {
            sensor.AddObservation(_playerController.GetCurrentLane() == i ? 1.0f : 0.0f);
        }
        base.CollectObservations(sensor);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        Tuple<bool, int> state = _playerController.GetState();
        AddReward(state.Item2 * 1f);
        if (state.Item1) 
        {   
            AddReward(-10f);
            EndEpisode();
        }
        AddReward(0.01f);
        _playerController.SetDesiredLane(_playerController.GetCurrentLane() + (actions.DiscreteActions[0] -1));
        _playerController.MoveLane();
    }
}
