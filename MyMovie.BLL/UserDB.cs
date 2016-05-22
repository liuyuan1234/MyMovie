using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace MyMovie.BLL
{
    public class UserDB
    {
        public int CheckUserName(string userName)
        {
            string sql = "select u.id from users u where username='" + userName + "'";

            int id = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    using (IDataReader reader = database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveUser(string userName, string password)
        {
            string sql = "insert into users(username,password) values('" + userName + "','" + password + "')";
            int id = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    database.ExecuteNonQuery(cmd);
                    id = 0;
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Check(string userName, string password)
        {
            string sql = "select u.id from users u where username='"+ userName +"' and password ='" + password +"'";
                 int id = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    using (IDataReader reader = database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string GetUserName(int userID)
        {
            string sql = "select u.username from users u where id='" + userID + "'";

            string id = "";
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    using (IDataReader reader = database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            id = reader.GetString(0);
                        }
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
