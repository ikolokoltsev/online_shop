using UserApp;
using ItemApp;

namespace TradeOfferApp;

enum TradeOfferStatus
{
    Pending,
    Accepted,
    Denied
}

class TradeOffer
{
    public string OfferId;
    public User OfferFrom;
    public User OfferTo;
    public List<Item> ItemsToTrade;
    public TradeOfferStatus Status = TradeOfferStatus.Pending;

    public TradeOffer(User offerFrom, User offerTo, List<Item> itemsToTrade)
    {
        OfferFrom = offerFrom;
        OfferTo = offerTo;
        OfferId = $"{offerFrom.Username}{offerTo.Username}";
        ItemsToTrade = itemsToTrade;
    }
}