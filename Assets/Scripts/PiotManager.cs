using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiotManager : MonoBehaviour
{
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    private SpriteRenderer plot;

    private SpriteRenderer plant;
    private BoxCollider2D plantCollider;
   
    private bool isPlanted = false;
    private int stage = 0;
    private float timer;



    private PlantObject selectedPlant;

    private FarmManager fm;

    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.GetComponentInParent<FarmManager>();

        plot = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && stage < selectedPlant.plantStages.Length-1)
            {
                timer = selectedPlant.timeBtwStages;
                stage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if(stage == selectedPlant.plantStages.Length-1 && !fm.isPlanting)
                Harvest();
        }
        else if(fm.isPlanting && fm.selectedPlant.plant.buyPrice <=fm.money)
        {
            Plant(fm.selectedPlant.plant);
        }
    }

    private void OnMouseOver()
    {
        if (fm.isPlanting)
        {
            if (isPlanted || fm.selectedPlant.plant.buyPrice > fm.money)
            {
                //cnt
                plot.color = unavailableColor;
            }
            else
            {
                //can
                plot.color = availableColor;
            }
        }
    }

    private void OnMouseExit() => plot.color = Color.white;

    private void Harvest()
    {  
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
    }

    private void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);

        stage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    private void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[stage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
}
