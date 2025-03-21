using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iswarya_trainticket
{


    public class Program
    {

        public static void bookticket(Passengers p)
        {
            booktickets bt = new booktickets();

            //<--------------------------Waiting list --------------------------------------------------->
            if (booktickets.awl == 0)
            {
                Console.WriteLine("Sorry No Tickets Available");
                return;
            }
            //<---------------------------GIVING SELECTED BERTHS--------------------------------------------------->
            p.berthpref = p.berthpref?.ToUpper();
            if ((p.berthpref.Equals("L") && booktickets.alb > 0) || (p.berthpref.Equals("M") && booktickets.amb > 0) || (p.berthpref.Equals("U") && booktickets.aub > 0))
            {
                if (p.berthpref.Equals("L"))
                {
                    Console.WriteLine("Lower Berth booked Successfully");
                    bt.bookingticket(p, booktickets.Lblist[0], "L");
                    booktickets.Lblist.RemoveAt(0);
                    booktickets.alb--;
                }
                else if (p.berthpref.Equals("M"))
                {
                    Console.WriteLine("Upper Berth Booked Successfully");
                    bt.bookingticket(p, booktickets.mblist[0], "M");
                    booktickets.mblist.RemoveAt(0);
                    booktickets.amb--;
                }
                else if (p.berthpref.Equals("U"))
                {
                    Console.WriteLine("Upper Berth Booked Successfully");
                    bt.bookingticket(p, booktickets.Ublist[0], "U");
                    booktickets.Ublist.RemoveAt(0);
                    booktickets.aub--;
                }


            }

            //<-----------------------------------------GIVING AVAILABLE BERTHS------------------------------------------------>

            else if (booktickets.aub > 0)
            {
                Console.WriteLine("UPPER BERTH GIVEN: ");
                bt.bookingticket(p, booktickets.Ublist[0], "U");
                booktickets.Ublist.RemoveAt(0);
                booktickets.aub--;

            }
            else if (booktickets.alb > 0)
            {
                Console.WriteLine("LOWER BERTH GIVEN");
                bt.bookingticket(p, booktickets.Lblist[0], "L");
                booktickets.Lblist.RemoveAt(0);
                booktickets.alb--;
            }
            else if (booktickets.amb > 0)
            {
                Console.WriteLine("MIDDLE BERTH GIVEN");
                bt.bookingticket(p, booktickets.mblist[0], "M");
                booktickets.mblist.RemoveAt(0);
                booktickets.amb--;
            }
            else if (booktickets.arac > 0)
            {
                Console.WriteLine("RAC GIVEN");
                bt.racticket(p, booktickets.raclist[0], "RAC");
                booktickets.raclist.RemoveAt(0);
                booktickets.arac--;
            }
            else if (booktickets.awl > 0)
            {
                Console.WriteLine("WAITING LIST GIVEN");
                bt.waitinglistbooking(p, booktickets.wllist[0], "WAIT LIST");
                booktickets.wllist.RemoveAt(0);
                booktickets.awl--;
            }
           



        }


       


        static void Main(string[] args)
        {
            bool loop = true;

            while (loop)
            {
                Console.WriteLine(" 1. Book \n 2. Available Ticket \n 3. Booked Tickets \n 4. Cancel Ticket \n 5. Exit");
                Console.WriteLine("Enter Your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                        Console.WriteLine("Enter Passenger name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Age: ");
                        int age = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Gender [M/F]: ");
                        string gender = Console.ReadLine();
                        Console.WriteLine("Enter Your Berthpreference[L/M/U]: ");
                        string berthpref = Console.ReadLine();
                        Passengers p = new Passengers(name, age, gender, berthpref); // constructor used in class passengers
                        bookticket(p);
                        break;
                    case 2:
                        Console.WriteLine("-------------LISTING AVAILABLE TICKETS----------------------------");
                        Console.WriteLine("AVAILABLE LOWER BERTHS: " + booktickets.alb);
                        Console.WriteLine("AVAILABLE MIDDLE BERTHS: " + booktickets.amb);
                        Console.WriteLine("AVAILABLE UPPER BERTHS: " + booktickets.aub);
                        Console.WriteLine("AVAILABLE RAC TICKETS: " + booktickets.arac);
                        Console.WriteLine("AVAILABLE WAITING LIST TICKETS: " + booktickets.awl);
                        Console.WriteLine("--------------------END------------------------------------------");
                        break;

                    case 3:
                        booktickets bp = new booktickets();
                        bp.printpassdetail();
                        break;
                    case 4:
                        Console.WriteLine("ENTER PASSANGER ID: ");
                        int id = int.Parse(Console.ReadLine());
                        booktickets bt = new booktickets();
                        bt.ticketcancel(id);
                        break;
                    case 5:
                        loop = false;
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}