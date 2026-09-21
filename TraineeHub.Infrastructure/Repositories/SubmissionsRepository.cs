using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Cli.Infrastructue.DataStore;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
using TraineeHub.Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class SubmissionsRepository : ISubmissionsRepository

    {
        private readonly TraineeHubDbContext _stor;
        //  private readonly JsonDataStore _stor;
      
        

        public SubmissionsRepository(TraineeHubDbContext stor)
        {
            _stor = stor;
          
            

        }
        public void Add(Submission submission)
        {
            try
            {
                _stor.Submissions.Add(submission);
                _stor.SaveChanges();
                Console.WriteLine("The data was added and saved successfully.");

            }
            catch (Exception )
            {

                Console.WriteLine("No added");
            }

        }

        public List<Submission> GetAll()
        { 
            ;
            return _stor.Submissions.ToList();
        }

        public Submission? GetById(Guid id)
        {
             
            return _stor.Submissions.FirstOrDefault(s => s.Id == id);
        }

        public void SaveAll()
        {
            try
            {
                
                _stor.SaveChanges();
                Console.WriteLine("The data was saved successfully.");
            }
            catch (Exception)
            {
                Console.WriteLine("No saved");
            }
        }

        public void Update(Submission submission)
        {
            var existing = _stor.Submissions.FirstOrDefault(s => s.Id == submission.Id);
            if (existing != null)
            {
                
                existing.Notes = submission.Notes;
                existing.Stauts = submission.Stauts;
                existing.SubmittedAt = submission.SubmittedAt;
                _stor.SaveChanges();

            }
        }
    }



}
