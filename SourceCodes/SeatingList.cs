using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class SeatingList
    {
        public string TableId { get; set; }
        public string PubId { get; set; }
        public int MaxNo { get; set; }
        public string TableName { get; set; }
        public string TableDescription { get; set; }

        public SeatingList(string tableId, string pubId, int maxNo, string tableName, string tableDescription)
        {
            TableId = tableId;
            PubId = pubId;
            MaxNo = maxNo;
            TableName = tableName;
            TableDescription = tableDescription;
        }
    }
}
