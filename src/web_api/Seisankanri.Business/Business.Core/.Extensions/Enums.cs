using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Extensions
{
    public class Modules
    {
        public static string CORE = "Core";
        public static string INVENTORY = "Inventory";
        public static string MAUFACTURE = "Maufacture";
        public static string LOG = "Log";
    }

    public class Screen
    {
        public static string MESSAGE = "Message";
        public static string CACHE = "Cache";
        public static string MASTERCODE = "MasterCode";
        public static string RESOURCE = "Resource";
        public static string COMMON = "Common";
        public static string INVENTORY = "Inventory";
        public static string UNIT = "Unit";
        public static string MACHINE = "Machine";

    }

    public class MessageKey
    {
        public static string I_001 = "I_001"; // Data registration is complete.
        public static string I_002 = "I_002"; // The data update is complete.
        public static string I_003 = "I_003"; // Data deletion is complete.
        public static string I_004 = "I_004"; // Data export is complete.
        public static string I_005 = "I_005"; // Get data complete.

        public static string E_001 = "E_001"; // Data registration failed.
        public static string E_002 = "E_002"; // Failed to update the data.
        public static string E_003 = "E_003"; // Data cannot be deleted.
        public static string E_004 = "E_004"; // Data export failed.
        public static string E_005 = "E_005"; // Data master [master name] has been deleted or does not exist on the system
        public static string E_007 = "E_007"; // Data not found
        public static string E_008 = "E_008"; // Request content invalid
        public static string E_009 = "E_009"; // Data required.
        public static string E_010 = "E_010"; 

        public static string BE_003 = "BE_003"; // Internal server error
        public static string BE_004 = "BE_004"; // You have no authority to access the required resources


        public static string W_001 = "W_001"; // "Item" is required. Please enter the "Item".
        public static string W_002 = "W_002"; // "Item" is different. Please try again.
        public static string W_012 = "W_012"; 

        public static string CLEARALLSUCCESS = "ClearAllSuccess"; // Delete the success of all cashagain.

        public static string TYPE = "Type";
        public static string KEY = "Key";
        public static string VALUE = "Value";

    }

    public class CompanyType
    {
        public const string PLACE = "place";
        public const string BRANCH = "branch";
        public const string CUSTOMER = "customer";
        public const string SUPPLIER = "supplier";
        public const string OUTSOURCER = "outsourcer";
        public const string DESTINATION = "destination";
        public const string TRANSPOST = "transpost";
        public const string MAKER = "maker";    
    }
}
