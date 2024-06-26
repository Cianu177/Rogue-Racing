using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_button : MonoBehaviour
{
    [SerializeField] private Upgrades Upgrades_script;

    public void Upgrade()
    {
        string Upgrade_chosen = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
        Upgrades_script.UpgradeChosen(Upgrade_chosen);
        Upgrades_script.ButtonsSet();
    }
}
