using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask obstacleMask;
    [SerializeField] public List<Node> neighbours;
    public Vector3 neighbourFindRange;

    Material mat;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void GetNeighbours()
    {   
        neighbours = new List<Node>();
        Collider[] neigh = Physics.OverlapBox(transform.position, neighbourFindRange / 2, Quaternion.identity, mask);

        Debug.Log("OVERLAP: " + neigh.Length);
        foreach (Collider neighbourNode in neigh)
        {
            Vector3 aux = neighbourNode.transform.position - transform.position;
            if (GetComponent<Collider>() != neighbourNode && !Physics.Raycast(transform.position, aux, aux.magnitude, obstacleMask))
            {
                Debug.Log("SHOULD ADD: " + neighbourNode);
                neighbours.Add(neighbourNode.GetComponent<Node>());
            }
        }
    }
    public void DeleteIfOverlaped()
    {
        Collider[] neighbours = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, mask);

        Collider nodeCollider = GetComponent<Collider>();

        if (neighbours.Length > 1) DestroyImmediate(gameObject);
    }
    public bool showGizmos;

    private void OnDrawGizmosSelected()
    {
        if(showGizmos)
            Gizmos.DrawWireCube(transform.position, neighbourFindRange);
    }
}
