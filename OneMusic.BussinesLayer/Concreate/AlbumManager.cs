﻿using OneMusic.BussinesLayer.Abstarct;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.DataAccesLayer.Concreate;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Concreate
{

    public class AlbumManager : IAlbumService
    {

        private readonly IAlbumDal _albumDal;

        public AlbumManager(IAlbumDal albumDal)
        {
            _albumDal = albumDal;
        }

        public void TCreate(Album entity)
        {
            _albumDal.Create(entity);
        }
    }

        public void TDelete(int id)
        {
        _albumDal.Delete(id);
    }

        public Album TGetById(int id)
        {
            return _albumdal..GetById(id);
    
        }

        public List<Album> TGetList()
        {
      return _albumDal.getlist();
    }

        public void TUpdate(Album entity)
        {
        _albumDal.Update(entity);
    }
    }
}