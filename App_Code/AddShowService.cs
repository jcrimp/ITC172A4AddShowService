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

    public List<Artist> GetArtists()
    {
        List<Artist> artists = new List<Artist>();

        var arts = from a in ShowDB.Artists
                   select new { a.ArtistKey, a.ArtistName };

        
        foreach (var a in arts)
        {
            Artist art = new Artist();
            art.ArtistKey = a.ArtistKey;
            art.ArtistName = a.ArtistName;
            artists.Add(art);
        }

        return artists;
    }


    public List<Show> GetShows(int vKey)
    {
        List<Show> shows = new List<Show>();

        var shw = from s in ShowDB.Shows
                 where s.VenueKey == vKey
                 select new { s.ShowKey, s.ShowName, s.ShowDate, s.ShowTime, s.ShowTicketInfo };

        foreach (var sh in shw)
        {
            Show s = new Show();
            s.ShowKey = sh.ShowKey;
            s.ShowName = sh.ShowName;
            s.ShowDate = sh.ShowDate;
            s.ShowTime = sh.ShowTime;
            s.ShowTicketInfo = sh.ShowTicketInfo;
            shows.Add(s);
        }

        return shows;
    }
}
