namespace WebApplication1.Pages
{
 
  

    public class PubList
    {
        public string PubName { get; set; }

        public string Street { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }
        public string PubId { get; set; }
        public PubList(string pubId, string pubName, string street, string town, string postcode, string open, string close)
        {
            PubId = pubId;
            PubName = pubName;
            Street = street;
            Town = town;
            Postcode = postcode;
            Open = open;
            Close = close;
        }
    }
}