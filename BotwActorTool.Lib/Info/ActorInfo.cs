#pragma warning disable CS0169, CS0414, CS0649, IDE0044
// Reflection - the fields are assigned to/used, just not in a way the compiler can verify

/*
 * Abandon hope, all ye who enter here
 */

using Byml.Security.Cryptography;
using Nintendo.Aamp;
using Nintendo.Byml;
using Nintendo.Yaz0;
using Syroot.Maths;
using System.Net.NetworkInformation;
using System.Reflection;

namespace BotwActorTool.Lib.Info
{
    public class ActorInfo
    {
        private static BymlFile? actor_info_file;
        private FarActor? actor;
        public FarActor Actor { set => actor = value; }

        #region ActorInfo Params
        private Dictionary<string, int>? Chemical;
        private Dictionary<string, float>? aabbMax;
        private Dictionary<string, float>? aabbMin;
        private float? actorScale;
        private float? addColorA;
        private float? addColorB;
        private float? addColorG;
        private float? addColorR;
        private float? animalUnitBasePlayRate;
        private int? armorDefenceAddLevel;
        private bool? armorEffectAncientPowUp;
        private int? armorEffectEffectLevel;
        private string? armorEffectEffectType;
        private bool? armorEffectEnableClimbWaterfall;
        private bool? armorEffectEnableSpinAttack;
        private int? armorHeadMantleType;
        private string? armorNextRankName;
        private int? armorStarNum;
        private bool? armorUpperDisableSelfMantle;
        private int? armorUpperUseMantleType;
        private int? arrowArrowDeletePer;
        private int? arrowArrowNum;
        private int? arrowDeleteTime;
        private int? arrowDeleteTimeWithChemical;
        private int? arrowEnemyShootNumForDelete;
        private int? attackPower;
        private float? baseScaleX;
        private float? baseScaleY;
        private float? baseScaleZ;
        private string? bfres;
        private float? boundingForTraverse;
        private string? bowArrowName;
        private bool? bowIsLeadShot;
        private bool? bowIsRapidFire;
        private int? bowLeadShotNum;
        private int? bowRapidFireNum;
        private int? bugMask;
        private int? cookSpiceBoostEffectiveTime;
        private int? cookSpiceBoostHitPointRecover;
        private int? cookSpiceBoostMaxHeartLevel;
        private int? cookSpiceBoostStaminaLevel;
        private int? cookSpiceBoostSuccessRate;
        private int? cureItemEffectLevel;
        private string? cureItemEffectType;
        private int? cureItemEffectiveTime;
        private int? cureItemHitPointRecover;
        private float? cursorOffsetY;
        private Dictionary<string, string[]>? drops;
        private string? elink;
        private int? enemyRank;
        private Dictionary<string, dynamic>? farModelCulling;
        private int? generalLife;
        private Dictionary<string, string>[]? homeArea;
        private string? horseASVariation;
        private int? horseGearTopChargeNum;
        private int? horseNature;
        private int? horseUnitRiddenAnimalType;
        private int? instSize;
        private string[]? invalidTimes;
        private string[]? invalidWeathers;
        private bool? isHasFar;
        private int? itemBuyingPrice;
        private int? itemCreatingPrice;
        private int? itemSaleRevivalCount;
        private int? itemSellingPrice;
        private int? itemStainColor;
        private string? itemUseIconActorName;
        private Dictionary<string, dynamic>[]? locators;
        private float? lookAtOffsetY;
        private string? mainModel;
        private float? masterSwordSearchEvilDist;
        private string? masterSwordSleepActorName;
        private string? masterSwordTrueFormActorName;
        private int? masterSwordTrueFormAttackPower;
        private int? monsterShopBuyMamo;
        private int? monsterShopSellMamo;
        private Dictionary<string, float>? motorcycleEnergy;
        private string? name;
        private string? normal0ItemName01;
        private string? normal0ItemName02;
        private string? normal0ItemName03;
        private int? normal0ItemNum01;
        private int? normal0ItemNum02;
        private int? normal0ItemNum03;
        private int? normal0StuffNum;
        private int? pictureBookLiveSpot1;
        private int? pictureBookLiveSpot2;
        private int? pictureBookSpecialDrop;
        private string? profile;
        private float? rigidBodyCenterY;
        private int? rupeeRupeeValue;
        private bool? seriesArmorEnableCompBonus;
        private string? seriesArmorSeriesType;
        private string? slink;
        private int? sortKey;
        private bool? systemIsGetItemSelf;
        private string? systemSameGroupActorName;
        private Dictionary<string, int>? tags;
        private uint[]? terrainTextures;
        private string? travelerAppearGameDataName;
        private string? travelerDeleteGameDataName;
        private string? travelerRideHorseName;
        private string? travelerRoutePoint0Name;
        private string? travelerRoutePoint1Name;
        private string? travelerRoutePoint2Name;
        private string? travelerRoutePoint3Name;
        private string? travelerRoutePoint4Name;
        private string? travelerRoutePoint5Name;
        private string? travelerRoutePoint6Name;
        private string? travelerRoutePoint7Name;
        private string? travelerRoutePoint8Name;
        private string? travelerRoutePoint9Name;
        private string? travelerRoutePoint10Name;
        private string? travelerRoutePoint11Name;
        private string? travelerRoutePoint12Name;
        private string? travelerRoutePoint13Name;
        private string? travelerRoutePoint14Name;
        private string? travelerRoutePoint15Name;
        private string? travelerRoutePoint16Name;
        private string? travelerRoutePoint17Name;
        private string? travelerRoutePoint18Name;
        private string? travelerRoutePoint19Name;
        private string? travelerRoutePoint20Name;
        private string? travelerRoutePoint21Name;
        private string? travelerRoutePoint22Name;
        private string? travelerRoutePoint23Name;
        private string? travelerRoutePoint24Name;
        private string? travelerRoutePoint25Name;
        private string? travelerRoutePoint26Name;
        private string? travelerRoutePoint27Name;
        private string? travelerRouteType;
        private float? traverseDist;
        private string? variationMatAnim;
        private int? variationMatAnimFrame;
        private int? weaponCommonGuardPower;
        private int? weaponCommonPoweredSharpAddAtkMax;
        private int? weaponCommonPoweredSharpAddAtkMin;
        private int? weaponCommonPoweredSharpAddLifeMax;
        private int? weaponCommonPoweredSharpAddLifeMin;
        private float? weaponCommonPoweredSharpAddRapidFireMax;
        private float? weaponCommonPoweredSharpAddRapidFireMin;
        private bool? weaponCommonPoweredSharpAddSpreadFire;
        private bool? weaponCommonPoweredSharpAddSurfMaster;
        private float? weaponCommonPoweredSharpAddThrowMax;
        private float? weaponCommonPoweredSharpAddThrowMin;
        private bool? weaponCommonPoweredSharpAddZoomRapid;
        private int? weaponCommonPoweredSharpWeaponAddGuardMax;
        private int? weaponCommonPoweredSharpWeaponAddGuardMin;
        private int? weaponCommonRank;
        private int? weaponCommonSharpWeaponAddAtkMax;
        private int? weaponCommonSharpWeaponAddAtkMin;
        private bool? weaponCommonSharpWeaponAddCrit;
        private int? weaponCommonSharpWeaponAddGuardMax;
        private int? weaponCommonSharpWeaponAddGuardMin;
        private int? weaponCommonSharpWeaponAddLifeMax;
        private int? weaponCommonSharpWeaponAddLifeMin;
        private float? weaponCommonSharpWeaponPer;
        private int? weaponCommonStickDamage;
        private string? xlink;
        private string? yLimitAlgorithm;
        public int InstSize { get => instSize ?? 0; set => instSize = value; }
        public bool IsHasFar { get => isHasFar is true; set => isHasFar = value == false ? null : true; }
        public int SortKey { get => sortKey ?? 0; set => sortKey = value; }
        #endregion

        #region Param Maps
        private static readonly (string, string)[] INFO_LINKS = new (string, string)[]
        {
            ("actorScale", "ActorScale"),
            ("elink", "ElinkUser"),
            ("profile", "ProfileUser"),
            ("slink", "SlinkUser"),
            ("xlink", "XlinkUser"),
        };
        private static readonly (string, string)[] DROPTABLE_MAP = new (string, string)[]
        {
            ("Normal", "ItemName"),
            ("Normal2", "ItemName"),
            ("Normal3", "ItemName"),
            ("Normal4", "ItemName"),
            ("Normal5", "ItemName"),
        };
        private static readonly Dictionary<string, (string, string)> GPARAM_MAP = new()
        {
            { "animalUnitBasePlayRate", ("AnimalUnit", "BasePlayRate") },
            { "armorDefenceAddLevel", ("Armor", "DefenceAddLevel") },
            { "armorNextRankName", ("Armor", "NextRankName") },
            { "armorEffectAncientPowUp", ("ArmorEffect", "AncientPowUp") },
            { "armorEffectEffectLevel", ("ArmorEffect", "EffectLevel") },
            { "armorEffectEffectType", ("ArmorEffect", "EffectType") },
            { "armorEffectEnableClimbWaterfall", ("ArmorEffect", "EnableClimbWaterfall") },
            { "armorEffectEnableSpinAttack", ("ArmorEffect", "EnableSpinAttack") },
            { "armorHeadMantleType", ("ArmorHead", "MantleType") },
            { "armorUpperDisableSelfMantle", ("ArmorUpper", "DisableSelfMantle") },
            { "armorUpperUseMantleType", ("ArmorUpper", "UseMantleType") },
            { "arrowArrowDeletePer", ("Arrow", "ArrowDeletePer") },
            { "arrowArrowNum", ("Arrow", "ArrowNum") },
            { "arrowDeleteTime", ("Arrow", "DeleteTime") },
            { "arrowDeleteTimeWithChemical", ("Arrow", "DeleteTimeWithChemical") },
            { "arrowEnemyShootNumForDelete", ("Arrow", "EnemyShootNumForDelete") },
            { "attackPower", ("Attack", "Power") },
            { "bowArrowName", ("Bow", "ArrowName") },
            { "bowIsLeadShot", ("Bow", "IsLeadShot") },
            { "bowIsRapidFire", ("Bow", "IsRapidFire") },
            { "bowLeadShotNum", ("Bow", "LeadShotNum") },
            { "bowRapidFireNum", ("Bow", "RapidFireNum") },
            { "cookSpiceBoostEffectiveTime", ("CookSpice", "BoostEffectiveTime") },
            { "cookSpiceBoostHitPointRecover", ("CookSpice", "BoostHitPointRecover") },
            { "cookSpiceBoostMaxHeartLevel", ("CookSpice", "BoostMaxHeartLevel") },
            { "cookSpiceBoostStaminaLevel", ("CookSpice", "BoostStaminaLevel") },
            { "cookSpiceBoostSuccessRate", ("CookSpice", "BoostSuccessRate") },
            { "cureItemEffectLevel", ("CureItem", "EffectLevel") },
            { "cureItemEffectType", ("CureItem", "EffectType") },
            { "cureItemEffectiveTime", ("CureItem", "EffectiveTime") },
            { "cureItemHitPointRecover", ("CureItem", "HitPointRecover") },
            { "enemyRank", ("Enemy", "Rank") },
            { "generalLife", ("General", "Life") },
            { "horseASVariation", ("Horse", "ASVariation") },
            { "horseGearTopChargeNum", ("Horse", "GearTopChargeNum") },
            { "horseNature", ("Horse", "Nature") },
            { "horseUnitRiddenAnimalType", ("HorseUnit", "RiddenAnimalType") },
            { "itemBuyingPrice", ("Item", "BuyingPrice") },
            { "itemCreatingPrice", ("Item", "CreatingPrice") },
            { "itemSaleRevivalCount", ("Item", "SaleRevivalCount") },
            { "itemSellingPrice", ("Item", "SellingPrice") },
            { "itemStainColor", ("Item", "StainColor") },
            { "itemUseIconActorName", ("Item", "UseIconActorName") },
            { "masterSwordSearchEvilDist", ("MasterSword", "SearchEvilDist") },
            { "masterSwordSleepActorName", ("MasterSword", "SleepActorName") },
            { "masterSwordTrueFormActorName", ("MasterSword", "TrueFormActorName") },
            { "masterSwordTrueFormAttackPower", ("MasterSword", "TrueFormAttackPower") },
            { "monsterShopBuyMamo", ("MonsterShop", "BuyMamo") },
            { "monsterShopSellMamo", ("MonsterShop", "SellMamo") },
            { "pictureBookLiveSpot1", ("PictureBook", "LiveSpot1") },
            { "pictureBookLiveSpot2", ("PictureBook", "LiveSpot2") },
            { "pictureBookSpecialDrop", ("PictureBook", "SpecialDrop") },
            { "rupeeRupeeValue", ("Rupee", "RupeeValue") },
            { "seriesArmorEnableCompBonus", ("SeriesArmor", "EnableCompBonus") },
            { "seriesArmorSeriesType", ("SeriesArmor", "SeriesType") },
            { "systemIsGetItemSelf", ("System", "IsGetItemSelf") },
            { "systemSameGroupActorName", ("System", "SameGroupActorName") },
            { "travelerAppearGameDataName", ("Traveler", "AppearGameDataName") },
            { "travelerDeleteGameDataName", ("Traveler", "DeleteGameDataName") },
            { "travelerRideHorseName", ("Traveler", "RideHorseName") },
            { "travelerRoutePoint0Name", ("Traveler", "RoutePoint0Name") },
            { "travelerRoutePoint1Name", ("Traveler", "RoutePoint1Name") },
            { "travelerRoutePoint2Name", ("Traveler", "RoutePoint2Name") },
            { "travelerRoutePoint3Name", ("Traveler", "RoutePoint3Name") },
            { "travelerRoutePoint4Name", ("Traveler", "RoutePoint4Name") },
            { "travelerRoutePoint5Name", ("Traveler", "RoutePoint5Name") },
            { "travelerRoutePoint6Name", ("Traveler", "RoutePoint6Name") },
            { "travelerRoutePoint7Name", ("Traveler", "RoutePoint7Name") },
            { "travelerRoutePoint8Name", ("Traveler", "RoutePoint8Name") },
            { "travelerRoutePoint9Name", ("Traveler", "RoutePoint9Name") },
            { "travelerRoutePoint10Name", ("Traveler", "RoutePoint10Name") },
            { "travelerRoutePoint11Name", ("Traveler", "RoutePoint11Name") },
            { "travelerRoutePoint12Name", ("Traveler", "RoutePoint12Name") },
            { "travelerRoutePoint13Name", ("Traveler", "RoutePoint13Name") },
            { "travelerRoutePoint14Name", ("Traveler", "RoutePoint14Name") },
            { "travelerRoutePoint15Name", ("Traveler", "RoutePoint15Name") },
            { "travelerRoutePoint16Name", ("Traveler", "RoutePoint16Name") },
            { "travelerRoutePoint17Name", ("Traveler", "RoutePoint17Name") },
            { "travelerRoutePoint18Name", ("Traveler", "RoutePoint18Name") },
            { "travelerRoutePoint19Name", ("Traveler", "RoutePoint19Name") },
            { "travelerRoutePoint20Name", ("Traveler", "RoutePoint20Name") },
            { "travelerRoutePoint21Name", ("Traveler", "RoutePoint21Name") },
            { "travelerRoutePoint22Name", ("Traveler", "RoutePoint22Name") },
            { "travelerRoutePoint23Name", ("Traveler", "RoutePoint23Name") },
            { "travelerRoutePoint24Name", ("Traveler", "RoutePoint24Name") },
            { "travelerRoutePoint25Name", ("Traveler", "RoutePoint25Name") },
            { "travelerRoutePoint26Name", ("Traveler", "RoutePoint26Name") },
            { "travelerRoutePoint27Name", ("Traveler", "RoutePoint27Name") },
            { "travelerRouteType", ("Traveler", "RouteType") },
            { "weaponCommonGuardPower", ("WeaponCommon", "GuardPower") },
            { "weaponCommonPoweredSharpAddAtkMax", ("WeaponCommon", "PoweredSharpAddAtkMax") },
            { "weaponCommonPoweredSharpAddAtkMin", ("WeaponCommon", "PoweredSharpAddAtkMin") },
            { "weaponCommonPoweredSharpAddLifeMax", ("WeaponCommon", "PoweredSharpAddLifeMax") },
            { "weaponCommonPoweredSharpAddLifeMin", ("WeaponCommon", "PoweredSharpAddLifeMin") },
            { "weaponCommonPoweredSharpAddRapidFireMax", ("WeaponCommon", "PoweredSharpAddRapidFireMax") },
            { "weaponCommonPoweredSharpAddRapidFireMin", ("WeaponCommon", "PoweredSharpAddRapidFireMin") },
            { "weaponCommonPoweredSharpAddSpreadFire", ("WeaponCommon", "PoweredSharpAddSpreadFire") },
            { "weaponCommonPoweredSharpAddSurfMaster", ("WeaponCommon", "PoweredSharpAddSurfMaster") },
            { "weaponCommonPoweredSharpAddThrowMax", ("WeaponCommon", "PoweredSharpAddThrowMax") },
            { "weaponCommonPoweredSharpAddThrowMin", ("WeaponCommon", "PoweredSharpAddThrowMin") },
            { "weaponCommonPoweredSharpAddZoomRapid", ("WeaponCommon", "PoweredSharpAddZoomRapid") },
            { "weaponCommonPoweredSharpWeaponAddGuardMax", ("WeaponCommon", "PoweredSharpWeaponAddGuardMax") },
            { "weaponCommonPoweredSharpWeaponAddGuardMin", ("WeaponCommon", "PoweredSharpWeaponAddGuardMin") },
            { "weaponCommonRank", ("WeaponCommon", "Rank") },
            { "weaponCommonSharpWeaponAddAtkMax", ("WeaponCommon", "SharpWeaponAddAtkMax") },
            { "weaponCommonSharpWeaponAddAtkMin", ("WeaponCommon", "SharpWeaponAddAtkMin") },
            { "weaponCommonSharpWeaponAddCrit", ("WeaponCommon", "SharpWeaponAddCrit") },
            { "weaponCommonSharpWeaponAddGuardMax", ("WeaponCommon", "SharpWeaponAddGuardMax") },
            { "weaponCommonSharpWeaponAddGuardMin", ("WeaponCommon", "SharpWeaponAddGuardMin") },
            { "weaponCommonSharpWeaponAddLifeMax", ("WeaponCommon", "SharpWeaponAddLifeMax") },
            { "weaponCommonSharpWeaponAddLifeMin", ("WeaponCommon", "SharpWeaponAddLifeMin") },
            { "weaponCommonSharpWeaponPer", ("WeaponCommon", "SharpWeaponPer") },
            { "weaponCommonStickDamage", ("WeaponCommon", "StickDamage") },
        };
        private static readonly Dictionary<string, (string, string)> LIFECONDITION_MAP = new()
        {
            { "invalidTimes", ("InvalidTimes", "Item{0:000}") },
            { "invalidWeathers", ("InvalidWeathers", "Item{0:000}") },
            { "traverseDist", ("DisplayDistance", "Item") },
            { "yLimitAlgorithm", ("YLimitAlgorithm", "Item") },
        };
        private static readonly Dictionary<string, (string, string)> MODELLIST_MAP = new()
        {
            { "cursorOffsetY", ("Attention", "CursorOffsetY") },
            { "variationMatAnim", ("ControllerInfo", "VariationMatAnim") },
            { "variationMatAnimFrame", ("ControllerInfo", "VariationMatAnimFrame") },
        };
        private static readonly Dictionary<string, (string, string)> RECIPE_MAP = new()
        {
            { "normal0ItemName01", ("Normal0", "ItemName01") },
            { "normal0ItemName02", ("Normal0", "ItemName02") },
            { "normal0ItemName03", ("Normal0", "ItemName03") },
            { "normal0ItemNum01", ("Normal0", "ItemNum01") },
            { "normal0ItemNum02", ("Normal0", "ItemNum02") },
            { "normal0ItemNum03", ("Normal0", "ItemNum03") },
            { "normal0StuffNum", ("Normal0", "ColumnNum") },
        };
        #endregion

        public ActorInfo() { }
        public ActorInfo(BymlNode infoNode)
        {
            foreach ((string key, BymlNode node) in infoNode.Hash)
            {
                if (key == "homeArea")
                {
                    homeArea = node.Array.Select(n => RetrieveStringHash(n)).ToArray();
                }
                else if (key == "locators")
                {
                    locators = node.Array.Select(n => RetrieveDynamicHash(n)).ToArray();
                }
                else if (key == "tags")
                {
                    tags = RetrieveIntHash(node);
                }
                else if (key == "terrainTextures")
                {
                    terrainTextures = node.Array.Select(n => n.UInt).ToArray();
                }
                else if (key == "farModelCulling" || key == "drops")
                {
                    farModelCulling = RetrieveDynamicHash(node);
                }
                else if (key.Contains("invalid"))
                {
                    typeof(ActorInfo).GetField(key, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, node.Array.Select(x => x.String).ToArray());
                }
                else
                {
                    typeof(ActorInfo).GetField(key, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, Retrieve(node));
                }
            }
        }

        public void Update()
        {
            name = actor!.Name;
            isHasFar = actor?.HasFar == false ? null : true; // coerce false to null
            UpdateFromActorLink();
            UpdateFromTags();
            UpdateFromDropTable();
            UpdateFromGParam();
            UpdateFromLifeCondition();
            UpdateFromModelList();
            UpdateFromPhysics();
            UpdateFromRecipe();
        }

        private void UpdateFromActorLink()
        {
            foreach ((string prop_name, string link) in INFO_LINKS)
            {
                string linkref = GetLink(link);
                if (linkref == "Dummy" || linkref == "1")
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
                else if (prop_name == "actorScale")
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, float.Parse(linkref));
                }
                else
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, linkref);
                }
            }
        }
        private void UpdateFromTags()
        {
            if (actor == null)
            {
                throw new InvalidDataException("Actor has not been set.");
            }
            tags = new();
            foreach (string tag in actor.Tags.Split(", "))
            {
                int v = (int)Crc32.Compute(tag);
                tags.Add($"tag{v:x8}", v);
            }
        }
        private void UpdateFromChemical()
        {
            if (GetLink("ChemicalUser") == "Dummy")
            {
                Chemical = null;
                return;
            }
            AampFile file = GetFile("ChemicalUser");
            Dictionary<string, int> chem = new();
            bool? cap = (int)file.RootNode.Lists("chemical_root")?.Lists("chemical_body")?
                .Objects("rigid_c_00")?.Params("attribute")?.Value! == 650;
            if (cap != null && (bool)cap)
            {
                chem["Capaciter"] = 1;
            }
            bool? burn = (string)file.RootNode.Lists("chemical_root")?.Lists("chemical_body")?
                .Objects("shape_00")?.Params("name")?.Value! == "WeaponFire";
            if (burn != null && (bool)burn)
            {
                chem["Burnable"] = 1;
            }
            Chemical = chem.Count > 0 ? chem : null;
        }
        private void UpdateFromDropTable()
        {
            if (GetLink("DropTableUser") == "Dummy")
            {
                drops = null;
                return;
            }
            AampFile file = GetFile("DropTableUser");
            Dictionary<string, string[]> normal_drops = new();
            foreach ((string obj_name, string param_name) in DROPTABLE_MAP)
            {
                ParamObject? obj = file.RootNode.Objects(obj_name);
                if (obj != null)
                {
                    normal_drops[obj_name] = obj.ParamEntries
                        .Where(e => e.HashString.Contains(param_name))
                        .Select(e => (string)Retrieve(e))
                        .ToArray();
                }
            }
            drops = normal_drops.Keys.Count > 0 ? normal_drops : null;
        }
        private void UpdateFromGParam()
        {
            if (GetLink("GParamUser") == "Dummy")
            {
                foreach (string prop_name in GPARAM_MAP.Keys)
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
                return;
            }
            AampFile file = GetFile("GParamUser");
            foreach ((string prop_name, (string obj_name, string param_name)) in GPARAM_MAP)
            {
                ParamObject? obj = file.RootNode.Objects(obj_name);
                if (obj != null)
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, Retrieve(obj.Params(param_name)!));
                }
                else
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
            }
        }
        private void UpdateFromLifeCondition()
        {
            if (GetLink("LifeConditionUser") == "Dummy")
            {
                foreach (string prop_name in LIFECONDITION_MAP.Keys)
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
                return;
            }
            AampFile file = GetFile("LifeConditionUser");
            foreach ((string prop_name, (string obj_name, string param_name)) in LIFECONDITION_MAP)
            {
                ParamObject? obj = file.RootNode.Objects(obj_name);
                if (obj != null)
                {
                    if (param_name.Contains('{'))
                    {
                        string[] strings = obj.ParamEntries.Select((e, i) => (string)Retrieve(e, i)).ToArray();
                        typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                            .SetValue(this, strings);
                    }
                    else
                    {
                        typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                            .SetValue(this, Retrieve(obj.Params(param_name)!));
                    }
                }
            }
        }
        private void UpdateFromModelList()
        {
            if (GetLink("ModelUser") == "Dummy")
            {
                foreach (string prop_name in MODELLIST_MAP.Keys)
                {
                    typeof(ActorInfo)
                        .GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
                bfres = null;
                addColorA = null;
                addColorB = null;
                addColorG = null;
                addColorR = null;
                baseScaleX = null;
                baseScaleY = null;
                baseScaleZ = null;
                farModelCulling = null;
                lookAtOffsetY = null;
                mainModel = null;
                return;
            }
            AampFile file = GetFile("ModelUser");
            ParamObject? obj;
            // Easy params
            foreach ((string prop_name, (string obj_name, string param_name)) in MODELLIST_MAP)
            {
                obj = file.RootNode.Objects(obj_name);
                if (obj != null)
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, Retrieve(obj.Params(param_name)!));
                }
            }
            if (IsDefault(variationMatAnim!))
            {
                variationMatAnim = null;
                variationMatAnimFrame = null;
            }
            // Hard params
            obj = file.RootNode.Objects("Attention");
            ParamEntry entry;
            if (obj != null)
            {
                entry = obj.Params("LookAtOffset")!;
                float offset = Retrieve(entry, prop: "Y");
                if (!0f.NearlyEquals(offset))
                {
                    lookAtOffsetY = offset;
                }
                else
                {
                    lookAtOffsetY = null;
                }
            }
            else
            {
                lookAtOffsetY = null;
            }
            obj = file.RootNode.Objects("ControllerInfo");
            if (obj != null)
            {
                entry = obj.Params("AddColor")!;
                Color4F col = Retrieve(entry);
                if (!0f.NearlyEquals(col.R + col.G + col.B + col.A))
                {
                    addColorA = col.A;
                    addColorB = col.B;
                    addColorG = col.G;
                    addColorR = col.R;
                }
                else
                {
                    addColorA = null;
                    addColorB = null;
                    addColorG = null;
                    addColorR = null;
                }
                entry = obj.Params("BaseScale")!;
                Vector3F vec = Retrieve(entry);
                if (!(1f.NearlyEquals(vec.X) && 1f.NearlyEquals(vec.Y) && 1f.NearlyEquals(vec.Z)))
                {
                    baseScaleX = vec.X;
                    baseScaleY = vec.Y;
                    baseScaleZ = vec.Z;
                }
                else
                {
                    baseScaleX = null;
                    baseScaleY = null;
                    baseScaleZ = null;
                }
                float[] fmc_floats = new float[5];
                vec = Retrieve(obj.Params("FarModelCullingCenter")!);
                fmc_floats[0] = vec.X;
                fmc_floats[1] = vec.Y;
                fmc_floats[2] = vec.Z;
                fmc_floats[3] = Retrieve(obj.Params("FarModelCullingHeight")!);
                fmc_floats[4] = Retrieve(obj.Params("FarModelCullingRadius")!);
                if (fmc_floats.Any(f => !0f.NearlyEquals(f)))
                {
                    farModelCulling = new()
                    {
                        ["center"] = new Dictionary<string, float>
                        {
                            { "X", fmc_floats[0] },
                            { "Y", fmc_floats[1] },
                            { "Z", fmc_floats[2] },
                        },
                        ["height"] = fmc_floats[3],
                        ["radius"] = fmc_floats[4]
                    };
                }
                else
                {
                    farModelCulling = null;
                }
            }
            else
            {
                addColorA = null;
                addColorB = null;
                addColorG = null;
                addColorR = null;
                baseScaleX = null;
                baseScaleY = null;
                baseScaleZ = null;
                farModelCulling = null;
            }
            bfres = Retrieve(file.RootNode
                .Lists("ModelData")!
                .Lists("ModelData_0")!
                .Objects("Base")!
                .Params("Folder")!
                );
            mainModel = Retrieve(file.RootNode
                .Lists("ModelData")!
                .Lists("ModelData_0")!
                .Lists("Unit")!
                .Objects("Unit_0")!
                .Params("UnitName")!
                );
        }
        private void UpdateFromPhysics()
        {
            if (GetLink("PhysicsUser") == "Dummy")
            {
                rigidBodyCenterY = null;
                return;
            }
            AampFile file = GetFile("PhysicsUser");
            ParamEntry? entry = file.RootNode
                .Lists("ParamSet")?
                .Lists("RigidBodySet")?
                .Lists("RigidBodySet_0")?
                .Lists("RigidBody_0")?
                .Objects(948250248)?
                .Params("center_of_mass");
            if (entry != null)
            {
                float y_val = Retrieve(entry, prop: "Y");
                if (!0f.NearlyEquals(y_val))
                {
                    rigidBodyCenterY = y_val;
                }
                else
                {
                    rigidBodyCenterY = null;
                }
            }
            else
            {
                rigidBodyCenterY = null;
            }
        }
        private void UpdateFromRecipe()
        {
            if (GetLink("RecipeUser") == "Dummy")
            {
                foreach (string prop_name in RECIPE_MAP.Keys)
                {
                    typeof(ActorInfo)
                        .GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, null);
                }
                return;
            }
            AampFile file = GetFile("RecipeUser");
            ParamObject? obj;
            foreach ((string prop_name, (string obj_name, string param_name)) in RECIPE_MAP)
            {
                obj = file.RootNode.Objects(obj_name);
                if (obj != null)
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, Retrieve(obj.Params(param_name)!));
                }
            }
        }

        private string GetLink(string link) => actor!.GetLink(link);
        private AampFile GetFile(string link) => actor!.GetPackAampFile(link);

        private static dynamic Retrieve(ParamEntry entry, int index = -1, string prop = "\0")
        {
            return entry.ParamType switch
            {
                ParamType.Boolean => (bool)entry.Value,
                ParamType.Float => (float)entry.Value,
                ParamType.Int => (int)entry.Value,
                ParamType.Vector2F => prop == "\0" ? (Vector2F)entry.Value! : typeof(Vector2F).GetField(prop)?.GetValue((Vector2F)entry.Value)!,
                ParamType.Vector3F => prop == "\0" ? (Vector3F)entry.Value! : typeof(Vector3F).GetField(prop)?.GetValue((Vector3F)entry.Value)!,
                ParamType.Vector4F => prop == "\0" ? (Vector4F)entry.Value! : typeof(Vector4F).GetField(prop)?.GetValue((Vector4F)entry.Value)!,
                ParamType.Color4F => prop == "\0" ? (Color4F)entry.Value! : typeof(Color4F).GetField(prop)?.GetValue((Color4F)entry.Value)!,
                ParamType.String32 => ((StringEntry)entry.Value).ToString(),
                ParamType.String64 => ((StringEntry)entry.Value).ToString(),
                ParamType.Curve1 => index == -1 ? (Curve[])entry.Value : ((Curve[])entry.Value)[index],
                ParamType.Curve2 => index == -1 ? (Curve[])entry.Value : ((Curve[])entry.Value)[index],
                ParamType.Curve3 => index == -1 ? (Curve[])entry.Value : ((Curve[])entry.Value)[index],
                ParamType.Curve4 => index == -1 ? (Curve[])entry.Value : ((Curve[])entry.Value)[index],
                ParamType.BufferInt => index == -1 ? (int[])entry.Value : ((int[])entry.Value)[index],
                ParamType.BufferFloat => index == -1 ? (float[])entry.Value : ((float[])entry.Value)[index],
                ParamType.String256 => ((StringEntry)entry.Value).ToString(),
                ParamType.Quat => index == -1 ? (float[])entry.Value : ((float[])entry.Value)[index],
                ParamType.Uint => (uint)entry.Value,
                ParamType.BufferUint => index == -1 ? (uint[])entry.Value : ((uint[])entry.Value)[index],
                ParamType.BufferBinary => index == -1 ? (byte[])entry.Value : ((byte[])entry.Value)[index],
                ParamType.StringRef => ((StringEntry)entry.Value).ToString(),
                _ => throw new ArgumentException("IMPOSSIBRU!"),
            };
        }

        private static dynamic Retrieve(BymlNode node)
        {
            return node.Type switch
            {
                NodeType.String => node.String,
                NodeType.Binary => node.Binary,
                NodeType.Bool => node.Bool,
                NodeType.Int => node.Int,
                NodeType.Float => node.Float,
                NodeType.UInt => node.UInt,
                NodeType.Int64 => node.Int64,
                NodeType.UInt64 => node.UInt64,
                NodeType.Double => node.Double,
                NodeType.Array => node.Array.Select(x => Retrieve(x)).ToArray(),
                NodeType.Hash => RetrieveHash(node),
                _ => throw new ArgumentException($"Invalid node type {node.Type}"),
            };
        }
        private static dynamic RetrieveHash(BymlNode node)
        {
            NodeType type = node.Hash.First().Value.Type;
            return type switch
            {
                NodeType.String => RetrieveStringHash(node),
                NodeType.Int => RetrieveIntHash(node),
                NodeType.Float => RetrieveFloatHash(node),
                NodeType.UInt => RetrieveUIntHash(node),
                NodeType.Array => RetrieveArrayStringHash(node),
                _ => throw new InvalidDataException($"Bad hash child node type {type}"),
            };
        }
        private static Dictionary<string, string> RetrieveStringHash(BymlNode node)
        {
            Dictionary<string, string> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.String;
            }
            return hash;
        }
        private static Dictionary<string, int> RetrieveIntHash(BymlNode node)
        {
            Dictionary<string, int> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.Int;
            }
            return hash;
        }
        private static Dictionary<string, float> RetrieveFloatHash(BymlNode node)
        {
            Dictionary<string, float> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.Float;
            }
            return hash;
        }
        private static Dictionary<string, uint> RetrieveUIntHash(BymlNode node)
        {
            Dictionary<string, uint> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.UInt;
            }
            return hash;
        }
        private static Dictionary<string, string[]> RetrieveArrayStringHash(BymlNode node)
        {
            Dictionary<string, string[]> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.Array.Select(x => x.String).ToArray();
            }
            return hash;
        }
        private static Dictionary<string, dynamic> RetrieveDynamicHash(BymlNode node)
        {
            Dictionary<string, dynamic> hash = new();
            foreach ((string key, BymlNode child) in node.Hash)
            {
                hash[key] = child.Type switch
                {
                    NodeType.Int => child.Int,
                    NodeType.Float => child.Float,
                    NodeType.String => child.String,
                    NodeType.Hash => RetrieveFloatHash(child),
                    NodeType.Array => Retrieve(child),
                    _ => throw new InvalidDataException("locators children only contain string and float"),
                };
            }
            return hash;
        }

        private static bool IsDefault(dynamic val)
        {
            return val switch
            {
                string v => v == "''",
                _ => throw new ArgumentException($"ActorInfo.IsDefault - Handle {val.GetType()}"),
            };
        }

        public static void SetActorInfoFile(BymlFile file) => actor_info_file = file;
        public static void ClearActorInfoFile() => actor_info_file = null;
        public void Apply()
        {
            SortedDictionary<string, BymlNode> ActorInfo = actor_info_file!.RootNode.Hash;
            uint hash = Crc32.Compute(name);

            int index = ActorInfo["Hashes"].Array.FindIndex(h => h.UInt == hash);
            if (index < 0)
            {
                BymlNode node = hash < 0x80000000 ? new BymlNode((int)hash) : new BymlNode(hash);
                ActorInfo["Hashes"].Array.Add(node);
                ActorInfo["Hashes"].Array.Sort((a, b) => (int)unchecked(a.UInt - b.UInt));
                ActorInfo["Actors"].Array.Insert(ActorInfo["Hashes"].Array.IndexOf(node), GetInfoByml());
            }
            else
            {
                ActorInfo["Actors"].Array[index] = GetInfoByml();
            }
        }
        public static void Write(string modRoot)
        {
            string actor_info_path = $"{modRoot}/Actor/ActorInfo.product.sbyml";
            File.WriteAllBytes(actor_info_path, Yaz0.Compress(actor_info_file!.ToBinary()));
        }
        public BymlNode GetInfoByml() => new(new Dictionary<string, BymlNode>(typeof(ActorInfo)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetValue(this) != null && f.FieldType != typeof(Actor) && f.FieldType != typeof(FarActor))
                .Select(f =>
                    {
                        BymlNode node;
                        if (f.Name == "tags")
                        {
                            node = CompileTags((f.GetValue(this) as Dictionary<string, int>)!);
                        }
                        else if (f.Name == "terrainTextures")
                        {
                            node = CompileHashes((f.GetValue(this) as uint[])!);
                        }
                        else
                        {
                            node = CompileNode(f.GetValue(this));
                        }
                        return new KeyValuePair<string, BymlNode>(f.Name, node);
                    }
                )
            ));
        private BymlNode CompileNode<T>(T prop)
        {
            return prop switch
            {
                string str => new BymlNode(str),
                bool b => new BymlNode(b),
                int i => new BymlNode(i),
                uint u => new BymlNode(u),
                long l => new BymlNode(l),
                ulong ul => new BymlNode(ul),
                float f => new BymlNode(f),
                double d => new BymlNode(d),
                string[] strs => new BymlNode(strs.Select(s => CompileNode(s)).ToList()),
                bool[] bools => new BymlNode(bools.Select(b => CompileNode(b)).ToList()),
                int[] ints => new BymlNode(ints.Select(i => CompileNode(i)).ToList()),
                uint[] uints => new BymlNode(uints.Select(u => CompileNode(u)).ToList()),
                long[] longs => new BymlNode(longs.Select(l => CompileNode(l)).ToList()),
                ulong[] ulongs => new BymlNode(ulongs.Select(ul => CompileNode(ul)).ToList()),
                float[] floats => new BymlNode(floats.Select(f => CompileNode(f)).ToList()),
                double[] doubles => new BymlNode(doubles.Select(d => CompileNode(d)).ToList()),
                Dictionary<string, string> dict => CompileDict(dict),
                Dictionary<string, bool> dict => CompileDict(dict),
                Dictionary<string, int> dict => CompileDict(dict),
                Dictionary<string, uint> dict => CompileDict(dict),
                Dictionary<string, long> dict => CompileDict(dict),
                Dictionary<string, ulong> dict => CompileDict(dict),
                Dictionary<string, float> dict => CompileDict(dict),
                Dictionary<string, double> dict => CompileDict(dict),
                Dictionary<string, string[]> dict => CompileDict(dict),
                Dictionary<string, bool[]> dict => CompileDict(dict),
                Dictionary<string, int[]> dict => CompileDict(dict),
                Dictionary<string, uint[]> dict => CompileDict(dict),
                Dictionary<string, long[]> dict => CompileDict(dict),
                Dictionary<string, ulong[]> dict => CompileDict(dict),
                Dictionary<string, float[]> dict => CompileDict(dict),
                Dictionary<string, double[]> dict => CompileDict(dict),
                Dictionary<string, string>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, bool>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, int>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, uint>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, long>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, ulong>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, float>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, double>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                Dictionary<string, dynamic> dict => CompileDict(dict),
                Dictionary<string, dynamic>[] dicts => new BymlNode(dicts.Select(d => CompileDict(d)).ToList()),
                _ => throw new ArgumentException($"Unexpected property type: {prop!.GetType()} {prop}"),
            };
        }
        private BymlNode CompileDict<T>(Dictionary<string, T> dict)
        {
            Dictionary<string, BymlNode> hash = new();
            foreach ((string key, T val) in dict)
            {
                BymlNode node = CompileNode(val);
                hash.Add(key, node);
            }
            return new(hash);
        }
        private static BymlNode CompileTags(Dictionary<string, int> prop)
        {
            Dictionary<string, BymlNode> hash = new();
            foreach ((string key, int value) in prop)
            {
                hash.Add(
                    key,
                    value < 0 ? new BymlNode((uint)value) : new BymlNode(value)
                );
            }
            return new(hash);
        }
        private static BymlNode CompileHashes(uint[] hashes)
        {
            return new(hashes.Select(h => h >= 0x80000000 ? new BymlNode(h) : new BymlNode((int)h)).ToList());
        }
    }
}
