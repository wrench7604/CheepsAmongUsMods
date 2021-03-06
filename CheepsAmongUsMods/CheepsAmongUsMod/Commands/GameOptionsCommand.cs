﻿using CheepsAmongUsApi.API;
using CheepsAmongUsApi.API.Enumerations;
using CheepsAmongUsMod.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameStateEnum = KHNHJFFECBP.KGEKNMMAKKN;
using TaskBarUpdates = GLOLCBDMNHG;

namespace CheepsAmongUsMod.Commands
{
    public class GameOptionsCommand : Command
    {
        public const string CommandStr = "/gameoptions";

        public GameOptionsCommand() : base(CommandStr, $"{CommandStr} <Option> <Value>", "Changes Game Options") { }

        public override void Executed(string[] args)
        {
            if (CheepsAmongUsMod.IsDecidingClient)
            {
                if (CheepsAmongUsMod.CurrentGame.GameState == GameStateEnum.Started)
                {
                    PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                        $"{Functions.ColorRed}The game options cannot be changed when the game has already started."
                        ); //Send chat to player
                }
                else
                {
                    try
                    {
                        switch (args[1].ToLower())
                        {
                            case "commontasks":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.CommonTasks = value;
                                    break;
                                }
                            case "confirmejects":
                                {
                                    var value = bool.Parse(args[2]);

                                    GameOptions.ConfirmEjects = value;
                                    break;
                                }
                            case "crewmatevision":
                                {
                                    var value = float.Parse(args[2]);

                                    GameOptions.CrewmateVision = value;
                                    break;
                                }
                            case "discussiontime":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.DiscussionTime = value;
                                    break;
                                }
                            case "emergencycooldown":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.EmergencyCooldown = value;
                                    break;
                                }
                            case "emergencymeetings":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.EmergencyMeetings = value;
                                    break;
                                }
                            case "impostorcount":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.ImpostorCount = value;
                                    break;
                                }
                            case "impostorvision":
                                {
                                    var value = float.Parse(args[2]);

                                    GameOptions.ImpostorVision = value;
                                    break;
                                }
                            case "killcooldown":
                                {
                                    var value = float.Parse(args[2]);

                                    GameOptions.KillCooldown = value;
                                    break;
                                }
                            case "killdistance":
                                {
                                    try
                                    {
                                        var val = (KillDistance)Enum.Parse(typeof(KillDistance), args[2], true);
                                        GameOptions.KillDistance = val;
                                    }
                                    catch
                                    {
                                        PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                                            $"{Functions.ColorRed}Syntax[]: {Functions.ColorPurple}{CommandStr} KillDistance <{NumEnumVals(typeof(KillDistance), " | ")}>"
                                            ); //Send syntax to player
                                        return;
                                    }

                                    break;
                                }
                            case "longtasks":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.LongTasks = (byte)value;
                                    break;
                                }
                            case "map":
                                {
                                    try
                                    {
                                        var val = (MapType)Enum.Parse(typeof(MapType), args[2], true);
                                        GameOptions.Map = val;
                                    }
                                    catch
                                    {
                                        PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                                            $"{Functions.ColorRed}Syntax[]: {Functions.ColorPurple}{CommandStr} Map <{NumEnumVals(typeof(MapType), " | ")}>"
                                            ); //Send syntax to player
                                        return;
                                    }

                                    break;
                                }
                            case "maxplayers":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.MaxPlayers = value;
                                    break;
                                }
                            case "playerspeed":
                                {
                                    var value = float.Parse(args[2]);

                                    GameOptions.PlayerSpeed = value;
                                    break;
                                }
                            case "shorttasks":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.ShortTasks = value;
                                    break;
                                }
                            case "taskbarupdates":
                                {
                                    try
                                    {
                                        var val = (TaskBarUpdates)Enum.Parse(typeof(TaskBarUpdates), args[2], true);
                                        GameOptions.TaskBarUpdates = val;
                                    }
                                    catch
                                    {
                                        PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                                            $"{Functions.ColorRed}Syntax[]: {Functions.ColorPurple}{CommandStr} TaskBarUpdates <{NumEnumVals(typeof(TaskBarUpdates), " | ")}>"
                                            ); //Send syntax to player
                                        return;
                                    }

                                    break;
                                }
                            case "votingtime":
                                {
                                    var value = int.Parse(args[2]);

                                    GameOptions.VotingTime = value;
                                    break;
                                }
                        }

                        GameOptions.RpcSyncSettings();
                        PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                        $"{Functions.ColorLime}Game Options have been updated."
                        ); //Send chat to player
                    }
                    catch
                    {
                        string options =
                            "CommonTasks | ConfirmEjects | CrewmateVision | DiscussionTime | EmergencyCooldown | " +
                            "EmergencyMeetings | ImpostorCount | ImpostorVision | KillCooldown | KillDistance | " +
                            "LongTasks | Map | MaxPlayers | PlayerSpeed | ShortTasks | TaskBarUpdates | VotingTime";

                        PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                            $"{Functions.ColorRed}Syntax[]: {Functions.ColorPurple}{CommandStr} <{options}> <Value>"
                            ); //Send syntax to player
                    }
                }
            }
            else
            {
                PlayerHudManager.AddChat(PlayerController.LocalPlayer,
                    $"{Functions.ColorRed}Sorry, but only {Functions.ColorCyan}{CheepsAmongUsMod.DecidingClient.PlayerData.PlayerName} " +
                    $"{Functions.ColorRed}can change the game options."
                    ); //Send chat to player
            }
        }

        public static string NumEnumVals(Type enumTypes, string delimiter)
        {
            string vals = "";

            foreach (var type in Enum.GetValues(enumTypes))
                vals += type.ToString() + delimiter;

            vals = vals.Substring(0, vals.Length - delimiter.Length);

            return vals;
        }
    }
}
