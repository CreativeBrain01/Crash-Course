using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopHandler : MonoBehaviour
{
    // Unlock Code
    // 0 = locked, 1 = unlocked, 2 = unlocked and selected

    /// <summary>
    /// Be sure to place them in this list as they appear in the shop menu(top to bottom)
    /// </summary>
    [SerializeField]
    Toggle[] toggles;

    public Toggle[] Toggles { get { return toggles; } }

    [SerializeField]
    ToggleGroup toggleGroup;

    [SerializeField]
    TMP_Text vehicle_desc;

    [SerializeField]
    TMP_Text money_Txt;

    [SerializeField]
    Button confirm_btn;

    public VehicleHandler.eVehicle currentVehicle;

    private void Start()
    {
        currentVehicle = (VehicleHandler.eVehicle)PlayerPrefs.GetInt("vehicle");

        List<VehicleData> vd = new List<VehicleData>();

        foreach (Toggle toggle in toggles)
        {
            vd.Add(toggle.GetComponent<VehicleData>());
        }

        string unlock = PlayerPrefs.GetInt("Unlocks").ToString();
        
        string Unlocks = string.IsNullOrEmpty(unlock) ?
            "2000000" : unlock;

        if (Unlocks == "2000000") PlayerPrefs.SetInt("Unlocks", 2000000);

        for (int i = 0; i < Unlocks.Length; i++)
        {
            switch (Unlocks[i])
            {
                case '0':
                    vd[i].isPurchased = false;
                    toggles[i].isOn = false;
                    break;
                    
                case '1':
                    vd[i].isPurchased = true;
                    toggles[i].isOn = false;
                    break;

                case '2':
                    vd[i].isPurchased = true;
                    toggles[i].isOn = true;
                    break;

                default:
                    break;
            }
        }
    }

    void Update()
    {
        VehicleData vd = toggleGroup.GetFirstActiveToggle().GetComponent<VehicleData>();

        vehicle_desc.text = vd.Description;

        if (vd.isPurchased)
        {
            if (currentVehicle == vd.Vehicle)
            {
                confirm_btn.GetComponentInChildren<Text>().text = "Equipped";
            } else
            {
                confirm_btn.GetComponentInChildren<Text>().text = "Select";
            }
            
        } else
        {
            confirm_btn.GetComponentInChildren<Text>().text = "$ " + vd.Cost;
        }
    }

    public void OnSelect()
    {
        #region Input Processing
        VehicleData vd = toggleGroup.GetFirstActiveToggle().GetComponent<VehicleData>();

        if (vd.isPurchased)
        {
            currentVehicle = vd.Vehicle;
            
        } else
        {
            if (PlayerPrefs.GetInt("HubCaps") >= vd.Cost)
            {
                PlayerPrefs.SetInt("HubCaps", PlayerPrefs.GetInt("HubCaps") - vd.Cost);
                vd.isPurchased = true;
                currentVehicle = vd.Vehicle;
            }
        }
        #endregion

        #region Data Processing/Saving
        PlayerPrefs.SetInt("vehicle", (int)currentVehicle);

        char[] value = { '2', '0', '0', '0', '0', '0', '0' };

        for (int i = 0; i < toggles.Length; i++)
        {
            VehicleData vd2 = toggles[i].GetComponent<VehicleData>();
            int val = 0;
            if (vd2.isPurchased)
            {
                val++;
                if (vd2.Vehicle == currentVehicle) val++;
            }
            string l = val.ToString();

            value[i] = l[0];
        }

        string vehicleCode = "";
        foreach (char c in value)
        {
            vehicleCode += c;
        }

        PlayerPrefs.SetInt("Unlocks", int.Parse(vehicleCode));

        PlayerPrefs.Save();
        #endregion
    }
}