using System;
using System.Collections.Generic;

namespace StarWars.UI.Models
{
    public class EntityResults<T> : Entity where T : Entity
    {
        public string previous
        {
            get;
            set;
        }

        public string next
        {
            get;
            set;
        }

        public string previousPageNo
        {
            get;
            set;
        }

        public string nextPageNo
        {
            get;
            set;
        }

        public long count
        {
            get;
            set;
        }

        public List<T> results
        {
            get;
            set;
        }
    }
}