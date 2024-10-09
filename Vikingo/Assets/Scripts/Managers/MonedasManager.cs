using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] private int monedasTest;
    public int MonedasTotales {  get; set; }//almacenar monedas total

    private string KEY_MONEDAS = "MYJUEGOS_MONEDAS";//llave para guardar

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_MONEDAS);
        CargarMonedas(); 
    }

    private void CargarMonedas()

    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest); //cargar monedas

    }
    public void AñadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);// guardar monedas
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad) // remover monedas

    {
        if (cantidad > MonedasTotales)
        {
            return;
        }

        MonedasTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }


}
