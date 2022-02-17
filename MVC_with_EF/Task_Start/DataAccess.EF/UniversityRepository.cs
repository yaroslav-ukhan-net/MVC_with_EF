using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF
{
    public class UniversityRepository
    {
        //in this class add options
        internal readonly string connectinString;

        public UniversityRepository(string connectinString)
        {
            this.connectinString = connectinString;
        }
        internal SqlConnection GetConnection()
        {
            var connection = new SqlConnection(connectinString);
            connection.Open();
            return connection;
        }
    }
}
