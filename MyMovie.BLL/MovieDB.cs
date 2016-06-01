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
    public class MovieDB
    {
        public MovieDetailModel GetDetail(int id)
        {
            string sql = "select m.id,d.typename,m.name,m.movieurl,m.introduce,m.actors";
            sql += " from [Movies] m";
            sql += " inner join dbo.DicType d on m.typename=d.typeid ";
            sql += " where id=" + id;

            MovieDetailModel model = new MovieDetailModel();
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
        
        public List<MovieDetailModel> GetListByTypeName(string typename)
        {
            string sql = "select m.id,d.typename as typename,m.name,m.[MovieImg]";
            sql += " from [Movies] m";
            sql += " inner join dbo.DicType d on m.typename=d.typeid ";
            sql += " where m.typename='" + typename + "'";

            List<MovieDetailModel> list = new List<MovieDetailModel>();
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
                return list;
            }
            catch (Exception ex)
            {
                return new List<MovieDetailModel>();
            }
        }

        /// <summary>
        /// 最新的
        /// </summary>
        /// <returns></returns>
        public List<MovieDetailModel> GetNewMovies()
        {
            string sql = "select top 8  m.id,d.typename as typename,m.name,m.[MovieImg]  from [Movies] m  inner join dbo.DicType d on m.typename=d.typeid  order by createTime desc;";
            
            List<MovieDetailModel> list = new List<MovieDetailModel>();
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
                return list;
            }
            catch (Exception ex)
            {
                return new List<MovieDetailModel>();
            }
        }


        public List<MovieDetailModel> GetPopularMovies()
        {
            string sql = "select top 8  m.id,d.typename as typename,m.name,m.[MovieImg]  from [Movies] m  inner join dbo.DicType d on m.typename=d.typeid  order by score desc;";

            List<MovieDetailModel> list = new List<MovieDetailModel>();
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
                return list;
            }
            catch (Exception ex)
            {
                return new List<MovieDetailModel>();
            }
        }
    }
}
