using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api;
using System.Collections.Generic;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Api.Components;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Knowledge;
using MelonLoader;
using UnityEngine;
using Il2CppAssets;
using Il2CppAssets.Scripts.Data;
using Il2CppAssets.Scripts.Data.TrophyStore;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Player;
using Il2CppAssets.Scripts.Utils;


[assembly: MelonInfo(typeof(BTD6Unlocker.BTD6UnlockerMain), "BTD6 Unlocker", "1.0.1", "Interesting")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace BTD6Unlocker
{
	public class BTD6UnlockerMain : BloonsTD6Mod
    {
        public static ModHelperButton button;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("BTD6Unlocker was loaded.");
        }

        public override void OnUpdate()
        {
            ModSettingHotkey instaHotkey = new ModSettingHotkey(KeyCode.F1, Il2CppAssets.Scripts.Unity.UI_New.InGame.HotkeyModifier.None);
            ModSettingHotkey knowledgeHotkey = new ModSettingHotkey(KeyCode.F2, Il2CppAssets.Scripts.Unity.UI_New.InGame.HotkeyModifier.None);
            ModSettingHotkey moneyAndXPHotkey = new ModSettingHotkey(KeyCode.F3, Il2CppAssets.Scripts.Unity.UI_New.InGame.HotkeyModifier.None);
            ModSettingHotkey trophiesButton = new ModSettingHotkey(KeyCode.F4, Il2CppAssets.Scripts.Unity.UI_New.InGame.HotkeyModifier.None);
            ModSettingHotkey unlockTrophyItemsButton = new ModSettingHotkey(KeyCode.F5, Il2CppAssets.Scripts.Unity.UI_New.InGame.HotkeyModifier.None);

            //f1 gives all insta monkeys
            if (instaHotkey.JustPressed())
            {
                List<string> monkes = Helpers.ValidBaseTowerNames().ToList();
                foreach (string monke in monkes)
                {
                    GetAllInstaMonkes(monke);
                }
            }

            //f2 unlocks all monkey knowledge
            if(knowledgeHotkey.JustPressed())
            {
                GameModel gameModel = Game.instance.model;
                KnowledgeModel[] models = gameModel.allKnowledge.ToArray();
                foreach (KnowledgeModel model in models)
                {
                    string m = model.name.Remove(0, 15);
                    if (GameExt.GetBtd6Player(Game.instance).HasKnowledge(m))
                        MelonLogger.Msg($"{m} is already unlocked");
                    else
                    {
                        MelonLogger.Msg($"unlocked {m}");
                        GameExt.GetBtd6Player(Game.instance).AcquireKnowledge(m);
                    }
                }
            }

            //f3 gives monkey money and monkey xp
            if (moneyAndXPHotkey.JustPressed()) 
            {
                GameExt.AddMonkeyMoney(Game.instance, 1000000);

                Il2CppSystem.Collections.Generic.List<string> monkes = Helpers.ValidBaseTowerNames();
                foreach (string monke in monkes)
                {
                    GameExt.GetBtd6Player(Game.instance).AddTowerXP(monke, 1000000);
                    MelonLogger.Msg($"Added 1000000 xp to all monkeys");
                }
            }

            //f4 gives trophies
            if (trophiesButton.JustPressed()) 
            {
                GameExt.GetBtd6Player(Game.instance).GainTrophies(10000, "event", null);
                MelonLogger.Msg("added 10000 trophies");
            }

            //f5 unlocks almost every trophy item
            if (unlockTrophyItemsButton.JustPressed())
            {
                TrophyStoreItems items = GameData.Instance.trophyStoreItems;
                List<TrophyStoreItem> itemList = items.storeItems.ToList();
                foreach(TrophyStoreItem item in itemList)
                {
                    GameExt.GetBtd6Player(Game.instance).AddTrophyStoreItem(item.name);
                    MelonLogger.Msg($"Unlocked {item.name}");
                }
            }
        }
        
        public static void GetAllInstaMonkes(string monke)
        {
            GameExt.GetBtd6Player(Game.instance).AddInstaTower(monke, new int[3], 50);
            for (int i = 1; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Btd6Player btd6Player = GameExt.GetBtd6Player(Game.instance);
                    int[] array = new int[3];
                    array[0] = i;
                    array[1] = j;
                    btd6Player.AddInstaTower(monke, array, 50);
                    GameExt.GetBtd6Player(Game.instance).AddInstaTower(monke, new int[]
                    {
                        i,
                        0,
                        j
                    }, 50);
                }
            }
            for (int k = 1; k < 6; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    Btd6Player btd6Player2 = GameExt.GetBtd6Player(Game.instance);
                    int[] array2 = new int[3];
                    array2[0] = l;
                    array2[1] = k;
                    btd6Player2.AddInstaTower(monke, array2, 50);
                    GameExt.GetBtd6Player(Game.instance).AddInstaTower(monke, new int[]
                    {
                        0,
                        k,
                        l
                    }, 50);
                }
            }
            for (int m = 1; m < 6; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    GameExt.GetBtd6Player(Game.instance).AddInstaTower(monke, new int[]
                    {
                        0,
                        n,
                        m
                    }, 50);
                    GameExt.GetBtd6Player(Game.instance).AddInstaTower(monke, new int[]
                    {
                        n,
                        0,
                        m
                    }, 50);
                }
            }
        }
    }
}
