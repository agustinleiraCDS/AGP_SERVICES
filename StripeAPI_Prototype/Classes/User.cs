using System;
namespace StripeAPI_Prototype.Classes
{
    public class User
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string cc { get; set; }
        public User()
        {

        }

        public string toString(){

            string res = "";

            res += "name = " + name;
            res += " lastname = " + lastname;
            res += " cc = " + cc;

            return res;
        }

    }
}
