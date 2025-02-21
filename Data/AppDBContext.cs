using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;
using ReactWebApplication.Models.TreeView;
using ReactWebApplication.Models.Transactions;

namespace ReactWebApplication.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }
      //  private readonly IConfiguration configuration;
      //  private readonly string _connectionString;
      //  public AppDBContext(IConfiguration configuration, string connectionString)
      //  {
      //      this.configuration = configuration;
      //      _connectionString = connectionString;
      //  }
   
      //  public DbSet<Product> Products { get; set; }
      //  public DbSet<ProcessMasterModel> asptblpromas { get; set; }
      //  public DbSet<CountryMaster> gtcountrymast { get; set; }
      //  public DbSet<StateMaster> gtstatemast { get; set; }
      //  public DbSet<CityMaster> gtcitymast { get; set; }
      //  public DbSet<CompanyMaster> gtcompmast { get; set; }
      //  public DbSet<EmployeeMaster> EmployeeMaster { get; set; }
      //  //public DbSet<UserMaster> UserMaster { get; set; }
      ////  public DbSet<MenuNameMaster> asptblmenuname { get; set; }
      //  public DbSet<NavigationMaster> asptblnavigation { get; set; }
      //  public DbSet<UserRightsModel> asptbluserrights { get; set; }
      //  public DbSet<BuyerMasterModel> asptblbuymas { get; set; }
      //  public DbSet<SizeMaster> asptblsizmas { get; set; }
      //  public DbSet<SizeGroupMaster> asptblsizgrp { get; set; }
      //  public DbSet<SizeGroupDetMaster> asptblsizgrpdet { get; set; }
    
      //  public DbSet<ReactWebApplication.Models.Masters.StyleCategoryMaster> asptblstycatmas { get; set; }
      //  public DbSet<ReactWebApplication.Models.Masters.StyleGroupMaster> asptblstygrpmas { get; set; }
      //  public DbSet<ReactWebApplication.Models.Masters.StyleItemMaster> asptblstyleitemmas { get; set; }
      //  public DbSet<ReactWebApplication.Models.Masters.FabricTypeMaster> asptblfabrictypemas { get; set; }
      //  public DbSet<ReactWebApplication.Models.Masters.YarnBlendMaster> asptblyarblemas { get; set; }
      //  public DbSet<ReactWebApplication.Models.Masters.FabricMaster> asptblfabmas { get; set; }

      //  public DbSet<ReactWebApplication.Models.Registration.AutoGenerateMaster> asptblautogeneratemas { get; set; }

    }
}
