using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CrudOperationFinalAssessment
{
    class Student_Details
    {
        SqlConnection con = null;
        void Accept()
        {

            char choice, ch = 'y';

            do
            {

                Console.WriteLine("****** Student_Details ******");
                Console.WriteLine("Press 1. for Insetion : ");
                Console.WriteLine("Press 2. for Selection : ");
                Console.WriteLine("Press 3. for Deletion : ");
                Console.Write("Please Press the respective number of the operation, What you want to perform :\t");
                choice = Convert.ToChar(Console.ReadLine());



                if (choice == '1' || choice == '2' || choice == '3')
                {

                    createTable();

                    switch (choice)
                    {
                        case '1':

                            insert();
                            break;

                        case '2':

                            select();
                            break;

                        case '3':

                            delete();
                            break;

                        default:
                            Console.WriteLine("\n \t You have entered Invalid operation  :");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n \t Invalid operation entered :");
                }

                Console.WriteLine("\n Please press Y for continue Operations and N for Not :");
                ch = Convert.ToChar(Console.ReadLine());

            } while (ch == 'y');

            Console.ReadKey();
        }


        void createTable()
        {
            try
            {
                //Created the Connection
                con = new SqlConnection("data source =.;database=Students; integrated security=SSPI");

                SqlCommand cm = new SqlCommand("create table Student(studid int primary key,name varchar(25) not null,course varchar(25) not null, semester int not null)", con);
                con.Open();
                cm.ExecuteNonQuery();
                Console.WriteLine("\n \t Table created successfully in the SQL Server");



            }
            catch (Exception e)
            {
                Console.WriteLine("\n \t Something went wrong, while connecting or executing" + e.Message);
            }
            finally
            {
                con.Close();
            }

        }

        void insert()
        {
            int id, sem;
            string name, course;

            Console.Write("\n \t Enter the Student Id :\t");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n \t Enter the Student Name:\t");
            name = Console.ReadLine();
            Console.Write("\n \t Enter the Student Course:\t");
            course = Console.ReadLine();
            Console.Write("\n \t Enter the Student Current Semester:\t");
            sem = Convert.ToInt32(Console.ReadLine());



            // SqlConnection con = null;
            try
            {


                // writing sql query
                SqlCommand cm = new SqlCommand("insertstud", con);



                // Opening Connection
                con.Open();
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add(new SqlParameter("@studid", id));
                cm.Parameters.Add(new SqlParameter("@Name", name));
                cm.Parameters.Add(new SqlParameter("@Course", course));
                cm.Parameters.Add(new SqlParameter("@Semester", sem));



                // Executing the SQL query to retrive the values fromt he table
                int i = cm.ExecuteNonQuery();
                Console.WriteLine("\n \t" + i + "\t Record Inserted using Stored Procedure");

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e.Message);
            }
            // Closing the connection
            finally
            {
                con.Close();
            }

        }



        void select()
        {
            try
            {
                // Creating Connection
                con = new SqlConnection("data source=.; database=Students; integrated security=SSPI");

                // writing sql query
                SqlCommand cm = new SqlCommand("selectstud", con);



                // Opening Connection
                con.Open();



                // Executing the SQL query to retrive the values fromt he table
                SqlDataReader dr = cm.ExecuteReader();

                

                Console.WriteLine("\n: : Student Details : :");
                Console.WriteLine("\n\nStudId \t Name \t\t Course \t semester");
                while (dr.Read())
                {

                    Console.WriteLine(dr[0] + "\t " + dr[1] + "\t   " + dr[2] + "\t\t" + dr[3]);


                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e.Message);
            }
            // Closing the connection
            finally
            {
                con.Close();
            }
        }



        void delete()
        {
            try
            {
                // Creating Connection
                con = new SqlConnection("data source=.; database=Students; integrated security=SSPI");

                // writing sql query
                SqlCommand cm = new SqlCommand("deletestud", con);



                // Opening Connection
                con.Open();



                // Executing the SQL query to retrive the values fromt he table
                int i = cm.ExecuteNonQuery();
                Console.WriteLine("\n\tRecord Deleted Successfully:\t" + i);

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e.Message);
            }
            // Closing the connection
            finally
            {
                con.Close();
            }
        }



        static void Main(string[] args)
        {

            Student_Details s = new Student_Details();
            s.Accept();
            s.createTable();



            Console.ReadLine();

        }
    }
}

