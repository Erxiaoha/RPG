using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHistoryTracker : MonoBehaviour
{
    private readonly HashSet<LocationSO> locationVisited = new HashSet<LocationSO>();

    public void RecordLocation(LocationSO locationSO)
    {
        locationVisited.Add(locationSO);
    }

    public bool HasVisted(LocationSO locationSO)
    {
        return locationVisited.Contains(locationSO);
    }
}
