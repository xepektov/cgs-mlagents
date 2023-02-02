using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class new_agent_script : Agent
{
    public Transform target;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.RandomRange(-70, 60), 0, Random.RandomRange(-100, 100));
        target.localPosition = new Vector3(Random.RandomRange(-70, 60), 0, Random.RandomRange(-100, 100));
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("working");
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float speed = 20;

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "goal")
        {
            SetReward(1f);
            EndEpisode();
        }
        else if(other.gameObject.tag == "wall")
        {
            SetReward(1f);
            EndEpisode();
        }
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("here");
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
