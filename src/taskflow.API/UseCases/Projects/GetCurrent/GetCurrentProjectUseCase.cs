using System.ComponentModel.DataAnnotations;
using taskflow.API.Contracts;
using taskflow.API.Entities;

namespace taskflow.API.UseCases.Projects.GetCurrent
{
    public class GetCurrentProjectUseCase
    {
        private readonly IProjectRepository _repository;

        public GetCurrentProjectUseCase(IProjectRepository repository) => _repository = repository;

        public List<Project>? Execute()
        {
           return _repository.GetCurrent();
        }

        public Project? GetCurrentId(int id) {

            Validator(id);
        
            return _repository.GetCurrentId(id);
        }

        private void Validator(int id) {

            if (id <= 0)
            {
                throw new NotImplementedException("Projeto deve ter Id maior que zero!");
            }
        }
    }
}
