using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class VehicleData : MonoBehaviour
{
    [SerializeField]
    VehicleHandler.eVehicle vehicle = VehicleHandler.eVehicle.Scooter;
    public VehicleHandler.eVehicle Vehicle { get { return vehicle; } }

    [SerializeField]
    string description = "This is a generic description.";
    public string Description { get { return description; } }

    [SerializeField]
    int cost = 0;
    public int Cost { get { return cost; } }

    [SerializeField]
    public bool isPurchased = false;
}
