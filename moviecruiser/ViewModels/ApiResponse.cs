using System;

namespace moviecruiser.ViewModels
{
    //Defines the format of response
    public class ApiResponse
    {
    public bool Success { get; set; }
    public string Message { get; set; }
    public Object Data { get; set; }
  }
}
