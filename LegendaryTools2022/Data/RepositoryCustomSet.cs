﻿
using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public interface IRepositoryCustomSet : IGenericRepository<CustomSets>
    {
        CustomSetsViewModel GetCustomSet();

    }
    public class RepositoryCustomSet : IRepositoryCustomSet
    {
        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public List<CustomSets> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomSetsViewModel GetCustomSet()
        {
            try
            {
                CustomSetsViewModel customSetsModel = new CustomSetsViewModel();

                using (var db = new DataContext())
                {
                    
                    customSetsModel.CustomSets = db.EntityCustomSets.ToList();                   


                    foreach(var item in customSetsModel.CustomSets)
                    {
                        if (item.Decks == null)
                            item.Decks = new List<Decks>();

                        item.Decks = db.EntityDecks.Where(x => x.SetId == item.SetId).ToList();
                        //foreach(var deck in item.Decks)
                        //{
                        //    if (deck.Cards == null)
                        //        deck.Cards = new List<Cards>();

                        //    deck.Cards = db.EntityCards.Where(x => x.DeckId == deck.DeckId).ToList();
                           
                        //}
                    }


                }
                return customSetsModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CustomSets GetOneById(long id)
        {
            throw new NotImplementedException();
        }

        public CustomSets GetOneByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(CustomSets model)
        {
            throw new NotImplementedException();
        }

        public void Update(CustomSets model)
        {
            throw new NotImplementedException();
        }

        public void Update(CustomSets model, CustomSets entity)
        {
            throw new NotImplementedException();
        }
    }
}
