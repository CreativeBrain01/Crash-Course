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

    public static int VehicleToDurability(eVehicle vehicle)
    {
        switch (vehicle)
        {
            case eVehicle.Scooter:
                return 1;

            case eVehicle.Quad:
                return 2;

            case eVehicle.Motorcycle:
                return 1;

            case eVehicle.Taxi:
                return 2;

            case eVehicle.Bus:
                return 3;

            case eVehicle.Truck:
                return 2;

            case eVehicle.Camper:
                return 4;

            default:
                return 0;
        }
    }

    public static int VehicleToSpeed(eVehicle vehicle)
    {
        switch (vehicle)
        {
            case eVehicle.Scooter:
                return 1;

            case eVehicle.Quad:
                return 1;

            case eVehicle.Motorcycle:
                return 4;

            case eVehicle.Taxi:
                return 2;

            case eVehicle.Bus:
                return 2;

            case eVehicle.Truck:
                return 3;

            case eVehicle.Camper:
                return 2;

            default:
                return 0;
        }
    }

    float speed = 1;
    public float Speed { get { return speed; } }
    int durability = 1;
    public float Durability { get { return durability; } }


    void Start()
    {
        int index = PlayerPrefs.GetInt("vehicle");
        vehicle = (eVehicle)index;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        speed = VehicleToSpeed(vehicle);
        durability = VehicleToDurability(vehicle);

        switch (vehicle)
        {
            case eVehicle.Scooter:
                sr.sprite = SpriteStorage.scooter;
                break;
            case eVehicle.Quad:
                sr.sprite = SpriteStorage.quad;
                break;
            case eVehicle.Motorcycle:
                sr.sprite = SpriteStorage.motorcycle;
                break;
            case eVehicle.Taxi:
                sr.sprite = SpriteStorage.taxi;
                break;
            case eVehicle.Bus:
                sr.sprite = SpriteStorage.bus;
                break;
            case eVehicle.Truck:
                sr.sprite = SpriteStorage.truck;
                break;
            case eVehicle.Camper:
                sr.sprite = SpriteStorage.camper;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (durability <= 0)
        {
            GameController.Instance.gameOver = true;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("vehicle", (int)vehicle);
        PlayerPrefs.Save();
    }

    public void TakeDamage()
    {
        float chance = (durability * 20) + 10;

        int value = Random.Range(0, 100);
        if (value >= chance)
        {
            durability--;
        }
    }
}
