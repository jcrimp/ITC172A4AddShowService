using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddShowService" in code, svc and config file together.
public class AddShowService : IAddShowService
{
    ShowTrackerEntities ShowDB = new ShowTrackerEntities();
    public bool AddArtist(Artist a)
    {
        bool result = true;

        try
        {
            Artist artist = new Artist();
            artist.ArtistName = a.ArtistName;
            artist.ArtistWebPage = a.ArtistWebPage;
            artist.ArtistEmail = a.ArtistEmail;
            artist.ArtistDateEntered = DateTime.Now;
            ShowDB.Artists.Add(artist);

            ShowDB.SaveChanges();
        }
        catch
        {
            result = false;
        }
        

        return result;

    }

    public bool AddShow(Show s, ShowDetail sd)
    {
        bool result = true;

        try
        {
            int key;

            Show show = new Show();


            show.ShowName = s.ShowName;
            show.ShowDate = s.ShowDate;
            show.ShowTime = s.ShowTime;
            show.ShowTicketInfo = s.ShowTicketInfo;
            show.VenueKey = s.VenueKey;
            show.ShowDateEntered = DateTime.Now;
            ShowDB.Shows.Add(show);

            ShowDetail showDetail = new ShowDetail();
            if (sd.ArtistKey == 0)
            {
                key = ShowDB.Artists.Max(a => a.ArtistKey);
            }

            else
            {
                key = (int)sd.ArtistKey;
            }
            showDetail.ArtistKey = key;

            showDetail.Show = show;
            showDetail.ShowDetailAdditional = sd.ShowDetailAdditional;
            showDetail.ShowDetailArtistStartTime = sd.ShowDetailArtistStartTime;
            //showDetail.Artist = sd.Artist;
            ShowDB.ShowDetails.Add(showDetail);

            ShowDB.SaveChanges();
        }

        catch
        {
            result = false;
        }

        
        return result;
    }
}
