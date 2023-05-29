using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

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


    public override void OnActionReceived(ActionBuffers actions)
    {
        if (_playerController.isCollided) 
        {
            AddReward(-10f);
            EndEpisode();
        }
        AddReward(0.01f);
        _playerController.desiredLane += (actions.DiscreteActions[0] - 1);
        _playerController.MoveLane();
    }
}
