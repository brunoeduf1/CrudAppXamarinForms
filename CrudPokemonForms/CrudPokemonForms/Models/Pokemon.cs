using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudPokemonForms.Models
{
    public class Pokemon
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string base_experience { get; set; }
        public string height { get; set; }
        public string weight { get; set; }

    }
}
