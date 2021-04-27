using MRRC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    class Program
    {
        //Menu Menu = new Menu();

        public static Fleet fleeet = new Fleet(@"..\..\..\Data\fleet.csv", @"..\..\..\Data\rentals.csv");

        static void Main(string[] args)
        {
            try
            {
                CRM crm = new CRM(args[0]);
                Fleet fleet = new Fleet(args[1], args[2]);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("No Arguments detected or 3 arguments werent given");
                Menu.ExitProgram();
            }

            int menuOption;
            int submenuOption;
            int subsubmenuOption;
            Menu.WelcomeMessage();
            do
            {
                Menu.MainMenu();
                menuOption = Menu.ReadOption();

                while (menuOption == 1)
                {
                    Menu.SubMenu(1);
                    submenuOption = Menu.ReadOption2(1);

                    if (submenuOption == 1)
                    {
                        while(submenuOption == 1)
                        {
                            Menu.subsubmenu();
                            subsubmenuOption = Menu.ReadOption3();

                            if(subsubmenuOption == 11)
                            {
                                break;
                            }

                            if (subsubmenuOption == 1)
                            {
                                Menu.DisplayCustomer();

                            }else if(subsubmenuOption == 2)
                            {

                                Menu.GetCustomerContents();

                            }
                        }
                        
                    }

                    if (submenuOption == 2)
                    {
                        Menu.NewCustomer();
                    }

                    while (submenuOption == 3)
                    {

                        Menu.EditCustomerMenu();
                        int EditMenu = Menu.ReadEditOption();

                        if(EditMenu == 11)
                        {
                            break;
                        }

                        if (EditMenu == 1)
                        {
                            Menu.EditCustomerMenuReceiveID(1);

                        }
                        if (EditMenu == 2)
                        {
                            Menu.EditCustomerMenuReceiveID(2);

                        }
                        if (EditMenu == 3)
                        {
                            Menu.EditCustomerMenuReceiveID(3);

                        }
                        if (EditMenu == 4)
                        {
                            Menu.EditCustomerMenuReceiveID(4);

                        }
                        if (EditMenu == 5)
                        {
                            Menu.EditCustomerMenuReceiveID(5);

                        }
                        if (EditMenu == 6)
                        {
                            Menu.EditCustomerMenuReceiveID(6);

                        }
                    }

                    if (submenuOption == 4)
                    {
                        Menu.removeCustomer();
                    }

                    if (submenuOption == 11)
                    {
                        break;
                    }
                }
                    
                while (menuOption == 2)
                {
                    Menu.SubMenu(2);
                    submenuOption = Menu.ReadOption2(2);

                    if (submenuOption == 11)
                    {
                        break;
                    }

                    if(submenuOption == 1)
                    {
                        while (submenuOption == 1)
                        {
                            Menu.fleetsubsubmenu();
                            subsubmenuOption = Menu.ReadOption3();

                            if (subsubmenuOption == 11)
                            {
                                break;
                            }

                            if (subsubmenuOption == 1)
                            {
                                Menu.DisplayFleet();

                            }
                        }   
                    }
                    if (submenuOption == 2)
                    {
                        while(submenuOption == 2)
                        {
                            Menu.newvehiclesubmenu();
                            subsubmenuOption = Menu.ReadOption3();
                            if (subsubmenuOption == 11)
                            {
                                break;
                            }

                            if (subsubmenuOption == 1)
                            {
                                Menu.NewVehicle();

                            }

                            if (subsubmenuOption == 2)
                            {
                                Menu.NewVehicleWDefaults();
                            }
                        }
                        


                    }
                    if (submenuOption == 3)
                    {
                        while(submenuOption == 3)
                        {
                            Menu.EditMenuWarning();
                            int WarningMenu = Menu.ReadOption5();

                            if(WarningMenu == 1)
                            {
                                break;
                            }

                            if(WarningMenu == 2)
                            {
                                Menu.ExitProgram();
                            }

                            if(WarningMenu == 3)
                            {
                                Menu.EditVehicleMenu();
                                int EditMenu = Menu.ReadEditVehicleOption();

                                if (EditMenu == 1)
                                {
                                    Menu.EditVehicleMenuReceiveID(1);

                                }
                                if (EditMenu == 2)
                                {
                                    Menu.EditVehicleMenuReceiveID(2);

                                }
                                if (EditMenu == 3)
                                {
                                    Menu.EditVehicleMenuReceiveID(3);

                                }
                                if (EditMenu == 4)
                                {
                                    Menu.EditVehicleMenuReceiveID(4);

                                }
                                if (EditMenu == 5)
                                {
                                    Menu.EditVehicleMenuReceiveID(5);

                                }
                                if (EditMenu == 6)
                                {
                                    Menu.EditVehicleMenuReceiveID(6);

                                }
                                if (EditMenu == 7)
                                {
                                    Menu.EditVehicleMenuReceiveID(7);

                                }
                                if (EditMenu == 8)
                                {
                                    Menu.EditVehicleMenuReceiveID(8);

                                }
                                if (EditMenu == 9)
                                {
                                    Menu.EditVehicleMenuReceiveID(9);

                                }
                                if (EditMenu == 10)
                                {
                                    Menu.EditVehicleMenuReceiveID(10);

                                }
                                if (EditMenu == 11)
                                {
                                    Menu.EditVehicleMenuReceiveID(11);

                                }
                                if (EditMenu == 12)
                                {
                                    Menu.EditVehicleMenuReceiveID(12);

                                }
                            }
                            
                        }
                        
                    }
                    if (submenuOption == 4)
                    {
                        Menu.removeVehicle();

                    }
                }

                while (menuOption == 3)
                {
                    Menu.SubMenu(3);
                    submenuOption = Menu.ReadOption2(3);

                    if (submenuOption == 11)
                    {
                        break;
                    }

                    if(submenuOption == 1)
                    {
                        //display
                        Menu.DisplayRentals();
                    }

                    if(submenuOption == 2)
                    {

                        //_________________________________________________  
                        //This code is from Shlomo Geva's lecture in week 9
                        //__________________________________________________
                        //search
                        while (submenuOption == 2) // forever or until user quits
                        {
                            try
                            {
                                Menu.getquery(out ArrayList query);
                                if (query[0].Equals("BACK"))
                                {
                                    break;
                                }
                                if (query[0].Equals("QUIT"))
                                {
                                    Console.WriteLine("Bye!");
                                    Menu.ExitProgram();
                                    
                                    
                                }
                                if (query[0].Equals("BLANK"))
                                {
                                    Menu.blankquery();
                                    break;
                                }
                                Menu.search(new RPN(query), fleeet); // convert query to RPN and search
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }

                    if(submenuOption == 3)
                    {
                        Menu.rentcar();
                    }

                    if(submenuOption == 4)
                    {
                        Menu.Return ();
                    }
                }


            } while (!(Console.ReadKey(true).Key == ConsoleKey.Escape));

            Menu.ExitProgram();
        }



    }
}