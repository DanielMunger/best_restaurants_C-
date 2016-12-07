using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisine_id;

    public Restaurant(string Name, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _cuisine_id = CuisineId;
    }

    public void SetId(int Id)
    {
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetName(string Name)
    {
      _name = Name;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetCuisineId(int CuisineId)
    {
      _cuisine_id = CuisineId;
    }

    public int GetCuisineId()
    {
      return _cuisine_id;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
      return allRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantCuisineId);", conn);

      SqlParameter restaurantNameParameter = new SqlParameter();
      restaurantNameParameter.ParameterName = "@RestaurantName";
      restaurantNameParameter.Value = this.GetName();
      cmd.Parameters.Add(restaurantNameParameter);

      SqlParameter restaurantCuisineIdParameter = new SqlParameter();
      restaurantCuisineIdParameter.ParameterName = "@RestaurantCuisineId";
      restaurantCuisineIdParameter.Value = this.GetCuisineId();
      cmd.Parameters.Add(restaurantCuisineIdParameter);
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
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      if(conn!=null)
      {
        conn.Close();
      }
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool restaurantIdEquality = (this.GetId() == newRestaurant.GetId());
        bool restaurantNameEquality = (this.GetName() == newRestaurant.GetName());
        bool restaurantCuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (restaurantNameEquality && restaurantCuisineIdEquality && restaurantIdEquality);
      }
    }

    
  }
}
