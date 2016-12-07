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
    public string GetCuisine()
    {
      return _cuisine;
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (cuisine) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);
      SqlParameter cuisineParameter = new SqlParameter();
      cuisineParameter.ParameterName = "@CuisineName";
      cuisineParameter.Value = this.GetCuisine();
      cmd.Parameters.Add(cuisineParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
    }

    public static Cuisine Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @CuisineID;", conn);
      SqlParameter cuisineParameter = new SqlParameter();
      cuisineParameter.ParameterName = "@CuisineId";
      cuisineParameter.Value = Id.ToString();
      cmd.Parameters.Add(cuisineParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int cuisineId = 0;
      string cuisineType = null;

      while(rdr.Read())
      {
        cuisineId = rdr.GetInt32(0);
        cuisineType = rdr.GetString(1);
      }
      Cuisine foundCuisine = new Cuisine(cuisineType, cuisineId);

      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
      return foundCuisine;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.GetId() == newCuisine.GetId();
        bool cuisineEquality = this.GetCuisine() == newCuisine.GetCuisine();

        return(idEquality && cuisineEquality);
      }
    }

    public void Update(string newCuisine)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET cuisine = @NewCuisine OUTPUT INSERTED.cuisine WHERE id = @CuisineId;", conn);

      SqlParameter newCuisineParameter = new SqlParameter();
      newCuisineParameter.ParameterName = "@NewCuisine";
      newCuisineParameter.Value = newCuisine;
      cmd.Parameters.Add(newCuisineParameter);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._cuisine = rdr.GetString(0);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
    }
  }
}
