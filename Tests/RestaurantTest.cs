using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_EmptyAtFirst()
    {
      int result = Restaurant.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveToDataBase()
    {
      Restaurant testRestaurant = new Restaurant("Pete's pies", 1);
      testRestaurant.Save();

      int result = Restaurant.GetAll().Count;

      Assert.Equal(1, result);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
    [Fact]
    public void Test_ReturnsTrueForSame()
    {
      Restaurant firstRestaurant = new Restaurant("Fire on the Mountain", 1);
      Restaurant secondRestaurant = new Restaurant("Fire on the Mountain", 1);
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
  }
}
