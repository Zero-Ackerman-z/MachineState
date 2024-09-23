using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyPath { 
    CuartoSala,
    CuartoBanno,
    CuartoCocina, 

    BannoSala,
    BannoCuarto,
    BannoCocina,

    SalaCuarto,
    SalaBanno,
    SalaCocina,

    CocinaSala,
    CocinaBanno,
    CocinaCuarto

}

[System.Serializable]
public class PathStore
{
    public KeyPath type;
    public Path paths = new Path();
    public PathStore()
    {

    }
}

public class PathManager : MonoBehaviour
{
    #region Singleton
    static PathManager _instance;
    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }
    static public PathManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(PathManager)) as PathManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("PathManager");
                    // DontDestroyOnLoad(go);
                    _instance = go.AddComponent<PathManager>();
                }
            }
            return _instance;
        }
    }
    #endregion
    Dictionary<KeyPath,Path> pathsManager = new Dictionary<KeyPath,Path>();

    public List<PathStore> listD = new List<PathStore>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pathStore in listD)
        {
            pathsManager[pathStore.type] = pathStore.paths;
        }
    }
    public Path GetPath(KeyPath path)
    {  
        return pathsManager[path];
    } 
    
}
