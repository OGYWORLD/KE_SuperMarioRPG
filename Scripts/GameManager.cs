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
    public int m_speed { get; set; }
    public int m_magicAttack { get; set; }
    public int m_magicDefense { get; set; }

    public Stats(int _attack, int _defense, int _speed, int _magicAttack, int _magicDefense)
    {
        m_attak = _attack;
        m_defense = _defense;
        m_speed = _speed;
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

public struct Pos
{
    public readonly float m_x;
    public readonly float m_y;
    public readonly float m_z;

    public Pos(float _x, float _y, float _z) : this()
    {
        m_x = _x;
        m_y = _y;
        m_z = _z;
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
    public int[] EXPNeeded { get; set; } = { 16, 48, 84 };
    public CharacterInfo[] members { get; set; } // members(Mario, Mellow) Info Array
    public int curFP { get; set; } = 10;
    public int maxFP { get; set; } = 10;
    public int gauge{ get; set; } = 0;
    public int coin { get; set; } = 0;
    public string curStage { get; set; } = "MushRoad";
    public string[] memBonus { get; set; }
    // !!!!!!!!!!F OR DEBUG CHANGE MELLOW
    public EMEMBER memberIndex { get; set; } = EMEMBER.MELLOW;
    //public EMEMBER memberIndex { get; set; } = EMEMBER.MARIO;
    public string beforeSceneName { get; set; } = "first";
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
    public Pos curMarioPos { get; set; } = new Pos(-5.450014f, 11.2f, 19.90001f);

    // Check Monster Active
    public List<string> monsterAtv { get; set; }

    // !!!! NEED SET DETAIL ARRAY SIZE - IN OUT NUM !!!!
    public Pos[] InOutPos = new Pos[6];

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
        Stats marioInitStat = new Stats(20, 0, 20, 10, 2);
        magicAttackInfo[] marioMagicAttack =
        {
            new magicAttackInfo("점프", 3, 25),
            new magicAttackInfo("파이어볼", 5, 20)
        };
        members[(int)EMEMBER.MARIO] = new CharacterInfo("마리오", 1, 20, 20, 0, EXPNeeded[0], 0, marioInitStat, marioMagicAttack, 0, 0);

        // Make Mellow Info
        Stats mellowInitStat = new Stats(22, 3, 18, 15, 10);
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
        chgStage.Add("d02out2", 2);
        chgStage.Add("d02out3", 4);

        // Check Monster Active
        monsterAtv = new List<string>();

        // Set In Out Pos
        InOutPos[0] = new Pos(-5.450014f, 11.2f, 19.90001f); // MushRoad1 - In

        // For Appear Monster in Battle
        curMoster = new Dictionary<string, btlMosterInfo>();
        curMoster["goomba"] = new btlMosterInfo(goomba, 0f);
        curMoster["nokonoko"] = new btlMosterInfo(nokonoko, 2.25f);
        curMoster["spikey"] = new btlMosterInfo(spikey, 0f);
        curMoster["hammerBro"] = new btlMosterInfo(HammerBro, 0f);

        // Monster Info in Battle
        monStats = new Dictionary<string, MonStats>();
        monStats["goomba"] = new MonStats(100, 1, 1, 1); // !!! FOR DEBUG !!! origin hp is 3, origin attak is 16
        monStats["nokonoko"] = new MonStats(10, 4, 1, 1);
        monStats["splikey"] = new MonStats(20, 5, 1, 2);
        monStats["hammerBro"] = new MonStats(50, 3, 3, 10);

        // items (0: mushroom, 1: honey syrup)
        m_items = new Dictionary<int, int>(); // item index, count
        m_items[(int)EITEMS.MUSHROOM] = 2; // !!!FOR DEDUG!!!!!!!!!!! origin 0
        m_items[(int)EITEMS.HONEY_SYRUP] = 2;// !!!FOR DEDUG!!!!!!!!!!! origin 0

    }
}
