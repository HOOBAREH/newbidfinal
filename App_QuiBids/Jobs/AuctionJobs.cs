using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_QuiBids.Models;
using DataLayer.Repository;
using Quartz;


namespace App_QuiBids.Jobs
{
    public class AuctionJobs : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var auctions = new AuctionRepo().GetAuctions().Where(x => !x.IsClose);
            //List<IndexAuction> list = new List<IndexAuction>();
            //List<string> listStr = new List<string>();
            foreach (var item in auctions)
            {
                var auction = item;
                bool result = false, startStatus = auction.StartStatus, isclose = auction.IsClose;
                TimeSpan timer = new TimeSpan();
                TimeSpan time = new TimeSpan();
                string colorStatus = "Black";
                //from database
                //Int64 truncated1 = auction.Auction_Time.Ticks;
                //Int64 adjusted = truncated1 << 24;
                //TimeSpan actual = TimeSpan.FromTicks(adjusted);

                var status = auction.Auction_Time.CompareTo(new TimeSpan(0, 0, 0));
                timer = auction.Auction_Time;
                if (!auction.IsClose)
                {
                    if (timer.CompareTo(new TimeSpan(0, 0, 0)) == 0)
                    {
                        time = timer;
                    }
                    else
                    {
                        time = timer.Subtract(TimeSpan.FromSeconds(1));
                    }
                    if (time.CompareTo(new TimeSpan(0, 0, 0)) == 0)//if time==0
                    {
                        if (timer.CompareTo(new TimeSpan(0, 0, 0)) == 0)
                        {
                            if (!auction.StartStatus)
                            {
                                startStatus = true;
                                time = TimeSpan.FromSeconds(auction.Close_Time);
                            }
                            else
                            {
                                isclose = true;
                            }
                        }
                    }

                    new AuctionRepo().UpdateTimer2(auction.Id, time, startStatus, isclose);

                }
                //colorStatus = startStatus == true ? "Red" : "Black";
                //var model = new IndexAuction
                //{
                //    Auction_Time = auction.Auction_Time.ToString(),
                //    Color = colorStatus,
                //    Id = auction.Id,
                //    Status = auction.StartStatus

                //};
                //list.Add(model);
            }
        }
    }
}