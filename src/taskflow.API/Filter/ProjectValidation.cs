using taskflow.API.Contracts;
using taskflow.API.Exceptions;

namespace taskflow.API.Filter
{
    public class ProjectValidation
    {
        private IProjectRepository _repository;

        public ProjectValidation(IProjectRepository repository) => _repository = repository;

        public bool GetProjectValidation(int projectId)
        {
            try
            {
                var exist = _repository.ExistProjectWithId(projectId);

                //if(exist == false)
                //{
                //    throw new Exception("Projeto invalido!");
                //}


            return exist;
            }
            catch (NotFoundException ex) {
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
