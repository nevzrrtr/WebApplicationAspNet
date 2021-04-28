using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1FirstLab.Models;
using System.Data;
using System.Data.OleDb;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1FirstLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Players> PlforMe = new List<Players>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
            /*string queryString = "SELECT playerid FROM roster";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
                // always call Close when done reading.
                reader.Close();
            }*/
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search()
        {
            Console.WriteLine(System.Security.Claims.ClaimTypes.Email);
            return View();
        }

        public IActionResult show_roster()
        {
            GetInfoFromTableRoster();
            return View(PlforMe);
        }

        [Authorize(Policy = "OnlyAdmin")]
        public IActionResult show_temp()
        {
            GetInfoFromTableTemp();
            return View(PlforMe);
        }


        private void GetInfoFromTableRoster()
        {
            if (PlforMe.Count > 0)
            {
                PlforMe.Clear();
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\anas2\\OneDrive\\Рабочий стол\\CRITTERS.accdb; " +
                "User Id = Admin; Password=;";
            string queryString = "SELECT * FROM roster";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PlforMe.Add(new Players()
                    {
                        playerID = reader["playerid"].ToString(),
                        jersey = reader["jersey"].ToString(),
                        fname = reader["fname"].ToString(),
                        sname = reader["sname"].ToString(),
                        position = reader["position"].ToString(),
                        birthday = reader["birthday"].ToString(),
                        weight = reader["weight"].ToString(),
                        height = reader["height"].ToString(),
                        birthcity = reader["birthcity"].ToString(),
                        birthstate = reader["birthstate"].ToString()
                    });
                }
                reader.Close();
                connection.Close();
            }
        }


        private void GetInfoFromTableTemp()
        {
            if (PlforMe.Count > 0)
            {
                PlforMe.Clear();
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\anas2\\OneDrive\\Рабочий стол\\CRITTERS.accdb; " +
                "User Id = Admin; Password=;";
            string queryString = "SELECT * FROM temp";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PlforMe.Add(new Players()
                    {
                        playerID = reader["playerid"].ToString(),
                        jersey = reader["jersey"].ToString(),
                        fname = reader["fname"].ToString(),
                        sname = reader["sname"].ToString(),
                        position = reader["position"].ToString(),
                        birthday = reader["birthday"].ToString(),
                        weight = reader["weight"].ToString(),
                        height = reader["height"].ToString(),
                        birthcity = reader["birthcity"].ToString(),
                        birthstate = reader["birthstate"].ToString()
                    });
                }

                reader.Close();
                connection.Close();
            }
        }


        public IActionResult DeleteRost(string position, DateTime startDate, DateTime endDate)
        {
            if (PlforMe.Count > 0)
            {
                PlforMe.Clear();
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\anas2\\OneDrive\\Рабочий стол\\CRITTERS.accdb; " +
               "User Id = Admin; Password=;";
            // if only position
            if (String.IsNullOrEmpty(position) == false && endDate.ToString() == "0001/1/1 0:00:00" && startDate.ToString() == "0001/1/1 0:00:00")
            {

                //Console.WriteLine(position + " " + startDate.ToString() + " " + endDate.ToString());

                //Console.WriteLine(position);
                string queryString = "SELECT * FROM roster WHERE [position] = \"" + position + "\"";
                //Console.WriteLine(queryString);
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    string queryStringDelete = "DELETE * FROM roster WHERE [position] = \"" + position + "\"";
                    command = new OleDbCommand(queryStringDelete, connection);
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if only start date
            if (String.IsNullOrEmpty(position) == true && endDate.ToString() == "0001/1/1 0:00:00" && startDate.ToString() != "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE birthday >= #" + startDate.ToString() + "#";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    OleDbDataAdapter adapter;
                    string queryStringDelete = "DELETE * FROM roster WHERE [birthday] >= #" + startDate + "#";
                    command = new OleDbCommand(queryStringDelete, connection);
                    adapter = new OleDbDataAdapter();
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if only end date
            if (String.IsNullOrEmpty(position) == true && endDate.ToString() != "0001/1/1 0:00:00" && startDate.ToString() == "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE birthday <= #" + endDate.ToString() + "#";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    OleDbDataAdapter adapter;
                    string queryStringDelete = "DELETE * FROM roster WHERE [birthday] <= #" + endDate + "#";
                    command = new OleDbCommand(queryStringDelete, connection);
                    adapter = new OleDbDataAdapter();
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if position and start date
            if (String.IsNullOrEmpty(position) == false && endDate.ToString() == "0001/1/1 0:00:00" && startDate.ToString() != "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE [position] = ? AND birthday >= ?";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    OleDbDataAdapter adapter;
                    adapter = new OleDbDataAdapter();
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();

                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    string queryStringDelete = "DELETE * FROM roster WHERE [position] = ? AND [birthday] >= ?";
                    command = new OleDbCommand(queryStringDelete, connection);
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if postion and end date
            if (String.IsNullOrEmpty(position) == false && endDate.ToString() != "0001/1/1 0:00:00" && startDate.ToString() == "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE [position] = ? AND birthday <= ?";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    OleDbDataAdapter adapter;
                    adapter = new OleDbDataAdapter();
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();

                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    string queryStringDelete = "DELETE * FROM roster WHERE [position] = ? AND [birthday] <= ?";
                    command = new OleDbCommand(queryStringDelete, connection);
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if end date and strt date
            if (String.IsNullOrEmpty(position) == true && endDate.ToString() != "0001/1/1 0:00:00" && startDate.ToString() != "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE birthday >= ? AND birthday <= ?";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    OleDbDataAdapter adapter;
                    adapter = new OleDbDataAdapter();
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();

                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    string queryStringDelete = "DELETE * FROM roster WHERE birthday >= ? AND [birthday] <= ?";
                    command = new OleDbCommand(queryStringDelete, connection);
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // if all
            if (String.IsNullOrEmpty(position) == false && endDate.ToString() != "0001/1/1 0:00:00" && startDate.ToString() != "0001/1/1 0:00:00")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string queryString = "SELECT * FROM roster WHERE [position] = ? AND birthday >= ? AND birthday <= ?";
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    OleDbDataAdapter adapter;
                    adapter = new OleDbDataAdapter();
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    //command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();

                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlforMe.Add(new Players()
                        {
                            playerID = reader["playerid"].ToString(),
                            jersey = reader["jersey"].ToString(),
                            fname = reader["fname"].ToString(),
                            sname = reader["sname"].ToString(),
                            position = reader["position"].ToString(),
                            birthday = reader["birthday"].ToString(),
                            weight = reader["weight"].ToString(),
                            height = reader["height"].ToString(),
                            birthcity = reader["birthcity"].ToString(),
                            birthstate = reader["birthstate"].ToString()
                        });
                    }
                    reader.Close();
                    string queryStringDelete = "DELETE * FROM roster WHERE [position] = ? AND birthday >= ? AND [birthday] <= ?";
                    command = new OleDbCommand(queryStringDelete, connection);
                    command.Parameters.Add("position", OleDbType.BSTR).Value = position;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = startDate;
                    command.Parameters.Add("birthday", OleDbType.Date).Value = endDate;
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            foreach (Players p in PlforMe)
            {
                //Console.WriteLine(p.fname + " " + p.sname);
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command;
                connection.Open();
                command = new OleDbCommand("INSERT INTO temp (playerid, jersey, fname, sname, [position], birthday, weight, height, birthcity, birthstate)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", connection);
                command.Parameters.Add("playerid", OleDbType.BSTR).Value = p.playerID;
                command.Parameters.Add("jersey", OleDbType.BigInt).Value = p.jersey;
                command.Parameters.Add("fname", OleDbType.BSTR).Value = p.fname;
                command.Parameters.Add("sname", OleDbType.BSTR).Value = p.sname;
                command.Parameters.Add("position", OleDbType.BSTR).Value = p.position;
                command.Parameters.Add("position", OleDbType.Date).Value = p.birthday;
                command.Parameters.Add("weight", OleDbType.BigInt).Value = p.weight;
                command.Parameters.Add("height", OleDbType.BigInt).Value = p.height;
                command.Parameters.Add("birthcity", OleDbType.BSTR).Value = p.birthcity;
                command.Parameters.Add("birthstate", OleDbType.BSTR).Value = p.birthstate;
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTemp(string PlID)
        {
            if (PlforMe.Count > 0)
            {
                PlforMe.Clear();
            }
            //Console.WriteLine(PlID);
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\anas2\\OneDrive\\Рабочий стол\\CRITTERS.accdb; " +
               "User Id = Admin; Password=;";
            string queryString = "SELECT * FROM temp WHERE playerid = \"" + PlID + "\"";
            //Console.WriteLine(queryString);
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                //command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PlforMe.Add(new Players()
                    {
                        playerID = reader["playerid"].ToString(),
                        jersey = reader["jersey"].ToString(),
                        fname = reader["fname"].ToString(),
                        sname = reader["sname"].ToString(),
                        position = reader["position"].ToString(),
                        birthday = reader["birthday"].ToString(),
                        weight = reader["weight"].ToString(),
                        height = reader["height"].ToString(),
                        birthcity = reader["birthcity"].ToString(),
                        birthstate = reader["birthstate"].ToString()
                    });
                }
                reader.Close();
                string queryStringDelete = "DELETE * FROM temp WHERE playerid = \"" + PlID + "\"";
                command = new OleDbCommand(queryStringDelete, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
                connection.Close();
            }
            foreach (Players p in PlforMe)
            {
                //Console.WriteLine(p.fname + " " + p.sname);
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command;
                connection.Open();
                command = new OleDbCommand("INSERT INTO roster (playerid, jersey, fname, sname, [position], birthday, weight, height, birthcity, birthstate)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", connection);
                command.Parameters.Add("playerid", OleDbType.BSTR).Value = p.playerID;
                command.Parameters.Add("jersey", OleDbType.BigInt).Value = p.jersey;
                command.Parameters.Add("fname", OleDbType.BSTR).Value = p.fname;
                command.Parameters.Add("sname", OleDbType.BSTR).Value = p.sname;
                command.Parameters.Add("position", OleDbType.BSTR).Value = p.position;
                command.Parameters.Add("position", OleDbType.Date).Value = p.birthday;
                command.Parameters.Add("weight", OleDbType.BigInt).Value = p.weight;
                command.Parameters.Add("height", OleDbType.BigInt).Value = p.height;
                command.Parameters.Add("birthcity", OleDbType.BSTR).Value = p.birthcity;
                command.Parameters.Add("birthstate", OleDbType.BSTR).Value = p.birthstate;
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
