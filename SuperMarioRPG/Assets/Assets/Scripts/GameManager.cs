using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMEMBER
{
    MARIO,
    MELLOW
}

public enum ECLOTHES
{
    HAMMER,
    SHIRT,
    PANTS
}

public enum EITEMS
{
    MUSHROOM, // HP 30
    HONEY_SYRUP, // FP 10
}

public enum ETOADASSIST
{
    ROCK_CANDY, // All of them, Damage 200
    FLOWER_ESSENCE, // Restores FP
    POWER_MUSHROOM // Restores HP
}

public struct Stats
{
    public int m_attak { get; set; }
    public int m_defense { get; set; }
    public int m_magicAttack { get; set; }
    public int m_magicDefense { get; set; }

    public Stats(int _attack, int _defense, int _magicAttack, int _magicDefense)
    {
        m_attak = _attack;
        m_defense = _defense;
        m_magicAttack = _magicAttack;
        m_magicDefense = _magicDefense;
    }

    public int this[int index]
    {
        get
        {
            if (index == 0)
                return m_attak;
            else if (index == 1)
                return m_defense;
            else if (index == 2)
                return m_magicAttack;
            else if (index == 3)
                return m_magicDefense;
            else
                return 0;
        }
        set
        {
            if (index == 0)
                m_attak = value;
            else if (index == 1)
                m_defense = value;
            else if (index == 2)
                m_magicAttack = value;
            else if (index == 3)
                m_magicDefense = value;

        }
    }
}

public struct Clothes
{
    public int m_attak { get; set; }
    public int m_defense { get; set; }
    public int m_magicAttack { get; set; }
    public int m_magicDefense { get; set; }
    public EMEMBER m_whoCanUse { get; set; }
    public int m_num { get; set; }
    public bool m_isTake { get; set; }

    public Clothes(int _attack, int _defense, int _magicAttack, int _magicDefense, EMEMBER _whoCanUse, int _num, bool _isTake)
    {
        m_attak = _attack;
        m_defense = _defense;
        m_magicAttack = _magicAttack;
        m_magicDefense = _magicDefense;
        m_whoCanUse = _whoCanUse;
        m_num = _num;
        m_isTake = _isTake;
    }

    public int this[int index]
    {
        get
        {
            if (index == 0)
                return m_attak;
            else if (index == 1)
                return m_defense;
            else if (index == 2)
                return m_magicAttack;
            else if (index == 3)
                return m_magicDefense;
            else if (index == 4)
                return (int)m_whoCanUse;
            else if (index == 5)
                return m_num;
            else
                return 0;
        }
        set
        {
            if (index == 0)
                m_attak = value;
            else if (index == 1)
                m_defense = value;
            else if (index == 2)
                m_magicAttack = value;
            else if (index == 3)
               m_magicDefense = value;
            else if (index == 4)
                m_whoCanUse = (EMEMBER)value;
            else if (index == 5)
                m_num = value;
        }
    }
}

public struct MonStats
{
    public readonly int m_hp;
    public readonly int m_atk;
    public readonly int m_exp;
    public readonly int m_coin;

    public MonStats(int _hp, int _atk, int _exp, int _coin)
    {
        m_hp = _hp;
        m_atk = _atk;
        m_exp = _exp;
        m_coin = _coin;
    }
}

public struct btlMosterInfo
{
    public GameObject m_monsterPrefeb { get; set; }
    public float m_monsterPosY { get; set; }

    public btlMosterInfo(GameObject _mPrefeb, float _mPosY)
    {
        m_monsterPrefeb = _mPrefeb;
        m_monsterPosY = _mPosY;
    }
}

public struct magicAttackInfo
{
    public string m_name { get; set; }
    public int m_cost { get; set; }
    public int m_attack { get; set; }

    public magicAttackInfo(string _name, int _cost, int _attack)
    {
        m_name = _name;
        m_cost = _cost;
        m_attack = _attack;
    }
}

public class GameManager : MonoBehaviour
{
    public class CharacterInfo
    {
        public string m_name { get; set; }
        public int m_level { get; set; }
        public int m_maxhp { get; set; }
        public int m_curhp { get; set; }
        public int m_exp { get; set; }
        public int m_leftExp { get; set; }
        public int m_curAttak { get; set; } // 0: default, 1: Hammer(Mario)
        public Stats m_stat { get; set; }
        public int m_maIndex { get; set; }
        public int m_isDead { get; set; }
        public string m_weapon { get; set; }
        public string m_clothes { get; set; }

        public magicAttackInfo[] m_magicAttack { get; set; }

        public CharacterInfo(string _name, int _level, int _maxhp, int _curhp, int _exp, int _leftExp, int _curAttak, Stats _stats, magicAttackInfo[] _maInfo, int _maIndex, int _isDead, string _weapon, string _clothes)
        {
            m_name = _name;
            m_level = _level;
            m_maxhp = _maxhp;
            m_curhp = _curhp;
            m_exp = _exp;
            m_leftExp = _leftExp;
            m_curAttak = _curAttak;
            m_stat = _stats;
            m_magicAttack = _maInfo;
            m_maIndex = _maIndex;
            m_isDead = _isDead; // 0: alive, 1: dead
            m_weapon = _weapon;
            m_clothes = _clothes;
        }
    }

    public static GameManager instance; // GameManager

    public int memberNum { get; } = 2; // Mario, Mellow
    public int itemNum { get; } = 2; // mushroom, honey syrup

    // About Stage Name
    public string curStage { get; set; } = "머시로드";
    public Dictionary<string, string> stageName { get; set; }

    public Dictionary<int, int> m_items { get; set; } // (item, num)
    public int[] EXPNeeded { get; set; } = { 16, 48, 84, 126, 224 };
    public CharacterInfo[] members { get; set; } // members(Mario, Mellow) Info Array
    public int curFP { get; set; } = 10;
    public int maxFP { get; set; } = 10;
    public int gauge{ get; set; } = 0;
    public int coin { get; set; } = 0;

    public EMEMBER memberIndex { get; set; } = EMEMBER.MARIO;
    
    public string beforeSceneName { get; set; } = "first";

    public string btlMoster { get; set; } = "goomba";

    public bool isNowBattle { get; set; } = false;

    // For Monster Appear in Battle
    public GameObject goomba = null;
    public GameObject nokonoko = null;
    public GameObject spikey = null;
    public GameObject hidog = null;
    public GameObject shyguy = null;
    public GameObject HammerBro = null;

    // For Monster Appear in Battle
    public Dictionary<string, btlMosterInfo> curMoster { get; set; }

    // Current Mario Position
    public Vector3 curMarioPos { get; set; } = new Vector3(-10f, 11.2f, 21f);

    // Check Monster Active
    public List<string> monsterAtv { get; set; }

    // !!!! NEED SET DETAIL ARRAY SIZE - IN OUT NUM !!!!
    public Vector3[] InOutPos = new Vector3[8];

    // Check Scene Change for Mario Pos
    public Dictionary<string, int> chgStage { get; set; }

    // For Monster Info In Battle
    public Dictionary<string, MonStats> monStats { get; set; }

    // For One Time Event Check
    public bool isWithMellow { get; set; } = false;

    // Info about Clothes & Weapon (EClothes, EMember)
    public Clothes[] validItem { get; set; } = new Clothes[3];

    // Boss Info
    public bool isHammerBroDead { get; set; } = false;
    public int isHammerBroEvent { get; set; } = 0;
    public bool isBossBtl { get; set; } = false;

    // menu
    public bool isMenu { get; set; } = false;

    // Audio
    public bool isToucehd { get; set; } = false;
    public bool isWin { get; set; } = false;
    public bool isLoose { get; set; } = false;

    void Awake()
    {
        // Checking Instance Num for SINGLETON
        if(instance == null)
        {
            instance = this;
        }
        else // !Warning: Two GameManager Exist
        {
            Destroy(gameObject);
        }

        // FOR SINGLETON
        DontDestroyOnLoad(gameObject);

        members = new CharacterInfo[memberNum];

        // Make Mario Info
        Stats marioInitStat = new Stats(7, 0, 5, 2);
        magicAttackInfo[] marioMagicAttack =
        {
            new magicAttackInfo("점프", 3, 8),
            new magicAttackInfo("파이어볼", 5, 20)
        };

        members[(int)EMEMBER.MARIO] = new CharacterInfo("마리오", 1, 20, 20, 0, EXPNeeded[0], 0, marioInitStat, marioMagicAttack, 0, 0, "", "");

        // Make Mellow Info
        Stats mellowInitStat = new Stats(9, 3, 13, 10);
        magicAttackInfo[] mellowMagicAttack =
        {
            new magicAttackInfo("번개찌리릿", 2, 10),
            new magicAttackInfo("무슨 생각하니", 1, 0)
        };

        members[(int)EMEMBER.MELLOW] = new CharacterInfo("멜로", 2, 20, 20, 0, EXPNeeded[1], 0, mellowInitStat, mellowMagicAttack, 0, 0, "", "");

        // Set InOutPosIndex
        chgStage = new Dictionary<string, int>();
        chgStage.Add("first", 0);
        chgStage.Add("d02out", 0);
        chgStage.Add("d02out3", 2);
        chgStage.Add("m01out", 4);
        chgStage.Add("d02out2", 6);

        // Check Monster Active
        monsterAtv = new List<string>();

        // Set In Out Pos
        InOutPos[0] = new Vector3(-10f, 11.2f, 21f); // d2out - In
        InOutPos[1] = new Vector3(-11f, 11.2f, 3f); // d2out - Out
        InOutPos[2] = new Vector3(-5f, 11.2f, 19.90001f); // d2out3 - In
        InOutPos[3] = new Vector3(-33.7f, 11.2f, -2f); // d2out3 - Out
        InOutPos[4] = new Vector3(-12f, 11.6f, 23f); // m01out - In
        InOutPos[5] = new Vector3(-42f, 11.7f, 15f); // m01out - Out
        InOutPos[6] = new Vector3(-20f, 9.8f, 30f); // d2out2 - In

        // For Appear Monster in Battle
        curMoster = new Dictionary<string, btlMosterInfo>();
        curMoster["goomba"] = new btlMosterInfo(goomba, 0f);
        curMoster["nokonoko"] = new btlMosterInfo(nokonoko, 0f);
        curMoster["spikey"] = new btlMosterInfo(spikey, 0f);
        curMoster["hidog"] = new btlMosterInfo(hidog, 0f);
        curMoster["shyguy"] = new btlMosterInfo(shyguy, 0f);
        curMoster["hammerBro"] = new btlMosterInfo(HammerBro, 0f);

        // Monster Info in Battle
        monStats = new Dictionary<string, MonStats>();
        monStats["goomba"] = new MonStats(11, 3, 5, 3);
        monStats["nokonoko"] = new MonStats(12, 4, 5, 3);
        monStats["spikey"] = new MonStats(15, 5, 7, 4);
        monStats["hidog"] = new MonStats(15, 6, 7, 6);
        monStats["shyguy"] = new MonStats(20, 6, 13, 6);
        monStats["hammerBro"] = new MonStats(50, 3, 7, 30);

        // items (0: mushroom, 1: honey syrup)
        m_items = new Dictionary<int, int>(); // item index, count
        m_items[(int)EITEMS.MUSHROOM] = 1; // !!!FOR DEDUG!!!!!!!!!!! origin 0
        m_items[(int)EITEMS.HONEY_SYRUP] = 1;// !!!FOR DEDUG!!!!!!!!!!! origin 0

        // Clothes & Weapon
        validItem[(int)ECLOTHES.HAMMER] = new Clothes(10, 0, 0, 0, EMEMBER.MARIO, 0, false);
        validItem[(int)ECLOTHES.SHIRT] = new Clothes(0, 6, 0, 6, EMEMBER.MARIO, 0, false);
        validItem[(int)ECLOTHES.PANTS] = new Clothes(0, 6, 0, 3, EMEMBER.MELLOW, 0, false);

        // Stage Name
        stageName = new Dictionary<string, string>();
        stageName["d02out"] = "머시 로드";
        stageName["d02out3"] = "머시 로드";
        stageName["m01out"] = "버섯 성";
        stageName["m01i1f1"] = "버섯 성";
        stageName["d02out2"] = "시프 로드";
        stageName["btld02a"] = "머시 로드";
        stageName["btld02b"] = "시프 로드";
        stageName["btld02a_Boss"] = "머시 로드";
    }
}
