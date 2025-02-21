using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactWebApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;


namespace ReactWebApplication
{

    public class Employee
    {
        public void EmployeeDetails()
        {
            Console.WriteLine("save and fetch employee details");
        }
        public void EMS() {
            Console.WriteLine("IN and OUT time");
        }
    }
    public class Trainee : BaseEmployee
    {
        public override void Salary()
        {
            throw new NotImplementedException();
        }

        public override void Work()
        {
            throw new NotImplementedException();
        }
    }
    public class TL : BaseEmployee
    {
        public override void Salary()
        {
            throw new NotImplementedException();
        }

        public override void Work()
        {
            throw new NotImplementedException();
        }
    }
    public class Manager : BaseEmployee, IFireoption
    {
        public void FireEmployee()
        {
            throw new NotImplementedException();
        }

        public override void Salary()
        {
            throw new NotImplementedException();
        }

        public override void Work()
        {
            throw new NotImplementedException();
        }
    }



    public abstract class BaseEmployee
    {
        public void EmployeeDetails()
        {
            Console.WriteLine("save and fetch employee details");
        }
        public void EMS()
        {
            Console.WriteLine("IN and OUT time");
        }
        public abstract void Work();
        public abstract void Salary();

    }

    public interface IFireoption
    {
        public void FireEmployee();
    }
    //work - same function name but different implementation
    //salary - same funtion name but differnt implementation

    public abstract class CommonClass
    {
 
        public string Active { get; set; }
        public long? Username { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }
        public string Modifiedon { get; set; }
        public string Modifiedby { get; set; }
        public string Ipaddress { get; set; }
    
        public abstract Task<DataTable> SelectCommond();
        public abstract Task<DataTable> SelectCommond(Int64 id);
        public abstract Task InsertCommond();
        public abstract Task UpdateCommond();
        public abstract Task<DataTable> GridLoad();
        public abstract Task<DataTable> GridLoad(Int64 id);
        public abstract Task DeleteCommond(Int64 id);


    }
   
}
