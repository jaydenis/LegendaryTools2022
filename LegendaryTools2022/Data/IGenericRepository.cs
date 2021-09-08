using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Data
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        void Insert(TModel model);
        void Update(TModel model);
        void Update(TModel model, TModel entity);

        void ClearData();
        List<TModel> GetAll();
        TModel GetOneByName(string name);
        TModel GetOneById(long id);

    }
}
