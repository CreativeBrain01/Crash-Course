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

    public static Sprite VehicleToSprite(eVehicle vehicle)
    {
        switch (vehicle)
        {
            case eVehicle.Scooter:
                return SpriteStorage.scooter;

            case eVehicle.Quad:
                return SpriteStorage.quad;

            case eVehicle.Motorcycle:
                return SpriteStorage.motorcycle;

            case eVehicle.Taxi:
                return SpriteStorage.taxi;

            case eVehicle.Bus:
                return SpriteStorage.bus;

            case eVehicle.Truck:
                return SpriteStorage.truck;

            case eVehicle.Camper:
                return SpriteStorage.camper;

            default:
                return null;
        }
    }

    float speed = 1;
    public float Speed { get { return speed; } }
    int durability = 1;
    public float Durability { get { return durability; } }
    public int lives = 1;

    SpriteRenderer sr;
    void Start()
    {
        int index = PlayerPrefs.GetInt("vehicle");
        vehicle = (eVehicle)index;

        if (vehicle == eVehicle.Camper)
        {
            lives = 2;
        }

        sr = GetComponent<SpriteRenderer>();

        speed = VehicleToSpeed(vehicle);
        durability = VehicleToDurability(vehicle);
        sr.sprite = VehicleToSprite(vehicle);

        float r = PlayerPrefs.GetFloat("color-Red", 1);
        float g = PlayerPrefs.GetFloat("color-Green", 1);
        float b =PlayerPrefs.GetFloat("color-Blue", 1);

        sr.color = new Color(r, g, b);
    }

    private void Update()
    {
        if (lives >= 2)
        {
            sr.sprite = SpriteStorage.camperWithTrailer;
        } else
        {
            if ((eVehicle)PlayerPrefs.GetInt("vehicle") == eVehicle.Camper)
            {
                sr.sprite = SpriteStorage.camper;
            }
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

            if (durability <= 0)
            {
                lives--;

                if (lives <= 0)
                {
                    GameController.Instance.gameOver = true;
                }
            }
        }
    }
}
