using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryCardType : IGenericRepository<CardTypes>
    {


    }
    public class RepositoryCardType : IRepositoryCardType
    {
        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<CardTypes> GetAll()
        {
            try
            {
                var modelList = new List<CardTypes>();
                using (var db = new DataContext())
                {
                    modelList = db.EntityCardTypes.ToList();

                }
                return modelList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public CardTypes GetOneById(long id)
        {
            try
            {
                CardTypes resultModel = new CardTypes();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityCardTypes.Where(x => x.CardTypeId == id).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CardTypes GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(CardTypes model)
        {
            throw new NotImplementedException();
        }

        public void Update(CardTypes model)
        {
            throw new NotImplementedException();
        }

        public void Update(CardTypes model, CardTypes entity)
        {
            throw new NotImplementedException();
        }
    }
}
