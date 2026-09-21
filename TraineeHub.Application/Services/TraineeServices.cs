using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Appliction.Services
{
    public class TraineeServices
    {
        private readonly ITraineeRepositroy _repository;

        public TraineeServices(ITraineeRepositroy repository)
        {
            _repository = repository;
        }

        public void AddTrainee(string fullName, string email)
        {

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Enter Full Name");


            }
            var valid = new EmailAddressAttribute();
            if (string.IsNullOrWhiteSpace(email) || !valid.IsValid(email)|| !email.Contains(".")) {
                throw new ArgumentException("InVaild Email");
            }
            var existEmail = _repository.GetByEmail(email);
            if (existEmail != null) { 
            throw new InvalidOperationException("Email already exist");
                  }
            var trainee = new Trainee();
            trainee.FullName= fullName;
            trainee.Email= email;
           
            _repository.Add(trainee);
        }
        public List<Trainee> AllTrainee()
        {
            return _repository.GetAll();
        }
    }
}
