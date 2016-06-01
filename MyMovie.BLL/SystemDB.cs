using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMovie.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace MyMovie.BLL
{
    public class SystemDB
    {
        public int Check(string userName, string password)
        {
            string sql = "select u.id from admin u where username='" + userName + "' and password ='" + password + "'";
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
            string sql = "select u.username from admin u where id='" + userID + "'";

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

        public int DeleteOne(int id)
        {
            string sql = "delete from Movies where id=" + id;
            int redult = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    database.ExecuteNonQuery(cmd);

                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddOne(MovieDetailModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO [dbo].[Movies]");
            sql.AppendLine("([createTime]");
            sql.AppendLine(",[TypeName]");
            sql.AppendLine(",[Name]");
            sql.AppendLine(",[MovieUrl]");
            sql.AppendLine(",[MovieImg]");
            sql.AppendLine(",[Introduce]");
            sql.AppendLine(",[Actors])");
            sql.AppendLine("VALUES");
            sql.AppendLine("(getdate()");
            sql.AppendLine(",'"+model.typename+"'");
            sql.AppendLine(",'"+model.Name+"'>");
            sql.AppendLine(",'"+model.MovieUrl +"'");
            sql.AppendLine(",'"+model.MovieImg+"'");
            sql.AppendLine(",'"+model.Introduce+"'");
            sql.AppendLine(",'"+model.Actors+"')");

            int redult = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql.ToString()))
                {
                    database.ExecuteNonQuery(cmd);

                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdOne(MovieDetailModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE [liuyuanMovie].[dbo].[Movies]");
            sql.AppendLine("SET ");
            sql.AppendLine(",[TypeName]");
            sql.AppendLine(",[Name]");
            sql.AppendLine(",[MovieUrl]");
            sql.AppendLine(",[MovieImg]");
            sql.AppendLine(",[Introduce]");
            sql.AppendLine(",[Actors])");
            sql.AppendLine("VALUES");
            sql.AppendLine("(getdate()");
            sql.AppendLine(",[TypeName] = '"+model.typename+"'");
            sql.AppendLine(",[Name] ='"+model.Name+"'>");
            sql.AppendLine(",[MovieUrl] ='"+model.MovieUrl +"'");
            sql.AppendLine(",[MovieImg] ='"+model.MovieImg+"'");
            sql.AppendLine(",[Introduce] ='"+model.Introduce+"'");
            sql.AppendLine(",[Actors] ='"+model.Actors+"'");
            sql.AppendLine("where id= "+model.ID);

            int redult = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql.ToString()))
                {
                    database.ExecuteNonQuery(cmd);

                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<MovieDetailModel> GetListByTypeName(string typename,int pagesize,int pageindex,out int recordcount ,out int pagecount)
        {
            recordcount=0;
            pagecount=0;
            string sql = "select m.id,d.typename as typename,m.name,m.createTime";
            sql += " from [Movies] m";
            sql += " inner join dbo.DicType d on m.typename=d.typeid ";
            if (typename != String.Empty)
            {
                sql += " where m.typename='" + typename + "'";
            }
            sql += "  order by createTime desc;";
                
            List<MovieDetailModel> list = new List<MovieDetailModel>();
            List<MovieDetailModel> result = new List<MovieDetailModel>();
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    using (IDataReader reader = database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            list.Add(new MovieDetailModel(reader));
                        }
                    }
                }
                //return list;
            }
            catch (Exception ex)
            {
                return new List<MovieDetailModel>();
            }

            for(int i=(pageindex-1)*pagesize;i<list.Count()&&i<pageindex*pagesize;i++)
            {
                result.Add(list[i]);
            }
            recordcount=list.Count();
            pagecount=Convert.ToInt32(Math.Ceiling(recordcount*1.0/pagesize));
            return result;

        }

        public MovieDetailModel GetDetail(int id)
        {
            MovieDetailModel model = null;
            string sql = "select * from dbo.movies where id=" + id;
            try
            {
                Database database = DatabaseFactory.CreateDatabase("MainConnection");
                using (DbCommand cmd = database.GetSqlStringCommand(sql))
                {
                    using (IDataReader reader = database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            model = new MovieDetailModel(reader);
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                return new MovieDetailModel();
            }
        }
    }
}
