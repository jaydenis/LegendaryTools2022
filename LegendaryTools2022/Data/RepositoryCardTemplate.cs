using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryCardTemplate : IGenericRepository<Templates>
    {

    }
    public class RepositoryCardTemplate : IRepositoryCardTemplate
    {
        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<Templates> GetAll()
        {
            try
            {
                var modelList = new List<Templates>();
                using (var db = new DataContext())
                {
                    modelList = db.EntityTemplates.ToList();

                }
                return modelList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }      

        public Templates GetOneById(long id)
        {
            try
            {
                Templates resultModel = new Templates();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityTemplates.Where(x => x.TemplateId == id).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Templates GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Templates model)
        {
            throw new NotImplementedException();
        }

        public void Update(Templates model)
        {
            throw new NotImplementedException();
        }

        public void Update(Templates model, Templates entity)
        {
            throw new NotImplementedException();
        }
    }
}
