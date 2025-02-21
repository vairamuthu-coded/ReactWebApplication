namespace ReactWebApplication.Models
{
    public interface IRespositroy
    {
       public Task <List<CountryMaster>> GetALL();
        public Task <CountryMaster> GetByID(Int64 id);
        public Task Create(CountryMaster cou);
        public Task Update(CountryMaster cou);
        public Task Detete(CountryMaster cou);
        public Task Save();
       
    }
}
