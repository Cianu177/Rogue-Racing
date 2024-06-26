using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    // Definition List of Upgrades
    Upgrade[] _Upgrades = new Upgrade[]
    {
        // Acceleration 
       new Upgrade { Name = "Acceleration I", Description = "Decreases The Amount Of Time To Reach Your Top Speed By X%", Rarity = "Bronze", Increase = 10},
       new Upgrade { Name = "Acceleration II", Description = "Decreases The Amount Of Time To Reach Your Top Speed By X%", Rarity = "Silver", Increase = 15},
       new Upgrade { Name = "Acceleration III", Description = "Decreases The Amount Of Time To Reach Your Top Speed By X%", Rarity = "Gold", Increase = 20},
       // Top Speed
       new Upgrade { Name = "Gotta Go Fast I", Description = "Increases The Top Speed Of Your Ship By X%", Rarity = "Bronze", Increase = 50},
       new Upgrade { Name = "Gotta Go Fast II", Description = "Increases The Top Speed Of Your Ship By X%", Rarity = "Silver", Increase = 50},
       new Upgrade { Name = "Gotta Go Fast III", Description = "Increases The Top Speed Of Your Ship By X%", Rarity = "Gold", Increase = 50},
       // Ship Weight/Mass
       new Upgrade { Name = "Weight Lift", Description = "Your Ship Becomes Lighter by X%, Increasing Air Time", Rarity = "Bronze", Increase = -3},
       // Boost Time
       new Upgrade { Name = "Boost", Description = "Increases Your Ships Boost Duration By X%", Rarity = "Bronze", Increase = 025},
       new Upgrade { Name = "Boooost", Description = "Increases Your Ships Boost Duration By X%", Rarity = "Silver", Increase = 050},
       new Upgrade { Name = "Boooooost", Description = "Increases Your Ships Boost Duration By X%", Rarity = "Gold", Increase = 075},
       // Turning
       new Upgrade { Name = "Turning I", Description = "Increases Your Ships Handling Capabilities By X%", Rarity = "Bronze", Increase = 200},
       new Upgrade { Name = "Turning II", Description = "Increases Your Ships Handling Capabilities By X%", Rarity = "Silver", Increase = 400},
       new Upgrade { Name = "Turning III", Description = "Increases Your Ships Handling Capabilities By X%", Rarity = "Gold", Increase = 800},
       // Decreasing Drift
       new Upgrade { Name = "Aerodynamic Stabilisers", Description = "Take Sharper Turns By Reducing Your Ships Drifting By X%", Rarity = "Silver", Increase = -1},
       new Upgrade { Name = "Aerodynamic Augmentation System", Description = "Take Sharper Turns By Reducing Your Ships Drifting By X%", Rarity = "Gold", Increase = -2},
       // Braking
       new Upgrade { Name = "New Braking Fluid", Description = "Increases Your Ships Braking Power By X%", Rarity = "Bronze", Increase = 10},
       new Upgrade { Name = "State of the Art Breaking System", Description = "Increases Your Ships Braking Power By X%", Rarity = "Silver", Increase = 20},
       // Health Increase
       new Upgrade { Name = "Reinforce", Description = "Increase Your Ships Health Pool By X%", Rarity = "Bronze", Increase = 5},
       new Upgrade { Name = "Strong as an Ox", Description = "Increase Your Ships Health Pool By X%", Rarity = "Silver", Increase = 10},
       new Upgrade { Name = "Tough as a Mountain", Description = "Increase Your Ships Health Pool By X%", Rarity = "Gold", Increase = 20},

    };

    [SerializeField] private Button Upgrade_button1;
    [SerializeField] private Button Upgrade_button2;


    [SerializeField] private Text Upgrade_DescriptionText1;
    [SerializeField] private Text Upgrade_DescriptionText2;
  


    private void Start()
    {
        ButtonsSet();
    }

    public void ButtonsSet()
    {
        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < _Upgrades.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        ShuffleList(availableUpgrades);
        Upgrade Upgrade_1 = _Upgrades[availableUpgrades[0]];
        Upgrade Upgrade_2 = _Upgrades[availableUpgrades[1]];
  

        // Setting text
        Upgrade_button1.transform.GetChild(0).GetComponent<Text>().text = Upgrade_1.Name;
        Upgrade_button2.transform.GetChild(0).GetComponent<Text>().text = Upgrade_2.Name;


        // Replacing the X with increase value
        Upgrade_DescriptionText1.text = Upgrade_1.Description.Replace("X", Upgrade_1.Increase.ToString());
        Upgrade_DescriptionText2.text = Upgrade_2.Description.Replace("X", Upgrade_2.Increase.ToString());


        // Setting color of the buttons
        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Bronze", new Color(1, 1, 1, 1));
        rarityColors.Add("Silver", new Color(0.5f, 1f, 0.5f, 1));
        rarityColors.Add("Gold", new Color(0.75f, 0.25f, 0.75f, 1));


        Upgrade_button1.GetComponent<Image>().color = rarityColors[Upgrade_1.Rarity];
        Upgrade_button2.GetComponent<Image>().color = rarityColors[Upgrade_2.Rarity];
  
    }

    // UPGRADES
    public void UpgradeChosen(string Upgrade_chosen)
    {
        if (Upgrade_chosen == "Acceleration I")
        {
            // Acceleration += increase;
            Debug.Log("Acceleration Increase");
        }
        else if (Upgrade_chosen == "Acceleration II")
        {
            Debug.Log("Acceleration Increase");
        }
        else if (Upgrade_chosen == "Acceleration III")
        {
            Debug.Log("Acceleration Increase");
        }
        else if (Upgrade_chosen == "Gotta Go Fast I")
        {
            Debug.Log("Top Speed Increase");
        }
        else if (Upgrade_chosen == "Gotta Go Fast II")
        {
            Debug.Log("Top Speed Increase");
        }
        else if (Upgrade_chosen == "Gotta Go Fast III")
        {
            Debug.Log("Top Speed Increase");
        }
        else if (Upgrade_chosen == "Weight Lift")
        {
            Debug.Log("Weight Decrease");
        }
        else if (Upgrade_chosen == "Boost")
        {
            Debug.Log("Boost Duration Increase");
        }
        else if (Upgrade_chosen == "Boooost")
        {
            Debug.Log("Boost Duration Increase");
        }
        else if (Upgrade_chosen == "Boooooost")
        {
            Debug.Log("Boost Duration Increase");
        }
        else if (Upgrade_chosen == "Turning I")
        {
            Debug.Log("Increase Turning");
        }
        else if (Upgrade_chosen == "Turning II")
        {
            Debug.Log("Increase Turning");
        }
        else if (Upgrade_chosen == "Turning III")
        {
            Debug.Log("Increase Turning");
        }
        else if (Upgrade_chosen == "Aerodynamic Stabilisers")
        {
            Debug.Log("Decrease Drifting");
        }
        else if (Upgrade_chosen == "Aerodynamic Augmentation System")
        {
            Debug.Log("Decreasse Drifting");
        }
        else if (Upgrade_chosen == "New Braking Fluid")
        {
            Debug.Log("Increase Braking Power");
        }
        else if (Upgrade_chosen == "State of the Art Braking System")
        {
            Debug.Log("Increase Braking Power");
        }
        else if (Upgrade_chosen == "Reinforce")
        {
            Debug.Log("Increase Health");
        }
        else if (Upgrade_chosen == "Strong as an Ox")
        {
            Debug.Log("Increase Health");
        }
        else if (Upgrade_chosen == "Tough as a Mountain")
        {
            Debug.Log("Increase Health");
        }
    }

    // SHUFFLE LIST
    public void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public float Increase { get; set; }
    }
}






// TOOLTIPS SCRIPT


public class Tooltips : MonoBehaviour
{
    [SerializeField] private GameObject Tooltip_1;
    [SerializeField] private GameObject Tooltip_2;
    [SerializeField] private GameObject Tooltip_3;
    [SerializeField] private GameObject Tooltip_4;


    [SerializeField] private GameObject Tooltip_1_trigger;
    [SerializeField] private GameObject Tooltip_2_trigger;
    [SerializeField] private GameObject Tooltip_3_trigger;
    [SerializeField] private GameObject Tooltip_4_trigger;


    private void Update()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        if (raycastResultList.Count == 1)
        {
            Tooltip_1.SetActive(false);
            Tooltip_2.SetActive(false);
            Tooltip_3.SetActive(false);
            Tooltip_4.SetActive(false);
        }

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject == Tooltip_1_trigger)
            {
                Tooltip_1.SetActive(true);
            }
            else if (raycastResultList[i].gameObject == Tooltip_2_trigger)
            {
                Tooltip_2.SetActive(true);
            }
            else if (raycastResultList[i].gameObject == Tooltip_3_trigger)
            {
                Tooltip_3.SetActive(true);
            }
            else if (raycastResultList[i].gameObject == Tooltip_4_trigger)
            {
                Tooltip_4.SetActive(true);
            }
        }
    }
}


public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Rarity { get; set; }
        public float Increase { get; set; }
    }

