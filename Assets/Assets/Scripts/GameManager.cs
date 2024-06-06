using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMEMBER
{
    MARIO,
    MELLOW
}

public enum EWEAPON
{
    HAMMER,
    FROGGIE_STICK
}

public enum ECLOTHES
{
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

        public magicAttackInfo[] m_magicAttack { get; set; }
        public Dictionary<int, Stats> m_clothes { get; set; } = new Dictionary<int, Stats>();
        public Dictionary<int, Stats> m_weapon { get; set; } = new Dictionary<int, Stats>();

        public CharacterInfo(string _name, int _level, int _maxhp, int _curhp, int _exp, int _leftExp, int _curAttak, Stats _stats, magicAttackInfo[] _maInfo, int _maIndex, int _isDead)
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
        }
    }

    public static GameManager instance; // GameManager

    public int memberNum { get; } = 2; // Mario, Mellow
    public int itemNum { get; } = 2; // mushroom, honey syrup

    public Dictionary<int, int> m_items { get; set; } // (item, num)
    public int[] EXPNeeded { get; set; } = { 16, 48, 84, 126 };
    public CharacterInfo[] members { get; set; } // members(Mario, Mellow) Info Array
    public int curFP { get; set; } = 10;
    public int maxFP { get; set; } = 10;
    public int gauge{ get; set; } = 0;
    public int coin { get; set; } = 0;
    public string curStage { get; set; } = "MushRoad";
    public string[] memBonus { get; set; }
    // !!!!!!!!!!F OR DEBUG CHANGE MELLOW
    //public EMEMBER memberIndex { get; set; } = EMEMBER.MELLOW;
    public EMEMBER memberIndex { get; set; } = EMEMBER.MARIO;
    
    // !!!!!!!!!!!!!FOR DEBUG origin is first !!!!!!!!!!!!!!!
    public string beforeSceneName { get; set; } = "d02out";
    public string btlMoster { get; set; } = "goomba";
    // !!!!!!! FOR DEBUG CHANGE TURE
    public bool isNowBattle { get; set; } = true;

    // For Monster Appear in Battle
    public GameObject goomba = null;
    public GameObject nokonoko = null;
    public GameObject spikey = null;
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
        Stats marioInitStat = new Stats(20, 0, 10, 2);
        magicAttackInfo[] marioMagicAttack =
        {
            new magicAttackInfo("점프", 3, 25),
            new magicAttackInfo("파이어볼", 5, 20)
        };

        members[(int)EMEMBER.MARIO] = new CharacterInfo("마리오", 1, 20, 20, 0, EXPNeeded[0], 0, marioInitStat, marioMagicAttack, 0, 0);

        // Make Mellow Info
        Stats mellowInitStat = new Stats(22, 3, 15, 10);
        magicAttackInfo[] mellowMagicAttack =
        {
            new magicAttackInfo("번개찌리릿", 2, 15),
            new magicAttackInfo("무슨 생각하니", 1, 0)
        };

        members[(int)EMEMBER.MELLOW] = new CharacterInfo("멜로", 2, 20, 20, 0, EXPNeeded[1], 0, mellowInitStat, mellowMagicAttack, 0, 0);

        // Set InOutPosIndex
        chgStage = new Dictionary<string, int>();
        chgStage.Add("first", 0);
        chgStage.Add("d02out", 0);
        chgStage.Add("d02out3", 2);
        chgStage.Add("m01out", 4);
        chgStage.Add("d02out2", 6);

        // Check Monster Active
        monsterAtv = new List<string>();

        // 상점은 따로 처리하셈
        // Set In Out Pos
        InOutPos[0] = new Vector3(-10f, 11.2f, 21f); // d2out - In
        InOutPos[1] = new Vector3(-11f, 11.2f, 3f); // d2out - Out
        InOutPos[2] = new Vector3(-5f, 11.2f, 19.90001f); // d2out3 - In
        InOutPos[3] = new Vector3(-32f, 11.2f, -3f); // d2out3 - Out
        InOutPos[4] = new Vector3(-12f, 11.8f, 23f); // m01out - In
        InOutPos[5] = new Vector3(-5.450014f, 11.2f, 19.90001f); // m01out - Out
        InOutPos[6] = new Vector3(-5.450014f, 11.2f, 19.90001f); // d2out2 - In
        InOutPos[7] = new Vector3(-5.450014f, 11.2f, 19.90001f); // d2out2 - out

        // For Appear Monster in Battle
        curMoster = new Dictionary<string, btlMosterInfo>();
        curMoster["goomba"] = new btlMosterInfo(goomba, 0f);
        curMoster["nokonoko"] = new btlMosterInfo(nokonoko, 1.8f);
        curMoster["spikey"] = new btlMosterInfo(spikey, 0f);
        curMoster["hammerBro"] = new btlMosterInfo(HammerBro, 0f);

        // Monster Info in Battle
        monStats = new Dictionary<string, MonStats>();
        monStats["goomba"] = new MonStats(11, 3, 1, 1);
        monStats["nokonoko"] = new MonStats(12, 4, 1, 1);
        monStats["spikey"] = new MonStats(10, 5, 1, 2);
        monStats["hammerBro"] = new MonStats(50, 3, 3, 10);

        // items (0: mushroom, 1: honey syrup)
        m_items = new Dictionary<int, int>(); // item index, count
        m_items[(int)EITEMS.MUSHROOM] = 0; // !!!FOR DEDUG!!!!!!!!!!! origin 0
        m_items[(int)EITEMS.HONEY_SYRUP] = 0;// !!!FOR DEDUG!!!!!!!!!!! origin 0

    }
}
