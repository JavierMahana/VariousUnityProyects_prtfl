using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Clase")]
public class Clase : ScriptableObject {

    public MaxStats maxStats;
    public GrowthRates gr;
    public TipoDeMovimiento tipoDeMovimiento;
    public TipoDeArma[] tipoDeArmasEquipables;
    public Sprite defaultSrite;
    
	
}
