using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHandler : MonoBehaviour
{
    public enum eVehicle
    {
        Scooter,
        Quad,
        Motorcycle,
        Taxi,
        Bus,
        Truck,
        Camper
    }
    eVehicle vehicle = eVehicle.Scooter;

    float speed = 1;
    public float Speed { get { return speed; } }
    int durability = 1;

    void Start()
    {
        int index = PlayerPrefs.GetInt("vehicle");
        vehicle = (eVehicle)index;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        switch (vehicle)
        {
            case eVehicle.Scooter:
                speed = 1;
                durability = 1;
                sr.sprite = SpriteStorage.scooter;
                break;
            case eVehicle.Quad:
                speed = 1;
                durability = 2;
                sr.sprite = SpriteStorage.quad;
                break;
            case eVehicle.Motorcycle:
                speed = 4;
                durability = 1;
                sr.sprite = SpriteStorage.motorcycle;
                break;
            case eVehicle.Taxi:
                speed = 2;
                durability = 2;
                sr.sprite = SpriteStorage.taxi;
                break;
            case eVehicle.Bus:
                speed = 2;
                durability = 3;
                sr.sprite = SpriteStorage.bus;
                break;
            case eVehicle.Truck:
                speed = 3;
                durability = 2;
                sr.sprite = SpriteStorage.truck;
                break;
            case eVehicle.Camper:
                speed = 2;
                durability = 4;
                sr.sprite = SpriteStorage.camper;
                break;
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("vehicle", (int)vehicle);
        PlayerPrefs.Save();
    }
}
