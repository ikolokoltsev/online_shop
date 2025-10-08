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

    public TradeOffer(User offerFrom, User offerTo, List<Item> itemsToTrade,  TradeOfferStatus? status = TradeOfferStatus.Pending)
    {
        OfferFrom = offerFrom;
        OfferTo = offerTo;
        OfferId = $"{offerFrom.Username}{offerTo.Username}";
        ItemsToTrade = itemsToTrade;
        Status = status ?? TradeOfferStatus.Pending;
    }

    public static void SaveTradeOfferToFile(TradeOffer tradeOffer, string filePath)
    {
        string trade_offer_items_lines = string.Join("|", tradeOffer.ItemsToTrade.Select(item => $"{item.ItemId}"));
        File.AppendAllLines(filePath,
            new[]
            {
                $"{tradeOffer.OfferFrom.Username},{tradeOffer.OfferTo.Username},{tradeOffer.Status},{trade_offer_items_lines}"
            });
    }
}