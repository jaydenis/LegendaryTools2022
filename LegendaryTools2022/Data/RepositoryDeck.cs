using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public Decks GetOneById(long id)
        {
            throw new NotImplementedException();
        }

        public Decks GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Decks model)
        {
            throw new NotImplementedException();
        }

        public void Update(Decks model)
        {
            throw new NotImplementedException();
        }

        public void Update(Decks model, Decks entity)
        {
            throw new NotImplementedException();
        }
    }
}
