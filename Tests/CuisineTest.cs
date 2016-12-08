using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_EmptyAtFirst()
    {
      int result = Cuisine.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveToDataBase()
    {
      Cuisine testCuisine = new Cuisine("Italian");
      testCuisine.Save();

      int result = Cuisine.GetAll().Count;

      Assert.Equal(1, result);
    }
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
    [Fact]
    public void Test_ReturnsTrueForSame()
    {
      Cuisine firstCuisine = new Cuisine("Sushi");
      Cuisine secondCuisine = new Cuisine("Sushi");
      Assert.Equal(firstCuisine, secondCuisine);
    }
    [Fact]
    public void Test_IdPassesCorrectly()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      int result = testCuisine.GetId();
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Find()
    {
      Cuisine newCuisine = new Cuisine("Mexican");

      newCuisine.Save();
      Cuisine testCuisine = Cuisine.Find(newCuisine.GetId());

      Assert.Equal(testCuisine, newCuisine);
    }
    [Fact]
    public void Test_Update_CuisineInDataBase()
    {
      string cuisine = "sushi";
      Cuisine testCuisine =  new Cuisine(cuisine);
      testCuisine.Save();

      string newCuisine = "Mexican";
      testCuisine.Update(newCuisine);
      string result = testCuisine.GetCuisine();
      
      Assert.Equal(newCuisine, result);
    }
    [Fact]
    public void Test_Delete()
    {
      Cuisine newCuisine = new Cuisine("Sushi");
      newCuisine.Save();
      Cuisine newCuisine2 = new Cuisine("Mexican");
      newCuisine2.Save();

      newCuisine.Delete();
      List<Cuisine> list = Cuisine.GetAll();
      List<Cuisine> list2 = new List<Cuisine> {newCuisine2};

      Assert.Equal(list, list2);
    }
  }
}
