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
        Trailer
    }
    eVehicle vehicle = eVehicle.Scooter;

    [SerializeField]
    Sprite scooterSprite;
    [SerializeField]
    Sprite quadSprite;
    [SerializeField]
    Sprite motorcycleSprite;
    [SerializeField]
    Sprite taxiSprite;
    [SerializeField]
    Sprite busSprite;
    [SerializeField]
    Sprite truckSprite;
    [SerializeField]
    Sprite trailerSprite;
    [SerializeField]
    Sprite trailerSprite2;

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
                sr.sprite = scooterSprite;
                break;
            case eVehicle.Quad:
                speed = 1;
                durability = 2;
                sr.sprite = quadSprite;
                break;
            case eVehicle.Motorcycle:
                speed = 4;
                durability = 1;
                sr.sprite = motorcycleSprite;
                break;
            case eVehicle.Taxi:
                speed = 2;
                durability = 2;
                sr.sprite = taxiSprite;
                break;
            case eVehicle.Bus:
                speed = 2;
                durability = 3;
                sr.sprite = busSprite;
                break;
            case eVehicle.Truck:
                speed = 3;
                durability = 2;
                sr.sprite = truckSprite;
                break;
            case eVehicle.Trailer:
                speed = 2;
                durability = 4;
                sr.sprite = trailerSprite;
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
