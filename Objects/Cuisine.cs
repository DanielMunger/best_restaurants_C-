using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _cuisine;

    public Cuisine(string Cuisine, int Id = 0)
    {
      _cuisine = Cuisine;
      _id = Id;
    }
    public void SetId(int Id)
    {
      _id = Id;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetCuisine(string Cuisine)
    {
      _cuisine = Cuisine;
    }
    public string GetString()
    {
      return _cuisine;
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
        allCuisines.Add(newCuisine);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
      return allCuisines;
    }
  }
}
