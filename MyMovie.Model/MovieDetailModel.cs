using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace MyMovie.Model
{
    public class MovieDetailModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 影片名称 疯狂动物城
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 视频文件地址  /Upload/Movies/fengkuangdongwucheng.mp4
        /// </summary>
        public string MovieUrl { get; set; }
        /// <summary>
        /// 图片地址  /Upload/Image/fengkuangdongwucheng.jpg
        /// </summary>
        public string MovieImg { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 演员
        /// </summary>
        public string Actors { get; set; }

        public string CreateTime { get; set; }

        public string typename { get; set; }

        public MovieDetailModel()
        {
            ID = 0;
        }

        public MovieDetailModel(IDataReader reader)
        {
            string columnName = String.Empty;
            for (var i = 0; i < reader.FieldCount; ++i)
            {
                if (reader.IsDBNull(i))
                {
                    continue;
                }
                columnName = reader.GetName(i).ToUpper(CultureInfo.InvariantCulture);
                switch (columnName)
                {
                    case "CREATETIME":
                        this.CreateTime = reader.GetDateTime(i).ToString("yyyy-MM-dd HH:mm");
                        break;
                    case "ID":
                        this.ID = reader.GetInt32(i);
                        break;
                    case "NAME":
                        this.Name = reader.GetString(i);
                        break;
                    case "TYPENAME":
                        this.typename = reader.GetString(i);
                        break;
                    case "MOVIEURL":
                        this.MovieUrl = reader.GetString(i);
                        break;
                    case "MOVIEIMG":
                        this.MovieImg = reader.GetString(i);
                        break;
                    case "INTRODUCE":
                        this.Introduce = reader.GetString(i);
                        break;
                    case "ACTORS":
                        this.Actors = reader.GetString(i);
                        break;
                }
            }
        }
    }
}
