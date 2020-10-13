using System;
using DataLayer.Models;
using DataLayer.Repositiries;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly AminWebEntities _db = new AminWebEntities();

        private MainRepo<TblChat> _chat;
        private MainRepo<TblDocs> _docs;
        private MainRepo<TblPlaylist> _playlist;
        private MainRepo<TblRole> _role;
        private MainRepo<TblUser> _user;
        private MainRepo<TblUserPlaylistRel> _userPlaylistRel;
        private MainRepo<TblUserVideoRel> _userVideoRel;
        private MainRepo<TblVideo> _video;
        private MainRepo<TblVideoPlaylistKeyword> _videoPlaylistKeyword;
        private MainRepo<TblWithdraw> _withdraw;

        public MainRepo<TblChat> Chat => _chat ?? (_chat = new MainRepo<TblChat>(_db));

        public MainRepo<TblDocs> Docs => _docs ?? (_docs = new MainRepo<TblDocs>(_db));

        public MainRepo<TblPlaylist> Playlist => _playlist ?? (_playlist = new MainRepo<TblPlaylist>(_db));

        public MainRepo<TblRole> Role => _role ?? (_role = new MainRepo<TblRole>(_db));

        public MainRepo<TblUser> User => _user ?? (_user = new MainRepo<TblUser>(_db));

        public MainRepo<TblUserPlaylistRel> UserPlaylistRel => _userPlaylistRel ?? (_userPlaylistRel = new MainRepo<TblUserPlaylistRel>(_db));

        public MainRepo<TblUserVideoRel> UserVideoRel => _userVideoRel ?? (_userVideoRel = new MainRepo<TblUserVideoRel>(_db));

        public MainRepo<TblVideo> Video => _video ?? (_video = new MainRepo<TblVideo>(_db));

        public MainRepo<TblVideoPlaylistKeyword> VideoPlaylistKeyword => _videoPlaylistKeyword ?? (_videoPlaylistKeyword = new MainRepo<TblVideoPlaylistKeyword>(_db));

        public MainRepo<TblWithdraw> Withdraw => _withdraw ?? (_withdraw = new MainRepo<TblWithdraw>(_db));

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
