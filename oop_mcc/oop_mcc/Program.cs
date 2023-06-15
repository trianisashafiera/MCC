using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace oop_mcc
{
    public class Program
    {
        static string connectionString = "Data Source=LAPTOP-KCLNIRVS;Database = db_hr2; Integrated Security=True;Connect Timeout=30;";
        static SqlConnection connection;

        public static void Main(string[] args)
        {
            connection = new SqlConnection(connectionString);

            /* List<Region> regions = GetAllRegion();
             foreach (Region region in regions)
             {
                 Console.WriteLine("Id :", +region.Id + ", Name : " + region.Name);
             }*/


            //InsertRegion
            Console.WriteLine("Insert");
            Console.Write("Masukkan nama region : ");
            string name = Console.ReadLine();
            int isInsertSuccessful = InserRegion(name);
            if (isInsertSuccessful > 0)
            {
                Console.WriteLine("Data berhasil ditambahkan!");
            }
            else
            {
                Console.WriteLine("Data gagal ditambahkan!");
            }


            //GetById Region
            Console.WriteLine("Get by ID");
            Console.Write("Masukkan ID region: ");
            int id = Convert.ToInt32(Console.ReadLine());
            GetRegionById(id);


            //Update Region
            Console.WriteLine("Update");
            Console.Write("Masukkan ID region yang akan diperbarui: ");
            int id_update;
            bool isIdValid = int.TryParse(Console.ReadLine(), out id_update);

            if (isIdValid)
            {
                Console.Write("Masukkan nama baru: ");
                string name_update = Console.ReadLine();
                int isUpdateRegionSuccessful = UpdateRegion(id_update, name_update);

                if (isUpdateRegionSuccessful > 0)
                {
                    Console.WriteLine("Data berhasil diperbarui!");
                }
                else
                {
                    Console.WriteLine("Data gagal diperbarui!");
                }
            }
            else
            {
                Console.WriteLine("ID yang dimasukkan tidak valid!");
            }

            /* // DeleteRegion
             Console.WriteLine("Delete");
             Console.Write("Masukkan ID region yang akan dihapus: ");
             int id_delete = Convert.ToInt32(Console.ReadLine());
             bool isDeleteSuccessful = DeleteRegion(id_delete);
             if (isDeleteSuccessful)
             {
                 Console.WriteLine("Data berhasil dihapus!");
             }
             else
             {
                 Console.WriteLine("Data tidak ditemukan atau gagal dihapus!");
             }*/

            Console.WriteLine("DELETE");
            Console.Write("Masukkan ID Region yang ingin dihapus: ");
            int id_delete = Convert.ToInt32(Console.ReadLine());
            bool isDeleteSuccessful = DeleteRegion(id_delete);
            if (isDeleteSuccessful)
            {
                Console.WriteLine("Data Berhasil Dihapus!");
            }
            else
            {
                Console.WriteLine("Data Gagal Dihapus atau Region Not Found!");
            }

            //InsertCountries
            Console.WriteLine("Insert");
            Console.WriteLine("Masukkan id country :");
            string id_country = Console.ReadLine();
            Console.Write("Masukkan Nama Country : ");
            string country_name = Console.ReadLine();
            Console.WriteLine("Masukkan region_id: ");
            int region_id = Convert.ToInt32(Console.ReadLine());
            int isInsertSuccessful1 = InserCountries(id_country,country_name, region_id);
            if (isInsertSuccessful1 > 0)
            {
                Console.WriteLine("Data berhasil ditambahkan!");
            }
            else
            {
                Console.WriteLine("Data gagal ditambahkan!");
            }

            //GetById Country
            Console.WriteLine("Get by ID");
            Console.Write("Masukkan ID Country: ");
            string idcountry = Console.ReadLine();
            GetCountryById(idcountry);

            //Update Country
            Console.WriteLine("Update");
            Console.Write("Masukkan ID Country yang akan diperbarui: ");
            string idCountry_update = Console.ReadLine();
            Console.WriteLine("Masukkan nama country yang akan diperbarui: ");
            string countryname_update = Console.ReadLine();
            Console.WriteLine("masukkan region id yang akan diperbarui:");
            int idRegion_update = Convert.ToInt32(Console.ReadLine());
            bool isUpdateSuccessful = UpdateCountry(idCountry_update, countryname_update, idRegion_update);

                if (isUpdateSuccessful)
                {
                    Console.WriteLine("Data berhasil diperbarui!");
                }
                else
                {
                    Console.WriteLine("Data gagal diperbarui!");
                }
            
            

            //Delete Country
            Console.WriteLine("DELETE");
            Console.Write("Masukkan ID Region yang ingin dihapus: ");
            string idCountry_delete = Console.ReadLine();
            bool isDeleteSuccessful1 = DeleteCountry(idCountry_delete);
            if (isDeleteSuccessful)
            {
                Console.WriteLine("Data Berhasil Dihapus!");
            }
            else
            {
                Console.WriteLine("Data Gagal Dihapus atau Region Not Found!");
            }

            //GetAll Employee
            Console.WriteLine("Get All Employee");
            List<Employee> employees = GetAllEmployee();
            foreach (Employee employee in employees)
            {
                Console.WriteLine("Id: " + employee.Id + ", Firstname: " + employee.first_name + ", Last_Name: " + employee.last_name + ", Email: " + employee.email + ", Phone_Number: " + employee.phone_number + ", Hire_Date: " + employee.hire_date +
                    ", Salary: " + employee.salary + ", Comission_Pct: " + employee.comission_pct + ", Manager_Id: " + employee.manager_id +
                    ", Job_Id: " + employee.job_id + ", Department_Id: " + employee.department_id);
            }

            //GetAll Location
            Console.WriteLine("Get All Location");
            List<Location> locations = GetAllLocation();
            foreach (Location location in locations)
            {
                Console.WriteLine("Id: " + location.Id + ", street_address: " + location.street_address + ", city: " + location.city);
            }

            //GetAll Jobs
            Console.WriteLine("Get All Jobs");
            List<Jobs> jobs = GetAllJobs();
            foreach (Jobs job in jobs)
            {
                Console.WriteLine("Id: " + job.Id + ", title: " + job.title + ", min salary: " + job.min_salary + ",maks salary : " + job.max_salary);
            }

            List<Department> departments = GetAllDepartments();
            foreach (Department department in departments)
            {
                Console.WriteLine("Id: " + department.Id + ", name: " + department.name + ", location id: " + department.location_id);
            }

        }


        /* Vehicle kapal = new Vehicle("yacht", "speed boat", "white");
        /*car.name = "BMW Z4";
        car.type = "Sport";
        car.color = "Red";
        kapal.Spesification();
        Wheel motorcycle = new Wheel(2, "Mio", "Scootic", "Blue");
        motorcycle.Spesification();

        Wheel car = new Wheel(4, "Avanza", "Family", "Silver");
        car.Spesification();

        ISound sound1 = new Implementsound();
        sound1.VehicleSoundCar(car.name);

        Pricelist price1 = new Pricelist(50000, "Revo", "Kurir", "Black");
        price1.Spesification();
        price1.PriceofVehicle(0.5);

        Wheel plane = new Wheel(6, "747-300", "Boeing", "White");
        plane.VehicleSoundPlane(plane.name);
        ISound sound2 = new Implementsound();
        sound2.VehicleSoundPlane(plane.name);
    }
    */

        //GetAll
        static List<Region> GetAllRegion()
        {
            var region = new List<Region>();
            try
            {
                connection = new SqlConnection(connectionString);

                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tb_m_regions";

                //Membuka koneksi
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var reg = new Region();
                        reg.Id = reader.GetInt32(0);
                        reg.Name = reader.GetString(1);

                        /*     region.Add(reg);*/

                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return region;
        }

        public static int InserRegion(string name)
        {
            int result = 0;
            connection = new SqlConnection(connectionString);

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "Insert Into tb_m_regions (name) VALUES (@name)";
                command.Transaction = transaction;

                //membuat parameter
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.Value = name;
                pName.SqlDbType = SqlDbType.VarChar;

                //Menambahkan parameter is command
                command.Parameters.Add(pName);

                //menjalankan command
                result = command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }
            connection.Close();
            return result;
        }

        public static void GetRegionById(int id)
        {
            connection.Open();

            try
            {
                // Membuat instance command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id";

                // Membuat parameter
                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@id";
                pRegionId.Value = id;
                pRegionId.SqlDbType = SqlDbType.Int;

                // Menambahkan parameter ke command
                command.Parameters.Add(pRegionId);

                // Mengeksekusi command dan membaca hasil
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        Console.WriteLine("ID: " + Id + ", Nama Region: " + name);
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Update Region
        public static int UpdateRegion(int id_update, string name_update)
        {
            int result = 0;

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE tb_m_regions SET name = @name WHERE id = @id, @name";

                SqlParameter pId = new SqlParameter("@id", SqlDbType.Int);
                pId.Value = id_update;
                command.Parameters.Add(pId);

                SqlParameter pName = new SqlParameter("@name", SqlDbType.VarChar);
                pName.Value = name_update;
                command.Parameters.Add(pName);

                result = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }


        /* public static bool DeleteRegion(int id_delete)
         {
             bool result = false;
             connection = new SqlConnection(connectionString);

             connection.Open();

             SqlTransaction transaction = connection.BeginTransaction();
             try
             {
                 // Membuat instance untuk command
                 SqlCommand command = new SqlCommand();
                 command.Connection = connection;
                 command.CommandText = "DELETE FROM tb_m_regions WHERE id = @id";
                 command.Transaction = transaction;

                 // Membuat parameter
                 SqlParameter pRegionId = new SqlParameter();
                 pRegionId.ParameterName = "@id";
                 pRegionId.Value = id_delete;
                 pRegionId.SqlDbType = SqlDbType.Int;

                 // Menambahkan parameter ke command
                 command.Parameters.Add(pRegionId);

                 int rowsAffected = command.ExecuteNonQuery();
                 transaction.Commit();
                 if (rowsAffected > 0)
                 {
                     result = true;
                 }
             }

             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
                 try
                 {
                     transaction.Rollback();
                 }
                 catch (Exception rollback)
                 {
                     Console.WriteLine(rollback.Message);
                 }
             }
             connection.Close();
             return result;
         }*/

        static bool DeleteRegion(int id_delete)
        {
            bool result = false;
            connection = new SqlConnection(connectionString);

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM tb_m_regions WHERE id = @id";
                command.Transaction = transaction;

                SqlParameter pIdRegion = new SqlParameter();
                pIdRegion.ParameterName = "@id";
                pIdRegion.Value = id_delete;
                pIdRegion.SqlDbType = SqlDbType.Int;

                command.Parameters.Add(pIdRegion);

                int rowsAffected = command.ExecuteNonQuery();
                transaction.Commit();

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }

            connection.Close();
            return result;
        }

        public static int InserCountries(string id, string countries_name, int region_id)
        {
            int result = 0;
            connection = new SqlConnection(connectionString);

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "Insert Into tb_m_countries (name) VALUES (@id, @name, @region_id)";
                command.Transaction = transaction;

                //membuat parameter
                SqlParameter pIdCountry = new SqlParameter();
                pIdCountry.ParameterName = "@id";
                pIdCountry.Value = id;
                pIdCountry.SqlDbType = SqlDbType.VarChar;

                SqlParameter pNameCountry = new SqlParameter();
                pNameCountry.ParameterName = "@name";
                pNameCountry.Value = countries_name;
                pNameCountry.SqlDbType = SqlDbType.VarChar;

                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@region_id";
                pRegionId.Value = region_id;
                pRegionId.SqlDbType = SqlDbType.Int;

                //Menambahkan parameter is command
                command.Parameters.Add(pIdCountry);
                command.Parameters.Add(pNameCountry);
                command.Parameters.Add(pRegionId);

                //menjalankan command
                result = command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }
            connection.Close();
            return result;
        }

        public static void GetCountryById(string id)
        {
            connection.Open();

            try
            {
                // Membuat instance command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tb_m_country WHERE id = @id";

                // Membuat parameter
                SqlParameter pCountryId = new SqlParameter();
                pCountryId.ParameterName = "@id";
                pCountryId.Value = id;
                pCountryId.SqlDbType = SqlDbType.VarChar;

                // Menambahkan parameter ke command
                command.Parameters.Add(pCountryId);

                // Mengeksekusi command dan membaca hasil
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string Id = reader.GetString(0);
                        string name = reader.GetString(1);
                        int regionId = reader.GetInt32(2);

                        /*  Console.WriteLine("ID: " + Id + ", Nama Country: " + name);*/
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Update Region
        public static bool UpdateCountry(string id_update, string name_update, int region_id)
        {
            bool result = false;

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE tb_m_country SET name = @name, region_id = @region_id WHERE id = @id";
                
                SqlParameter pIdCountry = new SqlParameter("@id", SqlDbType.Int);
                pIdCountry.Value = id_update;
                command.Parameters.Add(pIdCountry);

                SqlParameter pNameCountry = new SqlParameter("@name", SqlDbType.VarChar);
                pNameCountry.Value = name_update;
                command.Parameters.Add(pNameCountry);

                SqlParameter pIdRegion = new SqlParameter("@id", SqlDbType.Int);
                pIdRegion.Value = region_id;
                command.Parameters.Add(pIdRegion);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        static bool DeleteCountry(string id)
        {
            bool result = false;
            connection = new SqlConnection(connectionString);

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM tb_m_country WHERE id = @id";
                command.Transaction = transaction;

                SqlParameter pIdCountry = new SqlParameter();
                pIdCountry.ParameterName = "@id";
                pIdCountry.Value = id;
                pIdCountry.SqlDbType = SqlDbType.Int;

                command.Parameters.Add(pIdCountry);

                int rowsAffected = command.ExecuteNonQuery();
                transaction.Commit();

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception rollback)
                {
                    Console.WriteLine(rollback.Message);
                }
            }

            connection.Close();
            return result;
        }


        //GetAll Employee
        static List<Employee> GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                connection = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM employees";

                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.Id = reader.GetInt32(0);
                        emp.first_name = reader.GetString(1);
                        emp.last_name = reader.GetString(2);
                        emp.email = reader.GetString(3);
                        emp.phone_number = reader.GetString(4);
                        emp.hire_date = reader.GetDateTime(5);
                        emp.salary = reader.GetInt32(6);
                        emp.comission_pct = reader.GetDecimal(7);
                        emp.manager_id = reader.GetInt32(8);
                        emp.job_id = reader.GetString(9);
                        emp.department_id = reader.GetInt32(10);

                        employees.Add(emp);



                        employees.Add(emp);
                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return employees;
        }

        static List<Location> GetAllLocation()
        {
            var location = new List<Location>();
            try
            {
                connection = new SqlConnection(connectionString);

                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM locations";

                //Membuka koneksi
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var loc = new Location();
                        loc.Id = reader.GetInt32(0);
                        loc.street_address = reader.GetString(1);
                        loc.city = reader.GetString(2);

                        location.Add(loc);

                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return location;
        }

        static List<Jobs> GetAllJobs()
        {
            var job = new List<Jobs>();
            try
            {
                connection = new SqlConnection(connectionString);

                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM locations";

                //Membuka koneksi
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var job1 = new Jobs();
                        job1.Id = reader.GetInt32(0);
                        job1.title = reader.GetString(1);
                        job1.min_salary = reader.GetInt32(2);
                        job1.max_salary = reader.GetInt32(3);

                        job.Add(job1);

                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return job;
        }

        static List<Department> GetAllDepartments()
        {
            var department = new List<Department>();
            try
            {
                connection = new SqlConnection(connectionString);

                //Membuat instance untuk command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM locations";

                //Membuka koneksi
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dep = new Department();
                        dep.Id = reader.GetInt32(0);
                        dep.name = reader.GetString(1);
                        dep.location_id = reader.GetInt32(2);

                        department.Add(dep);

                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return department;
        }
    }

    class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int region_id { get; set; }
    }
    class Employee
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public DateTime hire_date { get; set; }
        public int salary { get; set; }
        public decimal comission_pct { get; set; }
        public int manager_id { get; set; }
        public string job_id { get; set; }
        public int department_id { get; set; }
    }
    class Location
    {
        public int Id { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
    }
    class Jobs
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int min_salary { get; set; }
        public int max_salary { get; set; }
    }
    class Department
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int location_id { get; set; }
    }
}
