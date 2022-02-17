using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    public class HomeTaskAssessmentRepository : RepositoryBase, IRepository<HomeTaskAssessment>
    {
        public HomeTaskAssessmentRepository(string connectionString) : base(connectionString)
        {

        }
        public HomeTaskAssessment Create(HomeTaskAssessment home)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"
                INSERT INTO [dbo].[HomeTaskAssessment]
                ([IsComplete]
                ,[Date]
                ,[HomeTaskId]
                ,[StudentId])
            VALUES
                (@IsComplete
                ,@Date
                ,@HomeTaskId
                ,@StudentId);
SELECT CAST(scope_identity() AS int)", connection);
                sqlCommand.Parameters.AddWithNullableValue("@IsComplete", home.IsComplete);
                sqlCommand.Parameters.AddWithNullableValue("@Date", home.Date);
                sqlCommand.Parameters.AddWithNullableValue("@HomeTaskId", home.HomeTaskId);
                sqlCommand.Parameters.AddWithNullableValue("@StudentId", home.StudentId);

                int identity = (int)sqlCommand.ExecuteScalar();
                if(identity == 0)
                {
                    return null;
                }
                home.Id = identity;
            }
            return home;
        }

        public List<HomeTaskAssessment> GetAll()
        {
            List<HomeTaskAssessment> result = new List<HomeTaskAssessment>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    @"
                    SELECT [Id]
                    ,[IsComplete]
                    ,[Date]
                    ,[HomeTaskId]
                    ,[StudentId]
                    FROM [dbo].[HomeTaskAssessment]", connection);

                using var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    HomeTaskAssessment homeTaskAssessment = new HomeTaskAssessment();
                    homeTaskAssessment.Id = reader.GetInt32(0);
                    homeTaskAssessment.IsComplete = reader.GetBoolean(1);
                    homeTaskAssessment.Date = reader.GetDateTime(2);
                    homeTaskAssessment.HomeTaskId = reader.GetInt32(3);
                    homeTaskAssessment.StudentId = reader.GetInt32(4);
                    result.Add(homeTaskAssessment);
                }

            }
            return result;
        }

        public HomeTaskAssessment GetById(int id)
        {
            return this.GetAll().SingleOrDefault(HomeTaskAssessment => HomeTaskAssessment.Id == id);

        }

        public void Remove(int id)
        {
            using SqlConnection connection = GetConnection();
            SqlCommand sqlCommand = new SqlCommand(
                $@"DELETE FROM [dbo].[HomeTaskAssessment]
                WHERE Id = {id}", connection);
            sqlCommand.ExecuteNonQuery();
        }

        public void Update(HomeTaskAssessment home)
        {
            using SqlConnection connection = GetConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                using SqlCommand sqlCommand = new SqlCommand(@"
                UPDATE [dbo].[HomeTaskAssessment]
                    SET [IsComplete] = @IsComplete 
                        ,[Date] = @Date
                    WHERE Id = @Id", connection, transaction);
                sqlCommand.Parameters.AddWithNullableValue("@IsComplete", home.IsComplete);
                sqlCommand.Parameters.AddWithNullableValue("@Date", home.Date);
                sqlCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
