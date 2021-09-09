using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LegendaryTools2022.Data.DataTools;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryCard : IGenericRepository<Cards>
    {
        List<Cards> GetAllCards();
        List<Cards> GetAll(long deckId);

    }
    public class RepositoryCard : IRepositoryCard
    {
        public Task ClearData()
        {
            throw new NotImplementedException();
        }


        public List<Cards> GetAllCards()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var result = db.EntityCards.ToList();
                   
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Cards> GetAll()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var result = db.EntityCards.ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Cards> GetAll(long deckId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var result = db.EntityCards.Where(x=>x.Deck.DeckId == deckId).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Cards GetOneById(long id)
        {
            try
            {
                Cards resultModel = new Cards();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityCards.Where(x => x.CardId == id).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Cards GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Cards model)
        {
            throw new NotImplementedException();
        }

        public void Update(Cards model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var entity = GetOneById(model.CardId);
                    if (entity != null)
                    {
                        DataCompareResult dcr = DataTools.CompareObjects(model, entity);
                        if (dcr.ObjectIdentical == false)
                        {
                            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Cards model, Cards entity)
        {
            throw new NotImplementedException();
        }

        void IGenericRepository<Cards>.ClearData()
        {
            throw new NotImplementedException();
        }
    }
}
