using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;

namespace Vaulty.Utils
{
    /// <summary>
    /// The Const class holds all the constant values used by the bot
    /// </summary>
    public class Const
    {
        public static readonly ulong SECONDS_IN_MINUTE = 60;
        public static readonly ulong SECONDS_IN_HOUR = 3600;
        public static readonly ulong SECONDS_IN_DAY = 86400;
        public static readonly ulong SECONDS_IN_WEEK = 604800;

        #region File names
        public static readonly string TOKEN_FILE = "sensitive.json";
        public static readonly string INFO_FILE = "info.json";
        #endregion

        #region Default values for DB insert/update
        public static readonly int USER_ADD_DEFAULT_COINS = 500;
        public static readonly int USER_ADD_DEFAULT_VAULTIUM = 0;
        #endregion

        #region Default rewards
        public static readonly int DAILY_REWARD = 80;
        public static readonly int WEEKLY_REWARD = 500;
        public static readonly int WORK_REWARD = 20;
        #endregion

        #region Streaks and Increments
        public static readonly double DAILY_STREAK_INCR = 0.23;
        #endregion

        #region Command usage

        // 🪙 Economy Core Module
        public static readonly string USAGE_BALANCE = "Usage : v!balance";
        public static readonly string USAGE_BAL = "Usage : v!bal";
        public static readonly string USAGE_DEPOSIT = "Usage : v!deposit <amount>";
        public static readonly string USAGE_WITHDRAW = "Usage : v!withdraw <amount>";
        public static readonly string USAGE_PAY = "Usage : v!pay <user> <amount>";
        public static readonly string USAGE_NETWORTH = "Usage : v!networth";

        // 🎁 Rewards Module
        public static readonly string USAGE_DAILY = "Usage : v!daily";
        public static readonly string USAGE_WEEKLY = "Usage : v!weekly";
        public static readonly string USAGE_BEG = "Usage : v!beg";
        public static readonly string USAGE_QUEST = "Usage : v!quest";
        public static readonly string USAGE_CRIME = "Usage : v!crime";

        // 💼 Jobs Module
        public static readonly string USAGE_JOB_LIST = "Usage : v!job list";
        public static readonly string USAGE_JOB_APPLY = "Usage : v!job apply <job>";
        public static readonly string USAGE_JOB_QUIT = "Usage : v!job quit";
        public static readonly string USAGE_JOB_WORK = "Usage : v!job work";

        // 🏢 Business Module
        public static readonly string USAGE_STARTBUSINESS = "Usage : v!startbusiness";
        public static readonly string USAGE_MYBUSINESS = "Usage : v!mybusiness";
        public static readonly string USAGE_UPGRADEBUSINESS = "Usage : v!upgradebusiness";

        // 🎲 Gambling & Minigames Module
        public static readonly string USAGE_COINFLIP = "Usage : v!coinflip <amount>";
        public static readonly string USAGE_SLOTS = "Usage : v!slots";
        public static readonly string USAGE_BLACKJACK = "Usage : v!blackjack";
        public static readonly string USAGE_DICE = "Usage : v!dice <amount>";
        public static readonly string USAGE_DUEL = "Usage : v!duel <user> <amount>";
        public static readonly string USAGE_LOTTERY_BUY = "Usage : v!lottery buy";
        public static readonly string USAGE_LOTTERY_DRAW = "Usage : v!lottery draw";
        public static readonly string USAGE_HEIST = "Usage : v!heist <user/group>";

        // 🛍️ Shop & Inventory Module
        public static readonly string USAGE_SHOP = "Usage : v!shop";
        public static readonly string USAGE_BUY = "Usage : v!buy <item>";
        public static readonly string USAGE_SELL = "Usage : v!sell <item>";
        public static readonly string USAGE_INVENTORY = "Usage : v!inventory";
        public static readonly string USAGE_USE = "Usage : v!use <item>";
        public static readonly string USAGE_ITEMINFO = "Usage : v!iteminfo <item>";
        public static readonly string USAGE_EQUIP = "Usage : v!equip <item>";

        // 🧬 RPG & Progression Module
        public static readonly string USAGE_PROFILE = "Usage : v!profile";
        public static readonly string USAGE_LEVELUP = "Usage : v!levelup";
        public static readonly string USAGE_SKILLS = "Usage : v!skills";
        public static readonly string USAGE_STATS = "Usage : v!stats";

        // 🏆 Leaderboard Module
        public static readonly string USAGE_LEADERBOARD = "Usage : v!leaderboard";
        public static readonly string USAGE_RANK = "Usage : v!rank";

        // 🤪 Fun & Meme Module
        public static readonly string USAGE_FLEX = "Usage : v!flex";
        public static readonly string USAGE_RICHMETER = "Usage : v!richmeter";
        public static readonly string USAGE_STEALCAT = "Usage : v!stealcat";

        // 🛠️ Admin Module
        public static readonly string USAGE_ADDMONEY = "Usage : v!addmoney <user> <amount>";
        public static readonly string USAGE_REMOVEMONEY = "Usage : v!removemoney <user> <amount>";
        public static readonly string USAGE_SETBALANCE = "Usage : v!setbalance <user> <amount>";
        public static readonly string USAGE_RESETUSER = "Usage : v!resetuser <user>";
        public static readonly string USAGE_ECONOMY_STATS = "Usage : v!economy stats";
        #endregion

        #region assets
        public static readonly string VAULTYCOINS_EMOJI = "<:vaultycoins:1358895330300788927>";
        public static readonly string VAULTIUM_EMOJI = "<:vtium:1358901632154337370>";
        #endregion
    }
}
