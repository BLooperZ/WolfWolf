using System.Collections.Generic;
using UnityEngine;

public class GInventory {
    // Store our items in a HashSet to ensure uniqueness
    public HashSet<GameObject> items = new HashSet<GameObject>();

    // Method to add items to our HashSet
    public void AddItem(GameObject i) {
        items.Add(i); // HashSet automatically handles duplicates
    }

    public bool HasItem(GameObject i) {
        return items.Contains(i);
    }

    // Method to search for a particular item
    public GameObject FindItemWithTag(string tag) {
        // Iterate through all the items
        foreach (GameObject i in items) {
            // Found a match
            if (i.tag == tag) {
                return i;
            }
        }
        // Nothing found
        return null;
    }

    // Remove an item from our HashSet
    public void RemoveItem(GameObject i) {
        items.Remove(i); // HashSet handles removal directly
    }
}
