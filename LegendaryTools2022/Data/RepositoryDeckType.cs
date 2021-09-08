using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryDeckType : IGenericRepository<DeckTypes>
    {


    }
    public class RepositoryDeckType : IRepositoryDeckType
    {
        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<DeckTypes> GetAll()
        {
            try
            {
                var modelList = new List<DeckTypes>();
                using (var db = new DataContext())
                {
                    modelList = db.EntityDeckTypes.ToList();

                }
                return modelList;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public DeckTypes GetOneById(long id)
        {
            try
            {
                DeckTypes resultModel = new DeckTypes();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityDeckTypes.Where(x => x.DeckTypeId == id).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DeckTypes GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(DeckTypes model)
        {
            throw new NotImplementedException();
        }

        public void Update(DeckTypes model)
        {
            throw new NotImplementedException();
        }

        public void Update(DeckTypes model, DeckTypes entity)
        {
            throw new NotImplementedException();
        }
    }
}
