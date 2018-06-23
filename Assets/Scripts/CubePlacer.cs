using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePlacer : MonoBehaviour
{
    public List<Material> Materials;
    private List<string> materials_Names = new List<string>();

    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        foreach (var material in Materials)
        {
            materials_Names.Add(material.name);
        }
    }

    private void Update()
    {
        if (!grid)
        {
            grid = FindObjectOfType<Grid>();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (!hitInfo.transform.gameObject.GetComponent<BoxCollider>())
                {
                    PlaceCubeNear(hitInfo.point);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                var temp = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.name;
                var index  = -1;

               

                for (int i = 0; i < materials_Names.Count; i++)
                {
                    var temp2 = materials_Names[i] + " (Instance)";
                    if (temp == temp2)
                    {
                        index = i;

                    }

                    Debug.Log(temp + " == " + temp2);
                }

                index++;

                if (index == 0)
                {
                    Debug.LogWarning("Fail to find texture");
                }

                if ( index == materials_Names.Count)
                {
                    hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = Materials[0];
                }
                else
                {
                    hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = Materials[index];
                }
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }


}