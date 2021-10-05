using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private List<Comment> comments {  get; set; }
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
            //login = "gerik123";
            this.login = login;
            RefreshOrdersList();
            RefreshCommentsList();
            CallMainPage();
        }
        //===========================Log -> Main
        private void CallMainPage()
        {
            Console.WriteLine("MAIN PAGE...");
            Menu("===MAIN MENU===",
                new[] { "Role Page", "Orders & Comments", "Exit"},
                new Action[] { CallRolePage, CallOrdersCommentsPage, GoToExit });
        }
        //===========================Main -> Role/OrderHistory
        private void CallRolePage()
        {
            Menu("Role Page", new [] {"Profile Info", "Settings", "Logout","Back"},
                                        new Action[]{ShowProfileInfo, CallProfileSettings, CallLogPage,GoToExit});
        }

        //Role Page -> Profile Info/Settings
        private void CallProfileSettings()
        {
            Menu("Settings", new[] { "Change Password", "Change Display Name", "Back" }, new Action[] { ChangeUserPassword, ChangeUserDisplayName, GoToExit });
        }

        private void CallOrdersCommentsPage()
        {
            Menu("Orders & Comments",
                new[] {"Orders", "Comments", "Back"},
                new Action[] { CallOrdersPage, CallCommentsPage ,GoToExit });
        }

        private void CallOrdersPage()
        {
            Menu("Orders",
                new[] { "Sort", "Search", "Result","Back" },
                new Action[] { CallSortPage, CallSearchPage, ShowOrdersResult, GoToExit });
        }

        private void CallCommentsPage()
        {
            Menu("Comments",
                new[] { "Add Comment", "All Comments", "Back" },
                new Action[] { AddComment, ShowComments, GoToExit });
        }



        //===========================OrderHistory -> Sort / Search / Repeat Order / Show Result / Write Comment
        private void CallSortPage()
        {
            Menu("Sort",
                new[] { "Ascending", "Descending", "Back" },
                new Action[] { SortOrderHistoryASC, SortOrderHistoryDESC, GoToExit });
        } //Sort -> ASC / DESC
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
        private void ShowOrdersResult()
        {
            Console.WriteLine("{0,-10}{1,-15}{2,-25}{3,-20}\n{4}", "Order ID", "Product", "Order Time","Comments", new string('=', 58));
            foreach (var order in orders_filtered)
            {
                Console.Write("{0,-10}{1,-15}{2,-25}", order.id, order.product, order.order_time);
                foreach (var comment in comments.Where(comment => comment.order_id == order.id)
                    .Select((val, i) => (val, i)))
                {
                    if (comment.i != 0)
                        Console.Write(new string(' ', 50));
                    Console.WriteLine($"{comment.val.text}");
                }
                if (comments.Where(comment => comment.order_id == order.id).Count() == 0)
                    Console.WriteLine("...");
            }
            Console.ReadKey();
        }
        private void ShowComments()
        {
            Console.WriteLine("{0,-15}{1,-15}{2,-25}{3,-5}\n{4}", "ID", "Order ID", "Order Time", "Text", new string('=', 59));
            foreach (var comment in comments)
                Console.WriteLine("{0,-15}{1,-15}{2,-25}{3,-5}", comment.id, comment.order_id, comment.comment_time, comment.text);
            Console.ReadKey();
        }
        //===========================
        private void ShowProfileInfo()
        {
            var user_info = userDAL.GetUserInfo(login);
            Console.WriteLine("{0,-25}{1,-20}{2,-20}", "Display Name", "Login", "Password");
            Console.WriteLine(new string('=', 65));
            Console.WriteLine("{0,-25}{1,-20}{2,-20}", user_info.Item3, user_info.Item1, user_info.Item2);
            Console.ReadKey();
        }
        private void SortOrderHistoryASC() => orders_filtered = orders.OrderBy(order => order.product).ToList();
        private void SortOrderHistoryDESC() => orders_filtered = orders.OrderByDescending(order => order.product).ToList();
        private void GoToExit() => exit = true;
        private void RefreshOrdersList()
        {
            orders = ordersLogDAL.GetOrderHistoryOfUser(login);
            orders_filtered = ordersLogDAL.GetOrderHistoryOfUser(login);
        }
        private void RefreshCommentsList()
        {
            comments = commentsDAL.GetCommentsOfUser(login);
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
        private void AddComment()
        {
            Console.Write("Order id : "); int order_id = int.Parse(Console.ReadLine());
            if (ordersLogDAL.OrderIDToUserCheck(login, order_id))
            {
                Console.Write("Comment : "); string comment_text = Console.ReadLine();
                commentsDAL.AddComment(order_id, comment_text);
            }
            RefreshCommentsList();
        }
        
    }
}
//"Sort", "Search", "Repeat Order", "Write Comment", "Result","Comments", "Back"