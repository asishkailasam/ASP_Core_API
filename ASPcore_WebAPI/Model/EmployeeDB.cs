using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace ASPcore_WebAPI.Model
{
    public class EmployeeDB
    {
        string connectionstring = "Server=localhost;Port=3306;Database=employeedb;User ID =root;Password=Test@010203;";

        //----------------------------------INSERT---------------------------------------------------------
        public string InsertDB(Employee objcls)
        {
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_EmpInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Sp_Name", objcls.Name);
                        cmd.Parameters.AddWithValue("Sp_Address", objcls.Address);
                        cmd.Parameters.AddWithValue("Sp_Salary", objcls.Salary);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        return ("Insert Sucessfull");

                    }


                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }



        //-------------------------------------------Select---------------------------------------------------

        public List<Employee> SelectDB()
        {
            var list = new List<Employee>();
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_selectAll", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                list.Add(new Employee
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Name = sdr["Name"].ToString(),
                                    Address = sdr["Address"].ToString(),
                                    Salary = Convert.ToDecimal(sdr["Salary"])
                                });
                            }
                        }
                    }
                    return list;
                }
                catch (Exception)
                {
                    return list;
                }
            }



        }

        //--------------------------SelectBy ID--------------------------------------------

        public Employee SelectDetailsWithId(int id)
        {
            var getdata = new Employee();
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_selectProfile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Sp_Id", id);

                    con.Open();
                    MySqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        getdata = new Employee
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = sdr["Name"].ToString(),
                            Address = sdr["Address"].ToString(),
                            Salary = Convert.ToDecimal(sdr["Salary"]),
                        };
                    }

                    con.Close();
                    return getdata;
                }
                catch (Exception)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    throw;
                }
            
            }
        }

        //----------------------------------Delete--------------------------------

       
        public string DeleteDB(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Sp_Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return "Deleted successfully";
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return ex.Message.ToString();
                }
            }
        }
        //------------------------------------Update---------------------------------

        public string UpdateDB(Employee emp)
        {
            string retval = "";

            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Sp_Id", emp.Id);
                    cmd.Parameters.AddWithValue("Sp_Name", emp.Name);
                    cmd.Parameters.AddWithValue("Sp_Salary", emp.Salary);
                    cmd.Parameters.AddWithValue("Sp_Address", emp.Address);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    retval = "Ok...updated";
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return ex.Message;
                }
            }

            return retval;
        }





    }
}
