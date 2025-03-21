using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iswarya_trainticket
{
    public class booktickets
    {
        public static int alb = 2;
        public static int aub = 1;
        public static int amb = 1;
        public static int arac = 1;
        public static int awl = 1;

        public static List<int> Lblist = new List<int> {};
        public static List<int> Ublist = new List<int> { };
        public static List<int> mblist = new List<int> {  };
        public static List<int> raclist = new List<int> { };
        public static List<int> wllist = new List<int> {  };
        public static List<int> totalberthlist = new List<int>(); // stored only passenger id


        static Queue<int> racque = new Queue<int>();
        static Queue<int> waitinglist = new Queue<int>();
        public static Dictionary<int, Passengers> storedlist = new Dictionary<int, Passengers>(); // stored both id and details

        // to get value from constructor
        public void bookingticket(Passengers p, int allnumber, string allberth)
        {
            p.allocatednumber = allnumber; // to set number to passanger
            p.alloctedberth = allberth; // to allocate berth
            storedlist.Add(p.passengerid, p); // stored passenger id and details
            totalberthlist.Add(p.passengerid);//stored only id

            Console.WriteLine("-----------------BOOKED DETAILS-------------------------------");
            Console.WriteLine("Passenger ID: " + p.passengerid);
            Console.WriteLine("Passenger Name: " + p.name);
            Console.WriteLine("Passenger Gender: " + p.gender);
            Console.WriteLine("Passenger Berthpreference: " + p.berthpref);
            Console.WriteLine("Alloted seat: " + allberth + allnumber);
            Console.WriteLine("----------------------BOOKED SUCCESSFULLY");

        }

        public void racticket(Passengers p, int allnumber, string racberth)
        {
            p.allocatednumber = allnumber;
            p.alloctedberth = racberth;
            storedlist.Add(p.passengerid, p);
            racque.Enqueue(p.passengerid);
            Console.WriteLine("Passenger ID: " + p.passengerid);
            Console.WriteLine("Passenger Name: " + p.name);
            Console.WriteLine("Passenger Gender: " + p.gender);
            Console.WriteLine("Passenger Berthpreference: " + p.berthpref);
            Console.WriteLine("Alloted seat: " + racberth + allnumber);
            Console.WriteLine("----------------------RAC BERTH GIVEN");

        }

        public void waitinglistbooking(Passengers p, int allnumber, string waitinglistberth)
        {
            p.allocatednumber = allnumber;
            p.alloctedberth = waitinglistberth;
            storedlist.Add(p.passengerid, p);
            waitinglist.Enqueue(p.passengerid);
            Console.WriteLine("Passenger ID: " + p.passengerid);
            Console.WriteLine("Passenger Name: " + p.name);
            Console.WriteLine("Passenger Gender: " + p.gender);
            Console.WriteLine("Passenger Berthpreference: " + p.berthpref);
            Console.WriteLine("Alloted seat: " + waitinglistberth + allnumber);
            Console.WriteLine("----------------------YOU ARE IN WAITING LIST");

        }

        //<--------------------------------------------------PRINTING BOOKED TICKET----------------------------------------->


        public void printpassdetail()
        {
            if (storedlist.Count == 0)
            {
                Console.WriteLine("NO PASSENGERS FOUND");
            }
            else
            {
                foreach (var passenger in storedlist.Values)
                {
                    Console.WriteLine("Passenger ID: " + passenger.passengerid);
                    Console.WriteLine("Name: " + passenger.name);
                    Console.WriteLine("Gender: " + passenger.gender);
                    Console.WriteLine("Berth Preference: " + passenger.berthpref);
                    Console.WriteLine("Allocated Seat: " + passenger.alloctedberth + " - " + passenger.allocatednumber);
                    Console.WriteLine("------------------------------");
                }
            }
        }

        public void ticketcancel(int passengerID)
        {
            if (storedlist.TryGetValue(passengerID, out Passengers p))
            {
                // Remove passenger from stored list and berth list
                storedlist.Remove(passengerID);
                totalberthlist.Remove(passengerID);
                int psnumber = p.allocatednumber;
                string freedBerth = p.alloctedberth;
                Console.WriteLine($"Passenger with ID {passengerID} has been successfully canceled.");

                // Free up the canceled berth
                if (freedBerth.Equals("L"))
                {
                    Lblist.Add(psnumber);
                    alb++;
                }
                else if (freedBerth.Equals("M"))
                {
                    mblist.Add(psnumber);
                    amb++;
                }
                else if (freedBerth.Equals("U"))
                {
                    Ublist.Add(psnumber);
                    aub++;
                }

                // Move RAC passenger to confirmed berth
                if (racque.Count > 0)
                {
                    int racPassengerId = racque.Dequeue(); // Get first RAC passenger
                    Passengers passengerfromrac = storedlist[racPassengerId];

                    // Assign the freed berth to RAC passenger
                    passengerfromrac.allocatednumber = psnumber;
                    passengerfromrac.alloctedberth = freedBerth;

                    // Update stored list with new seat details
                    storedlist[racPassengerId] = passengerfromrac;

                    Console.WriteLine($"RAC Passenger {passengerfromrac.passengerid} moved to confirmed berth {freedBerth}{psnumber}");

            


                    // Move waiting list passenger to RAC if RAC seats are available
                    if (waitinglist.Count > 0 && raclist.Count > 0)
                    {
                        int waitingPassengerId = waitinglist.Dequeue(); // Get first waiting list passenger
                        Passengers pfwl = storedlist[waitingPassengerId];

                        // Assign first available RAC berth
                        int availableRACSeat = raclist[0]; // Get first available RAC seat
                        pfwl.allocatednumber = availableRACSeat;
                        pfwl.alloctedberth = "RAC"; // Update berth type to "RAC"

                        // Remove assigned RAC seat from list
                        raclist.RemoveAt(0); // Remove the assigned RAC seat
                        racque.Enqueue(waitingPassengerId); // Move passenger from WL to RAC

                        Console.WriteLine($"Waiting List Passenger {pfwl.passengerid} moved to RAC berth {pfwl.allocatednumber}");

                        awl--; // Decrease available waiting list count
                    }
                }
            }
            else
            {
                Console.WriteLine($"Passenger with ID {passengerID} not found.");
            }
                
            }
        }
    }
 

