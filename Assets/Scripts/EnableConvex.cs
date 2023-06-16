using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableConvex : MonoBehaviour
{


    private void Awake()
    {
        CollectComponentsInChildren(transform);
    }

    void CollectComponentsInChildren(Transform parent)
    {
        // Get all the components attached to the parent transform
        MeshCollider[] components = parent.GetComponents<MeshCollider>();

        // Iterate through each component
        foreach (MeshCollider component in components)
        {
            component.convex = true;
        }

        // Iterate through each child of the parent transform
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            // Recursively call the function to process the child's components and its children's components
            CollectComponentsInChildren(child);
        }
    }
}
