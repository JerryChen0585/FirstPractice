using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstPractice.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryDataContext
    {
        static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //讀取所有產品分類資料
        public static List<Category> LoadCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                String strCmd = "select CategoryID,CategoryName,Description from Categories";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Category _category = new Category();
                        _category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                        _category.CategoryName = dr["CategoryName"].ToString();
                        _category.Description = dr["Description"].ToString();
                        categories.Add(_category);
                    }
                    dr.Close();
                    conn.Close();
                }

            }
            return categories;
        }
        public static void InsertCategory(Category category)
        {
            string strCmd = "insert Categories(CategoryName,Description)values(@CategoryName,@Description)";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", category.Description);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public static void DeleteCategory(int CategoryID)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                String strCmd = "Delete Categories where CategoryID=@CategoryID";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        //根據分類編號讀取要修改的產品分類名稱
        public static Category LoadCategoryByID(int CategoryID)
        {
            Category _category = new Category();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                String strCmd = "select CategoryID,CategoryName,Description from Categories where CategoryID=@CategoryID";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        _category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                        _category.CategoryName = dr["CategoryName"].ToString();
                        _category.Description = dr["Description"].ToString();
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            return _category;
        }
        //修改產品分類資料
        public static void EditCategory(Category category)
        {
            string strCmd = "update Categories set CategoryName=@CategoryName,Description=@Description where CategoryID=@CategoryID";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", category.Description);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}