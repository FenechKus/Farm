using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectedPlant;
    public bool isPlanting = false;
    public int money = 0;
    public TextMeshProUGUI moneyTxt;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    void Start() =>  moneyTxt.text = ("$" + money);



    public void SelectedPlant(PlantItem newPlant)
    {
        
        if (selectedPlant == newPlant)
        {
            selectedPlant.btnImage.color = buyColor;
            selectedPlant.btnText.text = "Buy";
            selectedPlant = null;
            isPlanting = false;

        }
        else
        {
            if (selectedPlant!=null)
            {
                selectedPlant.btnImage.color = buyColor;
                selectedPlant.btnText.text = "Buy";
            }
            selectedPlant = newPlant;
            selectedPlant.btnImage.color = cancelColor;
            selectedPlant.btnText.text = "Cancle";
            isPlanting = true;
        }
        
        
        
    }

    public void Transaction(int value)
    {
        money += value;
        moneyTxt.text = ("$" + money);
    }
}
