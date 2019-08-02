
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;
    public GameObject mapContainerPF;

    [HideInInspector]
    public GameObject mapContainerGO;
    public ColorATerreno[] opciones;

   
    
 
    public void GenerateMap()
    {
        Instantiate();
        
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }


        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {       
                GenerateConections(x, y);
            }
        }
    }
    void GenerateTile(int x, int y)
    {
        
        Color pixelColor = map.GetPixel(x, y);
        

        if (pixelColor.a == 0)
        {
            //the pixel is transparent 
            return;
        }

        

        foreach (ColorATerreno opcion in opciones)
        {
           

            if (opcion.color.Equals(pixelColor))
            {
                
                Casilla casilla = opcion.casilla.gameObject.GetComponent<Casilla>();
                casilla.position = new Vector2Int(x, y);
                //Aca la posicion es la esquina inferior izquierda
                Instantiate(opcion.casilla, new Vector2(x*0.16f, y*0.16f)  , Quaternion.identity, mapContainerGO.transform);
            }
        }

    }

    //Acá esta el error!!
    public void GenerateConections(int x, int y)
    {
        Casilla actualTile = TileDeEstasCoordenadas(x,y);
        
        if (x>0)
        {
            actualTile.conexiones.Add(new Conexion(actualTile, TileDeEstasCoordenadas(x - 1, y)));
        }
        
        if (x<map.width - 1)
        {
            actualTile.conexiones.Add(new Conexion(actualTile, TileDeEstasCoordenadas(x + 1, y)));
        }
        if (y>0)
        {
            actualTile.conexiones.Add(new Conexion(actualTile, TileDeEstasCoordenadas(x , y - 1)));
        }
        if (y<map.height - 1)
        {
            actualTile.conexiones.Add(new Conexion(actualTile, TileDeEstasCoordenadas(x, y + 1)));
        }
    }

    public void Instantiate()
    {
        mapContainerGO = mapContainerPF;
        mapContainerGO = Instantiate(mapContainerGO);

       
    }

    public Casilla TileDeEstasCoordenadas(int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        Casilla aTerreno = null;
        for (int i = 0; i < mapContainerGO.transform.childCount; i++)
        {
            GameObject a = mapContainerGO.transform.GetChild(i).gameObject;
            aTerreno = a.GetComponent<Casilla>();
            if (aTerreno.position == position)
            {
                break;
            }
        }
        return aTerreno;
       
    }

}
