using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Color lineColor;

    private List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();       

        for(int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        if(nodes.Count > 1)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 currentNodePos = nodes[i].position;
                Vector3 previousNodePos = Vector3.zero;

                if (i > 0)
                {
                    previousNodePos = nodes[i - 1].position;
                }
                else if (i == 0)
                {
                    previousNodePos = nodes[nodes.Count - 1].position;
                }

                Gizmos.DrawLine(previousNodePos, currentNodePos);
                Gizmos.DrawWireSphere(currentNodePos, 0.3f);
            }
        }
       
    }
}
