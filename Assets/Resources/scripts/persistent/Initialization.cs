using Persistent.Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

namespace Persistent
{
    public class Initialization : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            GameObject persistentPrefab = Resources.Load<GameObject>("prefabs/Persistent");
            GameObject persistentObject = Instantiate(persistentPrefab);

            Environment environment = persistentObject.GetComponent<Environment>();
            environment.persistencePath = Application.persistentDataPath + "/progress.data";

            Progress progress;
            if (File.Exists(environment.persistencePath))
            {
                using (FileStream file = File.OpenRead(environment.persistencePath))
                {
                    progress = (Progress)new BinaryFormatter().Deserialize(file);
                }
            }
            else
            {
                progress = new Progress();
                progress.currentDreyever = environment.defaultDreyever.name;
                progress.unlockedDreyevers = environment.defaultUnlockedDreyevers.Select(dreyever =>
                {
                    return dreyever.name;
                }).ToArray();
            }

            environment.progress = progress;
        }
    }
}