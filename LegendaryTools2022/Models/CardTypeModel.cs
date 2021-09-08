using Newtonsoft.Json;
using SQLite;
using System;

namespace LegendaryTools2022.Models
{
    public partial class CardTypeModel
    {
        [PrimaryKey, AutoIncrement]
        public Int64 ID { get; set; }
        public string CardTypeName { get; set; }

        public string CardTypeDisplayname { get; set; }

        public int DeckTypeId { get; set; }
    }
}
