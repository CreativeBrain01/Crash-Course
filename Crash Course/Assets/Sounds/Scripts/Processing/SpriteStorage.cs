using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteStorage
{
    public static Sprite scooter = Resources.Load("Vehicles/Scooter", typeof(Sprite)) as Sprite;
    public static Sprite quad = Resources.Load("Vehicles/Off Roader", typeof(Sprite)) as Sprite;
    public static Sprite motorcycle = Resources.Load("Vehicles/Motorcycle", typeof(Sprite)) as Sprite;
    public static Sprite taxi = Resources.Load("Vehicles/Taxi", typeof(Sprite)) as Sprite;
    public static Sprite bus = Resources.Load("Vehicles/Bus", typeof(Sprite)) as Sprite;
    public static Sprite truck = Resources.Load("Vehicles/Truck", typeof(Sprite)) as Sprite;
    public static Sprite camper = Resources.Load("Vehicles/Camper", typeof(Sprite)) as Sprite;
    public static Sprite trailer = Resources.Load("Vehicles/Trailer", typeof(Sprite)) as Sprite;
    public static Sprite camperWithTrailer = Resources.Load("Vehicles/CamperWithTrailer", typeof(Sprite)) as Sprite;
}
