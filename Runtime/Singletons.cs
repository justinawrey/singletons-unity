using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Singletons
{
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T _instance = null;
    public static T Instance => _instance;

    protected abstract bool PersistAcrossScenes { get; }

    private void Awake()
    {
      if (_instance == null)
      {
        _instance = this as T;
        if (PersistAcrossScenes)
        {
          DontDestroyOnLoad(gameObject);
        }
        return;
      }

      if (_instance == this) return;
      Destroy(gameObject);
    }
  }

  public class ScenePersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
  {
    protected override bool PersistAcrossScenes => true;
  }

  public class NonScenePersistenSingleton<T> : Singleton<T> where T : MonoBehaviour
  {
    protected override bool PersistAcrossScenes => false;
  }
}