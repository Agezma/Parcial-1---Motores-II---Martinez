using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodeGenerator : MonoBehaviour
{
    public List<Node> allNodes;
    public LayerMask nodeMask;
    public LayerMask obstacleMask;
    public Vector3 range;
    
    public Vector2Int size;
    Vector2Int lastSize;
    public Vector2Int separation;
    Vector2Int lastSeparation;
    public GameObject prefabNode;

    public bool showGizmos;

    private void Update()
    {   
        ChangeGizmos();

        if (lastSize != size || lastSeparation != separation)
        {
            CreateNodeMesh();
        }
        lastSeparation = separation;
        lastSize = size;
    }

    public void ChangeGizmos()
    {
        foreach (var item in allNodes)
        {
            item.showGizmos = showGizmos;
            item.neighbourFindRange = range;
        }
    }
   
    public void FindNeighboursInChildren()
    {
        foreach (var currNode in allNodes)
        {
            currNode.GetNeighbours();
        }
    }

    public void BorrarDuplicados()
    {
        int x = 0;

        foreach (var item in allNodes)
        {
            Collider[] nodes = Physics.OverlapBox(item.transform.position, item.transform.localScale, Quaternion.identity, item.mask);

            Collider nodeCollider = item.GetComponent<Collider>();

            if (nodes.Length > 1)
            {
                x++;
                DestroyImmediate(item.gameObject);
            }
            Debug.Log("Borrados: " + x);
        }
    }
    
    public void CreateNodeMesh()
    {
        allNodes = new List<Node>();

        int cant = transform.childCount;
        for (int i = 0; i < cant; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        Collider[] cols = new Collider[0];
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (Physics.OverlapSphere(transform.position + new Vector3(separation.x * i, 0, separation.y * j), 0.01f, obstacleMask).Length > 0)
                {
                    continue;
                }
        
                Node node = Instantiate(prefabNode).GetComponent<Node>();
                node.transform.position = transform.position + new Vector3(separation.x * i, 0, separation.y * j);
                node.transform.parent = transform;
                allNodes.Add(node);
            }
        }
    }

    public void SearchNewNodes()
    {
        int cant = allNodes.Count;
        allNodes = new List<Node>();

        var a = GetComponentsInChildren<Node>();
        for (int i = 0; i < a.Length; i++)
        {
            allNodes.Add(a[i]);
        }

        int final = allNodes.Count;

        Debug.Log("Cantidad inicial: " + cant + "\n Nueva cantidad: " + final);
    }

}