using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UserDataManager : GenericSingleton<UserDataManager>
{
    private UserData userData;

    public UserData UserData { get => userData; set => userData = value; }

    public void LoadData()
    {
        SaveSystem.LoadData(Constants.USERDATA_FILE, ref userData);
    }

    public void SaveData()
    {
        SaveSystem.SaveData(Constants.USERDATA_FILE, userData);
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

