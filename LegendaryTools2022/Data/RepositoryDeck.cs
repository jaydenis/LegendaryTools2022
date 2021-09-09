using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


using static LegendaryTools2022.Data.DataTools;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryDeck : IGenericRepository<Decks>
    {


    }

    public class RepositoryDeck : IRepositoryDeck
    {
        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<Decks> GetAll()
        {
            try
            {
                List<Decks> resultModel = new List<Decks>();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityDecks.Include(o => o.Cards).ToList();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Decks GetOneById(long id)
        {
            try
            {
                Decks resultModel = new Decks();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityDecks.Where(x => x.DeckId == id).Include(o=>o.Cards).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Decks GetOneByName(string name)
        {
            try
            {
                Decks resultModel = new Decks();

                using (var db = new DataContext())
                {

                    resultModel = db.EntityDecks.Where(x => x.DeckName == name || x.DeckDisplayName == name).Include(o => o.Cards).FirstOrDefault();
                }
                return resultModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Insert(Decks model)
        {
            throw new NotImplementedException();
        }

        public void Update(Decks model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var entity = GetOneById(model.DeckId);
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

        public void Update(Decks model, Decks entity)
        {
            throw new NotImplementedException();
        }
    }
}
