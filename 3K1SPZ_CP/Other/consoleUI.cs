using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _3K1SPZ_CP.DAL;

namespace _3K1SPZ_CP
{
    public class consoleUI
    {
        private bool exit { get; set; }
        private string login { get; set; }
        private List<Order> orders { get; set; }
        private List<Order> orders_filtered { get; set; }
        public consoleUI()
        {
            login = "";
            exit = false;
        }
        private void Menu(string title, string[] options, Action[] actions)
        {
            int rowN = options.Length, pPos = 1;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"\t{title}");
                for (int i = 0; i < rowN; i++)
                {
                    Console.Write($"{i + 1}. ");
                    Console.Write("{0,-20}", options[i]);
                    if (i + 1 == pPos)
                        Console.Write($"<");
                    Console.WriteLine();
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (1 < pPos) pPos--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (pPos < rowN) pPos++;
                        break;
                    case ConsoleKey.E:
                    case ConsoleKey.Enter:
                        Console.Clear();
                        actions[pPos - 1]();
                        break;
                }
            }
            exit = false;
        }
        //===========================
        public void CallLogPage()
        {
            string login, password;
            do
            {
                Console.Clear();
                Console.Write("Login : "); login = Console.ReadLine();
                Console.Write("Password : "); password = Console.ReadLine();
            } while (!userDAL.CheckPassword(login, password));
            this.login = login;
            CallMainPage();
        }
        //===========================Log -> Main
        private void CallMainPage()
        {
            Console.WriteLine("MAIN PAGE...");
            Menu("===MAIN MENU===",
                new[] { "Role Page", "Order History", "Exit" },
                new Action[] { CallRolePage, CallOrderHistory, GoToExit });
        }
        //===========================Main -> Role or OrderHistory
        private void CallRolePage()
        {
            Menu("Role Page", new [] {"Profile Info", "Settings", "Exit"}, new Action[]{ShowProfileInfo, CallProfileSettings, GoToExit});
        }

        private void CallProfileSettings()
        {
            Menu("Settings", new[] { "Change Password", "Change Display Name", "Exit" }, new Action[] { ChangeUserPassword, ChangeUserDisplayName, GoToExit });
        }

        private void ShowProfileInfo()
        {
            var user_info = userDAL.GetUserInfo(login);
            Console.WriteLine("{0,-25}{1,-20}{2,-20}", "Display Name", "Login", "Password");
            Console.WriteLine(new string('=',65));
            Console.WriteLine("{0,-25}{1,-20}{2,-20}",user_info.Item3, user_info.Item1, user_info.Item2);
            Console.ReadKey();
        }

        private void CallOrderHistory()
        {
            RefreshOrdersList();
            Menu("Options",
                new[] { "Sort", "Search", "Repeat Order", "Result", "Exit" },
                new Action[] { CallSortPage, CallSearchPage, CallRepeatOrderPage, CallResultPage, GoToExit });
        }
        //===========================OrderHistory -> Sort or Search or Repeat Order or Show Result
        private void CallSortPage()
        {
            Menu("Sort",
                new[] { "Ascending", "Descending", "Exit" },
                new Action[] { SortOrderHistoryASC, SortOrderHistoryDESC, GoToExit });
        }
        //---------------------------Sort -> ASC or DESC
        private void CallRepeatOrderPage()
        {
            Console.WriteLine("\tRepeat order");
            Console.Write("Order id : "); int order_id = int.Parse(Console.ReadLine());
            if(ordersLogDAL.OrderIDToUserCheck(login, order_id))
                ordersLogDAL.PlaceOrder(login, ordersLogDAL.GetProductNameByOrderID(order_id));
            RefreshOrdersList();
        }
        private void CallSearchPage()
        {
            Console.WriteLine("\tSearch");
            Console.Write("Product name : "); string product_name = Console.ReadLine();
            orders_filtered = orders.FindAll(order => order.product == product_name);
        }
        private void CallResultPage()
        {
            Console.WriteLine("{0,-10}{1,-15}{2,-20}\n{3}", "Order ID", "Product", "Order Time", new string('=', 45));
            foreach (var order in orders_filtered)
                Console.WriteLine("{0,-10}{1,-15}{2,-20}", order.id, order.product, order.order_time);
            Console.ReadKey();
        }
        //===========================
        private void SortOrderHistoryASC() => orders_filtered = orders.OrderBy(order => order.product).ToList();
        private void SortOrderHistoryDESC() => orders_filtered = orders.OrderByDescending(order => order.product).ToList();
        private void GoToExit() => exit = true;
        private void RefreshOrdersList()
        {
            orders = ordersLogDAL.GetOrderHistoryOfUser(login);
            orders_filtered = ordersLogDAL.GetOrderHistoryOfUser(login);
        }
        private void ChangeUserDisplayName()
        {
            Console.WriteLine("\tChange Display Name");
            Console.Write("New name : "); string new_disp_name = Console.ReadLine();
            userDAL.ChangeDispNameByLogin(login, new_disp_name);
        }

        private void ChangeUserPassword()
        {

            Console.WriteLine("\tChange Password");
            Console.Write("New password : "); string new_password = Console.ReadLine();
            userDAL.ChangePasswordByLogin(login, new_password);
        }
    }
}
