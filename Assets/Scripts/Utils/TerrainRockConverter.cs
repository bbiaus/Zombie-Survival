using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TerrainRockConverter : MonoBehaviour
{
    public Terrain terrain;
    public GameObject rockPrefab;
    public int treeIndex = 5;

    public void ConvertRocks()
    {
        if (terrain == null || rockPrefab == null)
        {
            Debug.LogWarning("Asigná el Terrain y el Prefab de la piedra.");
            return;
        }

        var treeInstances = terrain.terrainData.treeInstances;
        List<TreeInstance> newTrees = new List<TreeInstance>();

        foreach (var tree in treeInstances)
        {
            if (tree.prototypeIndex == treeIndex)
            {
                Vector3 worldPos = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;
                GameObject rock = (GameObject)PrefabUtility.InstantiatePrefab(rockPrefab);
                rock.transform.position = worldPos;
                
                float randomYRotation = Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(0f, randomYRotation, 0f);
                rock.transform.rotation = rotation;
                rock.transform.rotation = rotation;

                rock.AddComponent<MeshCollider>();
                GameObjectUtility.SetStaticEditorFlags(rock, StaticEditorFlags.NavigationStatic);

                rock.transform.SetParent(terrain.transform);

            }
            else
            {
                newTrees.Add(tree);
            }
        }

        terrain.terrainData.treeInstances = newTrees.ToArray();
        Debug.Log("Piedras convertidas con éxito.");
    }
}

