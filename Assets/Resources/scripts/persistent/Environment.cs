using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;
using Persistent.Model;
using Coins;
using System;

namespace Persistent
{
    public class Environment : MonoBehaviour
    {
        public static Environment instance = null;

        public Scriptables.Dreyevers.Dreyever defaultDreyever;
        public Scriptables.Dreyevers.Dreyever[] allDreyevers;

        private string persistencePath;
        private Progress progress;

        private void Awake()
        {
            instance = this;

            persistencePath = Application.persistentDataPath + "/progress.data";
        }

        void Start()
        {
            if (File.Exists(persistencePath))
            {
                using (FileStream file = File.OpenRead(persistencePath))
                {
                    progress = (Progress) new BinaryFormatter().Deserialize(file);
                }
            }
            else
            {
                progress = new Progress();
                progress.currentDreyever = defaultDreyever.name;
            }
        }

        private void Save()
        {
            using (FileStream file = File.Create(persistencePath))
            {
                new BinaryFormatter().Serialize(file, progress);
            }
        }

        public Scriptables.Dreyevers.Dreyever GetCurrentDreyever()
        {
            return allDreyevers.OfType<Scriptables.Dreyevers.Dreyever>().ToList()
                .Find(dreyever => dreyever.name == progress.currentDreyever);
        }

        public void SetCurrentDreyever(Sprite sprite)
        {
            progress.currentDreyever = allDreyevers.OfType<Scriptables.Dreyevers.Dreyever>().ToList()
                .Find(dreyever => dreyever.sprite == sprite).name;

            Save();
        }

        public int GetCoinsCount(CoinType type)
        {
            if(type == CoinType.A)
            {
                return progress.coinsCountA;
            } else if(type == CoinType.B)
            {
                return progress.coinsCountB;
            } else if(type == CoinType.C)
            {
                return progress.coinsCountC;
            } else
            {
                throw new NotImplementedException();
            }
        }

        public void IncreaseCoinsCount(CoinType type)
        {
            if(type == CoinType.A)
            {
                progress.coinsCountA++;
            } else if(type == CoinType.B)
            {
                progress.coinsCountB++;
            } else if(type == CoinType.C)
            {
                progress.coinsCountC++;
            } else
            {
                throw new NotImplementedException();
            }

            Save();
        }
    }
}