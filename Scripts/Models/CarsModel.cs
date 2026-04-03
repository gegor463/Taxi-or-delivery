using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CarsModel 
{
    public int Id { get; set; }
    
    public string NameOfCar { get; set; }
    public int CostOfCar { get; set; }
    public string NameOfUpgrade { get; set; }
    public int ValueOfUpgrade {  get; set; }
    public int CostOfUpgrade { get; set; }
    public int IsPurchasedCar { get; set; }
}
