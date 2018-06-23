using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CubePlacer : MonoBehaviour
{
    [SerializeField] private List<Material> Materials;
    private List<string> materials_Names = new List<string>();

    private static readonly string path_Textures = "Textures";

    private float width;
    private float length;

    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();

        width = PlayerPrefs.GetFloat("Width");
        length = PlayerPrefs.GetFloat("Length");

        Materials = Resources.LoadAll(path_Textures, typeof(Material)).Cast<Material>().ToList();
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
            ChangeTexture();
        }
    }

    private void ChangeTexture()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            var material = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material;
            var index = -1;
            Vector2 scale;

            for (int i = 0; i < materials_Names.Count; i++)
            {
                var materialName = materials_Names[i] + " (Instance)";
                if (material.name == materialName)
                {
                    index = i;
                }

            }

            if (index == -1)
            {
                Debug.LogWarning("Fail to find texture");
            }


            if (hitInfo.transform.gameObject.GetComponent<BoxCollider>())
            {
                scale = new Vector2(hitInfo.transform.localScale.x, hitInfo.transform.localScale.z);

            }
            else
            {
                if (!(hitInfo.transform.localEulerAngles.x == 270))
                {
                    scale = new Vector2(width, length);
                }
                else if (hitInfo.transform.localEulerAngles.y == 0 || hitInfo.transform.localEulerAngles.y == 180)
                {
                    scale = new Vector2(width, 1);
                }
                else
                {
                    scale = new Vector2(length, 1);
                }
            }


            if (index == materials_Names.Count - 1)
            {
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = Materials[0];
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.mainTextureScale = scale;

            }
            else
            {
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = Materials[index + 1];
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.mainTextureScale = scale;

            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        var temp = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

        temp.position = finalPosition;

        temp.gameObject.GetComponent<MeshRenderer>().material = Materials[0];

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }


}