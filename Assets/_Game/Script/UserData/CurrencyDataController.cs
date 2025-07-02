using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CurrencyDataController : GenericSingleton<CurrencyDataController>
{
    private CurrencyData currencyData;

    public CurrencyData CurrencyData { get { return currencyData; } }

    public void LoadData()
    {
        SaveSystem.LoadData(Constants.CURRENCY_FILE, ref currencyData);
    }

    public void SaveData()
    {
        SaveSystem.SaveData(Constants.CURRENCY_FILE, currencyData);
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

}

[Serializable]
public class CurrencyData
{
    public int Gem;
    public int Coin;
}
