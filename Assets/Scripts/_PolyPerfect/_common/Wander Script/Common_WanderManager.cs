using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyperfect.Common
{
  public class Common_WanderManager : MonoBehaviour
  {
    [SerializeField]
    private bool peaceTime;
    public bool PeaceTime
    {
      get
      {
        return peaceTime;
      }
      set
      {
        peaceTime = true;
      }
    }

    private static Common_WanderManager instance;
    public static Common_WanderManager Instance
    {
      get
      {
        return instance;
      }
    }

    private void Awake()
    {
      if (instance != null && instance != this)
      {
        Destroy(gameObject);
        return;
      }

      instance = this;
    }

    private void Start()
    {
      if (peaceTime)
      {
        Debug.Log("AnimalManager: Peacetime is enabled, all animals are non-agressive.");
      }
    }
  }
}