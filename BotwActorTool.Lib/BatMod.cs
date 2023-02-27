using BotwActorTool.Lib.Gamedata;
using BotwActorTool.Lib.Info;
using Byml.Security.Cryptography;
using MsbtLib;
using Nintendo.Byml;
using Nintendo.Sarc;
using Nintendo.Yaz0;
using System.Threading.Tasks;

namespace BotwActorTool.Lib
{
    public class BatMod
    {
        private static readonly string[] vanillaResidents = new[]
        {
            "GameROMPlayer",
            "Dm_Npc_Gerudo_HeroSoul_Kago",
            "Dm_Npc_Goron_HeroSoul_Kago",
            "Dm_Npc_Rito_HeroSoul_Kago",
            "Dm_Npc_Zora_HeroSoul_Kago",
            "Dm_Npc_RevivalFairy",
            "PlayerStole2",
            "WakeBoardRope",
            "Armor_Default_Extra_00",
            "Armor_Default_Extra_01",
            "Item_Conductor",
            "Animal_Insect_X",
            "Animal_Insect_A",
            "Animal_Insect_B",
            "Animal_Insect_M",
            "Animal_Insect_S",
            "Explode",
            "Obj_SupportApp_Wind",
            "NormalArrow",
            "FireArrow",
            "IceArrow",
            "ElectricArrow",
            "BombArrow_A",
            "AncientArrow",
            "BrightArrow",
            "BrightArrowTP",
            "RemoteBomb",
            "RemoteBomb2",
            "RemoteBombCube",
            "RemoteBombCube2",
            "Item_Magnetglove",
            "Obj_IceMakerBlock",
            "CarryBox",
            "PlayerShockWave",
            "FireRodLv1Fire",
            "FireRodLv2Fire",
            "FireRodLv2FireChild",
            "ThunderRodLv1Thunder",
            "ThunderRodLv2Thunder",
            "ThunderRodLv2ThunderChild",
            "IceRodLv1Ice",
            "IceRodLv2Ice",
            "Animal_Insect_H",
            "Animal_Insect_F",
            "Item_Material_07",
            "Item_Material_03",
            "Item_Material_01",
            "Item_Ore_F",
        };
        private static readonly Dictionary<string, uint> vanillaResidentHashes = new()
        {
            { "Actor/Pack/GameROMPlayer.sbactorpack", 4273479378 },
            { "Actor/Pack/Dm_Npc_Gerudo_HeroSoul_Kago.sbactorpack", 3773931489 },
            { "Actor/Pack/Dm_Npc_Goron_HeroSoul_Kago.sbactorpack", 2772346057 },
            { "Actor/Pack/Dm_Npc_Rito_HeroSoul_Kago.sbactorpack", 3880771950 },
            { "Actor/Pack/Dm_Npc_Zora_HeroSoul_Kago.sbactorpack", 653058859 },
            { "Actor/Pack/Dm_Npc_RevivalFairy.sbactorpack", 1521873499 },
            { "Actor/Pack/PlayerStole2.sbactorpack", 3408409891 },
            { "Actor/Pack/WakeBoardRope.sbactorpack", 106039674 },
            { "Actor/Pack/Armor_Default_Extra_00.sbactorpack", 1971312132 },
            { "Actor/Pack/Armor_Default_Extra_01.sbactorpack", 787596870 },
            { "Actor/Pack/Item_Conductor.sbactorpack", 76034968 },
            { "Actor/Pack/Animal_Insect_X.sbactorpack", 2167048374 },
            { "Actor/Pack/Animal_Insect_A.sbactorpack", 922219188 },
            { "Actor/Pack/Animal_Insect_B.sbactorpack", 1871755172 },
            { "Actor/Pack/Animal_Insect_M.sbactorpack", 2419912203 },
            { "Actor/Pack/Animal_Insect_S.sbactorpack", 191237640 },
            { "Actor/Pack/Explode.sbactorpack", 1240324486 },
            { "Actor/Pack/Obj_SupportApp_Wind.sbactorpack", 2049854056 },
            { "Actor/Pack/NormalArrow.sbactorpack", 1775651883 },
            { "Actor/Pack/FireArrow.sbactorpack", 3306872235 },
            { "Actor/Pack/IceArrow.sbactorpack", 1462512396 },
            { "Actor/Pack/ElectricArrow.sbactorpack", 3449856327 },
            { "Actor/Pack/BombArrow_A.sbactorpack", 506996187 },
            { "Actor/Pack/AncientArrow.sbactorpack", 1997745123 },
            { "Actor/Pack/BrightArrow.sbactorpack", 3786466584 },
            { "Actor/Pack/BrightArrowTP.sbactorpack", 3237859275 },
            { "Actor/Pack/RemoteBomb.sbactorpack", 1630332628 },
            { "Actor/Pack/RemoteBomb2.sbactorpack", 1026551472 },
            { "Actor/Pack/RemoteBombCube.sbactorpack", 604762016 },
            { "Actor/Pack/RemoteBombCube2.sbactorpack", 3094714298 },
            { "Actor/Pack/Item_Magnetglove.sbactorpack", 79775929 },
            { "Actor/Pack/Obj_IceMakerBlock.sbactorpack", 2662414309 },
            { "Actor/Pack/CarryBox.sbactorpack", 591811891 },
            { "Actor/Pack/PlayerShockWave.sbactorpack", 4183424749 },
            { "Actor/Pack/FireRodLv1Fire.sbactorpack", 1664698508 },
            { "Actor/Pack/FireRodLv2Fire.sbactorpack", 1898966725 },
            { "Actor/Pack/FireRodLv2FireChild.sbactorpack", 1542186388 },
            { "Actor/Pack/ThunderRodLv1Thunder.sbactorpack", 3823219124 },
            { "Actor/Pack/ThunderRodLv2Thunder.sbactorpack", 4092918367 },
            { "Actor/Pack/ThunderRodLv2ThunderChild.sbactorpack", 2300334641 },
            { "Actor/Pack/IceRodLv1Ice.sbactorpack", 3783925249 },
            { "Actor/Pack/IceRodLv2Ice.sbactorpack", 3550616022 },
            { "Actor/Pack/Animal_Insect_H.sbactorpack", 2721219692 },
            { "Actor/Pack/Animal_Insect_F.sbactorpack", 2125978181 },
            { "Actor/Pack/Item_Material_07.sbactorpack", 2654330357 },
            { "Actor/Pack/Item_Material_03.sbactorpack", 816752107 },
            { "Actor/Pack/Item_Material_01.sbactorpack", 3601373104 },
            { "Actor/Pack/Item_Ore_F.sbactorpack", 3484539377 },
            { "Actor/Pack/DemoXLinkActor.sbactorpack", 358577780 },
            { "Actor/Pack/ElectricWaterBall.sbactorpack", 1169177835 },
            { "Actor/Pack/EventCameraRumble.sbactorpack", 1569758598 },
            { "Actor/Pack/EventControllerRumble.sbactorpack", 2801441933 },
            { "Actor/Pack/EventMessageTransmitter1.sbactorpack", 4218999639 },
            { "Actor/Pack/EventSystemActor.sbactorpack", 319501893 },
            { "Actor/Pack/Fader.sbactorpack", 3871651769 },
            { "Actor/Pack/SceneSoundCtrlTag.sbactorpack", 152483989 },
            { "Actor/Pack/SoundTriggerTag.sbactorpack", 3413403585 },
            { "Actor/Pack/TerrainCalcCenterTag.sbactorpack", 1102160157 },
        };
        private static readonly char[] separators = new [] { '/', '\\' };
        private readonly List<string> actorNames;
        private readonly List<string> modResidents;
        private readonly List<Actor> openActors = new();
        private readonly Dictionary<uint, ActorInfo> actorInfo;
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, MsbtEntry>>> localization = new();
        private readonly FlagStore gameData = new();
        private readonly string path;
        public string Name { get; }
        public List<string> ActorNames { get { return actorNames; } }
        public List<Actor> OpenActors { get { return openActors; } }
        public BatMod(string modRoot)
        {
            path = modRoot;
            Name = Path.TrimEndingDirectorySeparator(path).Split(separators).Last();
            SarcFile sarc;
            bool nx = Convert.ToBoolean((int)Config.Mode);
            string contentName = nx ? "romfs" : "content";
            //string vanillaRoot = nx ? Config.GameDirNx : Config.UpdateDir;
            string vanillaRoot = @"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)";
            Config.Lang = "USen";
            
            // Flag parsing takes about 450ms, so it gets its own thread in the background, first
            string modBootupPath = $"{path}/{contentName}/Pack/Bootup.pack";
            if (File.Exists(modBootupPath))
            {
                sarc = new(modBootupPath);
            }
            else
            {
                sarc = new($"{vanillaRoot}/Pack/Bootup.pack");
            }
            var residentsTask = Task.Run(() => BymlFile.FromBinary(sarc.Files["Actor/ResidentActors.byml"])
                    .RootNode.Array
                    .Select(n => n.Hash["name"].String)
                    .Where(n => !vanillaResidents.Contains(n))
                    .ToList());
            var flagTask = Task.Run(() =>
            {
                foreach ((string name, byte[] data) in new SarcFile(Yaz0.Decompress(sarc.Files["GameData/gamedata.ssarc"])).Files)
                {
                    gameData.AddFlagsFromByml(name, BymlFile.FromBinary(data), false);
                }
            });
            
            // ActorInfo parsing takes about 350ms, so it gets its own thread second
            string infoPath = $"{path}/{contentName}/Actor/ActorInfo.product.sbyml";
            BymlNode infoRoot;
            if (Path.Exists(infoPath))
            {
                infoRoot = new BymlFile(Yaz0.Decompress(infoPath)).RootNode;
            }
            else
            {
                infoRoot = new BymlFile(Yaz0.Decompress($"{vanillaRoot}/Actor/ActorInfo.product.sbyml")).RootNode;
            }
            var info = Task.Run(
                () => infoRoot.Hash["Hashes"].Array
                    .Select((b, i) => (b.UInt, new ActorInfo(infoRoot.Hash["Actors"].Array[i])))
            );
            
            // Everything else takes about 100ms total, so that's what the main thread
            // does while waiting for flags and actor info
            string titleBGPath = $"{path}/{contentName}/Pack/TitleBG.pack";
            modResidents = new();
            if (Path.Exists(titleBGPath))
            {
                modResidents.AddRange(new SarcFile(titleBGPath).Files
                    .Where(f => f.Key.Contains(".sbactorpack") && vanillaResidentHashes[f.Key] != Crc32.Compute(f.Value))
                    .Select(f => f.Key.Split('/', '.')[1])
                    .ToList());
            }

            string bootupLangPath = $"{path}/{contentName}/Pack/Bootup_{Config.Lang}.pack";
            if (File.Exists(bootupLangPath))
            {
                sarc = new(bootupLangPath);
            }
            else
            {
                sarc = new($"{vanillaRoot}/Pack/Bootup_{Config.Lang}.pack");
            }
            // Since this is a nightmare, I won't attempt to let it document itself
            // Opens the message ssarc, then runs a task for each file in ActorType
            // which converts the contents of the file from `key_part: loc` to
            // `key: part: loc` and stores it as the localization dictionary
            localization = new SarcFile(Yaz0.Decompress(sarc.Files[$"Message/Msg_{Config.Lang}.product.ssarc"]))
                .Files
                .Where(kvp => kvp.Key.StartsWith("ActorType"))
                .Select(kvp =>
                {
                    string profile = kvp.Key.Split('/', '.')[1];
                    var texts = new MSBT(kvp.Value).GetTexts()
                        .Select(kvp =>
                        {
                            string[] parts = kvp.Key.Split("_");
                            string actor = string.Join('_', parts[0..^1]);
                            string part = parts[^1];
                            MsbtEntry entry = kvp.Value;
                            return (actor, part, entry);
                        })
                        .GroupBy(
                            t => t.actor,
                            t => (t.part, t.entry),
                            (a, b) => (a, b.ToDictionary(k => k.part, v => v.entry))
                        )
                        .ToDictionary(t => t.a, t => t.Item2);
                    return (profile, texts);
                })
                .ToDictionary(t => t.profile, t => t.texts);

            actorNames = Directory.EnumerateFiles($"{path}/{contentName}/Actor/Pack", "*.sbactorpack")
                .Select(p => Path.GetFileNameWithoutExtension(p))
                .ToList();
            modResidents.AddRange(residentsTask.Result);
            actorNames.AddRange(modResidents);
            actorNames.Sort();
            actorInfo = info.Result.ToDictionary(t => t.UInt, t => t.Item2);
            if (!flagTask.IsCompleted)
            {
                flagTask.Wait();
            }
        }
        public BatMod(string modRoot, bool nothing = false)
        {
            path = modRoot;
            Name = Path.TrimEndingDirectorySeparator(path).Split(separators).Last();
            bool nx = Convert.ToBoolean((int)Config.Mode);
            string contentName = nx ? "romfs" : "content";
            //string vanillaRoot = nx ? Config.GameDirNx : Config.UpdateDir;
            string vanillaRoot = @"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)";
            Config.Lang = "USen";

            actorNames = Directory.EnumerateFiles($"{path}/{contentName}/Actor/Pack", "*.sbactorpack")
                .Select(p => Path.GetFileNameWithoutExtension(p))
                .ToList();

            SarcFile sarc;
            string modBootupPath = $"{path}/{contentName}/Pack/Bootup.pack";
            if (File.Exists(modBootupPath))
            {
                sarc = new(modBootupPath);
            }
            else
            {
                sarc = new($"{vanillaRoot}/Pack/Bootup.pack");
            }
            modResidents = BymlFile.FromBinary(sarc.Files["Actor/ResidentActors.byml"])
                .RootNode.Array
                .Select(n => n.Hash["name"].String)
                .Where(n => !vanillaResidents.Contains(n))
                .ToList();
            foreach ((string name, byte[] data) in new SarcFile(Yaz0.Decompress(sarc.Files["GameData/gamedata.ssarc"])).Files)
            {
                gameData.AddFlagsFromByml(name, BymlFile.FromBinary(data), false);
            }

            string bootupLangPath = $"{path}/{contentName}/Pack/Bootup_{Config.Lang}.pack";
            if (File.Exists(bootupLangPath))
            {
                sarc = new(bootupLangPath);
            }
            else
            {
                sarc = new($"{vanillaRoot}/Pack/Bootup_{Config.Lang}.pack");
            }
            foreach ((string name, byte[] data) in new SarcFile(Yaz0.Decompress(sarc.Files[$"Message/Msg_{Config.Lang}.product.ssarc"])).Files)
            {
                string[] parts = name.Split('/');
                if (parts[0] == "ActorType")
                {
                    string profile = parts[1].Split('.')[0];
                    localization[profile] = new();
                    foreach ((string key, MsbtEntry value) in new MSBT(data).GetTexts())
                    {
                        string[] keyParts = key.Split('_');
                        string actor = string.Join('_', keyParts[0..^1]);
                        if (!localization[profile].ContainsKey(actor))
                        {
                            localization[profile][actor] = new();
                        }
                        string part = keyParts[^1];
                        localization[profile][actor][part] = value;
                    }
                }
            }

            string infoPath = $"{path}/{contentName}/Actor/ActorInfo.product.sbyml";
            BymlNode infoRoot;
            if (Path.Exists(infoPath))
            {
                infoRoot = new BymlFile(Yaz0.Decompress(infoPath)).RootNode;
            }
            else
            {
                infoRoot = new BymlFile(Yaz0.Decompress($"{vanillaRoot}/Actor/ActorInfo.product.sbyml")).RootNode;
            }
            actorInfo = new();
            for (int i = 0; i < infoRoot.Hash["Actors"].Array.Count; i++)
            {
                actorInfo.Add(infoRoot.Hash["Hashes"].Array[i].UInt, new ActorInfo(infoRoot.Hash["Actors"].Array[i]));
            }

            string titleBGPath = $"{path}/{contentName}/Pack/TitleBG.pack";
            if (Path.Exists(titleBGPath))
            {
                List<string> temp = new();
                SarcFile titlebg = new(titleBGPath);
                foreach ((string name, byte[] data) in titlebg.Files)
                {
                    if (name.Contains(".sbactorpack") && vanillaResidentHashes[name] != Crc32.Compute(data))
                    {
                        temp.Add(name.Split('/').Last().Split('.').First());
                    }
                }
                actorNames.AddRange(modResidents);
            }
            actorNames.Sort();
        }
    }
}
