#pragma warning disable CS0169, CS0649, IDE0044
// Reflection - the fields are assigned to/used, just not in a way the compiler can verify

/*
 * Abandon hope, all ye who enter here
 */

using Byml.Security.Cryptography;
using Nintendo.Aamp;
using Syroot.Maths;
using System.Reflection;

namespace BotwActorTool.Lib.Info
{
    internal class ActorInfo
    {
        private Actor? actor;
        private FarActor? far;

        #region ActorInfo Params
        private Dictionary<string, int>? Chemical;
        private float? aabbMax;
        private float? aabbMin;
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
        public int? instSize { get; set; }
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
        private string[]? normal0ItemNames;
        private int[]? normal0ItemNums;
        private int? normal0ItemStuffNum;
        private int? pictureBookLiveSpot1;
        private int? pictureBookLiveSpot2;
        private int? pictureBookSpecialDrop;
        private string? profile;
        private float? rigidBodyCenterY;
        private int? rupeeRupeeValue;
        private bool? seriesArmorEnableCompBonus;
        private string? seriesArmorSeriesType;
        private string? slink;
        public int? sortKey { get; set; }
        private bool? systemIsGetItemSelf;
        private string? systemSameGroupActorName;
        private Dictionary<string, dynamic>? tags;
        private string[]? terrainTextures;
        private string? travelerAppearGameDataName;
        private string? travelerDeleteGameDataName;
        private string? travelerRideHorseName;
        private string[]? travelerRoutePointNames;
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
            { "travelerRoutePointNames", ("Traveler", "RoutePoint{0}Name") },
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
            { "normal0ItemNames", ("Normal0", "ItemName") },
            { "normal0ItemNums", ("Normal0", "ItemNum") },
            { "normal0StuffNum", ("Normal0", "ColumnNum") },
        };
        #endregion

        public ActorInfo(Actor actor)
        {
            this.actor = actor;
        }
        public ActorInfo(FarActor far)
        {
            this.far = far;
        }

        public void Update()
        {
            name = actor?.Name ?? far?.Name ?? throw new Exception("No Actor for this Info?");
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
                if (linkref == "Dummy" || linkref == "1.0")
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
            string tags = actor?.Tags ?? far?.Tags ?? throw new Exception("No Actor for this Info?");
            this.tags = new();
            foreach (string tag in tags.Split(", "))
            {
                dynamic v = Crc32.Compute(tag);
                v = v >= 0x80000000 ? v : (int)v;
                this.tags.Add(string.Format("tag{0:00000000X}", Crc32.Compute(tag)), v);
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
            if (chem.Count > 0)
            {
                Chemical = chem;
            }
            else
            {
                Chemical = null;
            }
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
                if (obj == null)
                {
                    continue;
                }
                normal_drops[obj_name] = obj.ParamEntries
                    .Where(e => e.HashString.Contains(param_name))
                    .Select(e => (string)Retrieve(e))
                    .ToArray();
            }
            if (normal_drops.Keys.Count > 0)
            {
                drops = normal_drops;
            }
            else
            {
                drops = null;
            }
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
                if (obj == null)
                {
                    continue;
                }
                if (param_name.Contains('{'))
                {
                    string[] points = new string[28];
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i] = Retrieve(obj.Params(string.Format(param_name, i))!);
                    }
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, points.Where(s => !IsDefault(s)).ToArray());
                }
                else
                {
                    typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(this, Retrieve(obj.Params(param_name)!));
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
                if (obj == null)
                {
                    continue;
                }
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
                if (obj == null)
                {
                    continue;
                }
                typeof(ActorInfo).GetField(prop_name, BindingFlags.NonPublic | BindingFlags.Instance)!
                    .SetValue(this, Retrieve(obj.Params(param_name)!));
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
                Vector3F vec = Retrieve(entry);
                if (!0f.NearlyEquals(vec.Y))
                {
                    lookAtOffsetY = vec.Y;
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
                rigidBodyCenterY = Retrieve(entry);
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
            normal0ItemNames = file.RootNode
                .Objects(RECIPE_MAP["normal0ItemNames"].Item1)?
                .ParamEntries
                .Where(e => e.HashString.Contains(RECIPE_MAP["normal0ItemNames"].Item2))
                .Select((e, i) => (string)Retrieve(e, i))
                .ToArray();
            normal0ItemNums = file.RootNode
                .Objects(RECIPE_MAP["normal0ItemNums"].Item1)?
                .ParamEntries
                .Where(e => e.HashString.Contains(RECIPE_MAP["normal0ItemNums"].Item2))
                .Select((e, i) => (int)Retrieve(e, i))
                .ToArray();
            ParamEntry? col_num = file.RootNode
                    .Objects(RECIPE_MAP["normal0StuffNum"].Item1)?
                    .Params(RECIPE_MAP["normal0StuffNum"].Item2);
            if (col_num != null)
            {
                normal0ItemStuffNum = Retrieve(col_num);
            }
            else
            {
                normal0ItemStuffNum = null;
            }
        }

        private string GetLink(string link)
        {
            string linkref;
            if (actor != null)
            {
                linkref = actor.GetLink(link);
            }
            else
            {
                linkref = far!.GetLink(link);
            }
            return linkref;
        }
        private AampFile GetFile(string link)
        {
            AampFile file;
            if (actor != null)
            {
                file = actor.GetPackAampFile(link);
            }
            else
            {
                file = far!.GetPackAampFile(link);
            }
            return file;
        }

        private static dynamic Retrieve(ParamEntry entry, int index = 0, string prop = "\0")
        {
            return entry.ParamType switch
            {
                ParamType.Boolean => (bool)entry.Value!,
                ParamType.Float => (float)entry.Value!,
                ParamType.Int => (int)entry.Value!,
                ParamType.Vector2F => ((Vector2F)entry.Value!)[index],
                ParamType.Vector3F => ((Vector3F)entry.Value!)[index],
                ParamType.Vector4F => ((Vector4F)entry.Value!)[index],
                ParamType.Color4F => typeof(Color4F).GetProperty(prop)?.GetValue((Color4F)entry.Value!)!,
                ParamType.String32 => ((StringEntry)entry.Value!).ToString(),
                ParamType.String64 => ((StringEntry)entry.Value!).ToString(),
                ParamType.Curve1 => (Curve[])entry.Value!,
                ParamType.Curve2 => (Curve[])entry.Value!,
                ParamType.Curve3 => (Curve[])entry.Value!,
                ParamType.Curve4 => (Curve[])entry.Value!,
                ParamType.BufferInt => (int[])entry.Value!,
                ParamType.BufferFloat => (float[])entry.Value!,
                ParamType.String256 => ((StringEntry)entry.Value!).ToString(),
                ParamType.Quat => ((float[])entry.Value!)[index],
                ParamType.Uint => (uint)entry.Value!,
                ParamType.BufferUint => (uint[])entry.Value!,
                ParamType.BufferBinary => (byte[])entry.Value!,
                ParamType.StringRef => ((StringEntry)entry.Value!).ToString(),
                _ => throw new ArgumentException("IMPOSSIBRU!"),
            };
        }

        private static bool IsDefault(dynamic val)
        {
            return val switch
            {
                string v => v == "''",
                _ => throw new ArgumentException($"ActorInfo.IsDefault - Handle {val.GetType()}"),
            };
        }
    }
}
