using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inset_and_get_data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    string user, pass, db;
                    Console.WriteLine("Conect to Database");
                    Console.WriteLine("Input User ID");
                    user = Console.ReadLine();
                    Console.WriteLine("Input password");
                    pass = Console.ReadLine();
                    Console.WriteLine("Input Database destinaiton");
                    db = Console.ReadLine();
                    Console.WriteLine("Write K to Connect to Database");
                    char chr = Convert.ToChar(Console.ReadLine().ToUpper());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string koneksi = "data source=MY-PREDATOR;"
                                + "initial catalog = {0};"
                                + "user ID={1} ;password={2}";
                                conn = new SqlConnection(string.Format(koneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. View All data");
                                        Console.WriteLine("2. Add Data");
                                        Console.WriteLine("3. Delete Data");
                                        Console.WriteLine("4. Update");
                                        Console.WriteLine("5. Search");
                                        Console.WriteLine("6. Exit");
                                        Console.WriteLine("Enter your choise(1-4):");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data student");
                                                    pr.read(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    string nim, nast, almt, date, noid;
                                                    Console.Clear();
                                                    Console.WriteLine("Input student data ");
                                                    Console.WriteLine("Input NIM:");
                                                    nim = Console.ReadLine();
                                                    Console.WriteLine("Input Student name:");
                                                    nast = Console.ReadLine();
                                                    Console.WriteLine("Input Student Addres:");
                                                    almt = Console.ReadLine();

                                                
                                                    Console.WriteLine("Input student Birth date: ");
                                                    date = Console.ReadLine();
                                                    
                                                    Console.WriteLine("Enter student department ID (numbers only):");
                                                    noid = Console.ReadLine();

                                                        
                                                    try
                                                    {
                                                        pr.insert(nim, nast, almt, date, noid, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to add data");
                                                    }

                                                }
                                                break;
                                            case '3':
                                                {
                                                    string nim;
                                                    Console.Clear();
                                                    Console.WriteLine("Input student data ");
                                                    Console.WriteLine("Input student id:");
                                                    nim = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(nim, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to delete data");
                                                    }
                                                }
                                                break;
                                            case '4':
                                                {
                                                    string nim, almt;
                                                    Console.Clear();
                                                    Console.WriteLine("Input student NIM that you want to cahnge their addres ");
                                                    nim = Console.ReadLine();
                                                    Console.WriteLine("Input the new Student Addres:");
                                                    almt = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.update(nim, almt, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to change data");
                                                    }
                                                }
                                                break;
                                            case '5':
                                                {
                                                    string nim;
                                                    Console.Clear();
                                                    Console.WriteLine("Welcome to search function ");
                                                    Console.WriteLine("Input student Id to search for ");
                                                    nim = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.search(nim, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n You dont have premission to search for data");
                                                    }
                                                }
                                                break;
                                            case '6':
                                                {
                                                    conn.Close();
                                                    return;
                                                }
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n Invalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Check the enterd value");
                                    }
                                }

                            }
                        default:
                            {
                                Console.WriteLine("\n Invalid option");

                            }
                            break;

                    }



                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This user dont have acces to database ");
                }
            }

        }

        public void read(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("select * from Student", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));

                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string nim, string nast, string almt, string date, string noid, SqlConnection con)
        {
            string str = "";
            str = "insert into Student (student_id,full_name,date_of_birth,address,department_id)" + " values(@nim,@nma,@date,@almat,@did)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", nim));
            cmd.Parameters.Add(new SqlParameter("nma", nast));
            cmd.Parameters.Add(new SqlParameter("almat", almt));
            cmd.Parameters.Add(new SqlParameter("date", date));
            cmd.Parameters.Add(new SqlParameter("did", noid));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data have been added");

        }

        public void delete(string nim, SqlConnection con)
        {
            string str = "";
            str = "delete from Student where student_id " + " = '" + nim + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data have been deleted");

        }

        public void update(string nim, string almt, SqlConnection con)
        {
            string str = "";

            str = "UPDATE Student SET address" + " = '" + almt + "' WHERE student_id " + "='" + nim + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data have been cahnged");

        }

        public void search(string nim, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand("select * from Student where student_id " + " = '" + nim + "'", con);
            SqlDataReader r = cmd.ExecuteReader();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This is what was found");

            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));

                }
                Console.WriteLine();
            }

        }

    }
}
