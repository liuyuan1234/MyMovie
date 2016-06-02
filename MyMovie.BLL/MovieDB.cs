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
        //select top 8 * from [Movies] m order by id desc;

        //select top 8 * from [Movies] m order by pingfen desc;

        public MovieDetailModel GetDetail(int id)
        {
            string sql = "select m.id,d.typename,m.name,m.movieurl,m.introduce,m.actors,m.score";
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
            string sql = "select m.id,d.typename as typename,m.name,m.[MovieImg],m.Score";
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
                            MovieDetailModel model = new MovieDetailModel(reader);
                            model.MovieImg = "/Upload/img/" + model.MovieImg;
                            list.Add(model);
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
            string sql = "select top 8  m.id,d.typename as typename,m.name,m.[MovieImg],m.Score  from [Movies] m  inner join dbo.DicType d on m.typename=d.typeid  order by createTime desc;";
            
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
                            MovieDetailModel model = new MovieDetailModel(reader);
                            model.MovieImg = "/Upload/img/" + model.MovieImg;
                            list.Add(model);
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
            string sql = "select top 8  m.id,d.typename as typename,m.name,m.[MovieImg],m.Score  from [Movies] m  inner join dbo.DicType d on m.typename=d.typeid  order by score desc;";

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
                            MovieDetailModel model = new MovieDetailModel(reader);
                            model.MovieImg = "/Upload/img/" + model.MovieImg;
                            list.Add(model);
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
