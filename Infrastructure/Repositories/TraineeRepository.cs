using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Cli.Infrastructue.DataStore;
using TraineeHub.Domain.Intreface;
using TraineeHub.Domain.Entities;
using TraineeHub.Infrastructure.Persistence;



namespace Infrastructure.Repositories 
{
    public class TraineeRepository : ITraineeRepositroy

    {
      //  private readonly JsonDataStore _stor;
        
        private readonly TraineeHubDbContext _stor;
        public TraineeRepository(TraineeHubDbContext stor)
        {
            _stor = stor;
            
        }

        public void Add(Trainee trainee)
        {
            try
            {
                //var data= _stor.LoadData();
                //data.Trainees.Add(trainee);
                //_stor.Save(data);
                _stor.Trainees.Add(trainee);
                _stor.SaveChanges();
                Console.WriteLine("The data was added and saved successfully.");

            }
            catch (Exception)
            {

                Console.WriteLine("No added");
            }

        }

        public List<Trainee> GetAll() { 
        //{ var data = _stor.LoadData();
        //   return data.Trainees;
            return _stor.Trainees.ToList();
        }

        public Trainee? GetById(Guid id)
        {
            // var data = _stor.LoadData();
            //return data.Trainees.FirstOrDefault(x => x.Id == id);
            return _stor.Trainees.FirstOrDefault(x => x.Id == id);

        }
        public Trainee? GetByEmail(string email)
        { 
            //var   data = _stor.LoadData();
            //return data.Trainees.FirstOrDefault(x => x.Email == email);
            return _stor.Trainees.FirstOrDefault(x => x.Email == email);
        }
    }
}
