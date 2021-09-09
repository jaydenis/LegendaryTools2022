using LegendaryCardEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Data
{
    public class UnitOfWork : IDisposable
    {
        private DataContext context = new DataContext();
        private GenericRepository<CustomSets> setsRepository;
        private GenericRepository<Decks> decksRepository;
        private GenericRepository<Cards> cardsRepository;
        private GenericRepository<DeckTypes> deckTypesRepository;
        private GenericRepository<CardTypes> cardTypesRepository;
        private GenericRepository<Templates> templatesRepository;

        public GenericRepository<CustomSets> CustomSetsRepository
        {
            get
            {

                if (this.setsRepository == null)
                {
                    this.setsRepository = new GenericRepository<CustomSets>(context);
                }
                return setsRepository;
            }
        }

        public GenericRepository<Decks> DecksRepository
        {
            get
            {

                if (this.decksRepository == null)
                {
                    this.decksRepository = new GenericRepository<Decks>(context);
                }
                return decksRepository;
            }
        }

        public GenericRepository<Cards> CardsRepository
        {
            get
            {

                if (this.cardsRepository == null)
                {
                    this.cardsRepository = new GenericRepository<Cards>(context);
                }
                return cardsRepository;
            }
        }

        public GenericRepository<DeckTypes> DeckTypesRepository
        {
            get
            {
                if (this.deckTypesRepository == null)
                {
                    this.deckTypesRepository = new GenericRepository<DeckTypes>(context);
                }
                return deckTypesRepository;
            }
        }

        public GenericRepository<CardTypes> CardTypesRepository
        {
            get
            {
                if (this.cardTypesRepository == null)
                {
                    this.cardTypesRepository = new GenericRepository<CardTypes>(context);
                }
                return cardTypesRepository;
            }
        }

        public GenericRepository<Templates> TemplatesRepository
        {
            get
            {
                if (this.templatesRepository == null)
                {
                    this.templatesRepository = new GenericRepository<Templates>(context);
                }
                return templatesRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
