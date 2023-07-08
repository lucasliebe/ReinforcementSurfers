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

    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
    //     discreteActions[0] = 1;
    //     discreteActions[1] = 2;
    //     if (Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         discreteActions[0] = 0;
    //     }
    //     if (Input.GetKey(KeyCode.RightArrow))
    //     {
    //         discreteActions[0] = 2;
    //     }
    //     if (Input.GetKey(KeyCode.UpArrow))
    //     {
    //         discreteActions[1] = 0;
    //     }
    //     if (Input.GetKey(KeyCode.DownArrow))
    //     {
    //         discreteActions[1] = 1;
    //     }
    // }

    public override void CollectObservations(VectorSensor sensor)
    {
        for (int i = 0; i < (int)_groundController.lanes; i++)
        {
            sensor.AddObservation(_playerController.GetCurrentLane() == i ? 1.0f : 0.0f);
        }
        sensor.AddObservation(_playerController.GetCurrentPosition().y);
        base.CollectObservations(sensor);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        Tuple<bool, int, int> state = _playerController.GetState();
        AddReward(state.Item2 * 0.4f * state.Item3);
        if (state.Item1) {
            AddReward(-1f * state.Item3);
            EndEpisode();
            return;
        }
        if (actions.DiscreteActions[0] != 1) {
            AddReward(0f);
            _playerController.SetDesiredLane(_playerController.GetCurrentLane() + (actions.DiscreteActions[0] - 1));
            _playerController.MoveLane();
        } else {
            AddReward(0.001f * (float) Math.Pow(state.Item3, 2));
        }
        if (actions.DiscreteActions[1] == 0)
        {
            _playerController.TriggerIsJumping();
        }
        else if (actions.DiscreteActions[1] == 1)
        {
            _playerController.TriggerIsSliding();
        }

        // if (actions.DiscreteActions[2] == 0)
        // {
        //     _playerController.TriggerMultiplier();
        // } else if (actions.DiscreteActions[2] == 1)
        // {
        //     _playerController.TriggerShield();
        // }
    }
}
