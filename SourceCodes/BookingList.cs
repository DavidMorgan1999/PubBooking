using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class BookingList
    {
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public string TableId { get; set; }
        public string PubId { get; set; }
        public string BookingDate { get; set; }
        public string BookingTime { get; set; }
        public string PubName { get; set; }
        public string TableName { get; set; }
        public string Address { get; set; }
        public string MaxNoSeats { get; set; }

        public BookingList(string bookingId, string userId, string tableId, string pubId, string bookingDate, string bookingTime, string pubName, string tableName, string address, string maxNoSeats)
        {
            BookingId = bookingId;
            UserId = userId;
            TableId = tableId;
            PubId = pubId;
            BookingDate = bookingDate;
            BookingTime = bookingTime;
            PubName = pubName;
            TableName = tableName;
            Address = address;
            MaxNoSeats = maxNoSeats;
        }
    }
}
