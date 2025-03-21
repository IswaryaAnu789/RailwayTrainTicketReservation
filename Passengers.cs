using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iswarya_trainticket
{
    public class Passengers
    {
        public static int id = 1;
        public string name;
        public int age;
        public string gender;
        public string berthpref;
        public int passengerid = id++;
        public int allocatednumber;
        public string alloctedberth;



        public Passengers(string name, int age, string gender, string berthpref) // to get value from user and store in constructor
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.berthpref = berthpref;
            allocatednumber = -1;
            alloctedberth = " ";


        }
    }
}
