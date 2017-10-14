using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarAI : MonoBehaviour {

    [SerializeField]
    private Transform path;

    private List<Transform> nodes;
    private int currentNode = 0;

    private CarController m_Car;
    [SerializeField]
    private float maxAccel = 1.0f;

    // Use this for initialization
    void Start () {
        m_Car = GetComponent<CarController>();

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        m_Car.Move(relativeVector.x / relativeVector.magnitude,
                    maxAccel,
                    maxAccel, 
                    0);
        CheckWaypointDistance();
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
          
        }
    }
}
