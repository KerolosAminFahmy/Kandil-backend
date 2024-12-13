using kandil.Domain.Entities;
using Kandil.Application.RepositoryInterfaces;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Infrastructure.RepositoryImplementation
{
    public class UnitOfWork(ApplicationDbContext _context) : IUnitOfWork
    {
        private IBaseRepository<City> _City;
        public IBaseRepository<City> City
            {
                get
                {
                    return _City ??= new BaseRepository<City>(_context);
                }
             }
        private IBaseRepository<area> _area;
        public IBaseRepository<area> area
        {
            get
            {
                return _area ??= new BaseRepository<area>(_context);
            }
        }

        private IBaseRepository<Project> _Project;
        public IBaseRepository<Project> Project
        {
            get
            {
                return _Project ??= new BaseRepository<Project>(_context);
            }
        }
        private IBaseRepository<ProjectImage> _ProjectImages;
        public IBaseRepository<ProjectImage> ProjectImages
        {
            get
            {
                return _ProjectImages ??= new BaseRepository<ProjectImage>(_context);
            }
        }
        private IBaseRepository<AdvantageProject> _AdvantageProjects;
        public IBaseRepository<AdvantageProject> AdvantageProjects
        {
            get
            {
                return _AdvantageProjects ??= new BaseRepository<AdvantageProject>(_context);
            }
        }
        private IBaseRepository<LocationProject> _LocationProjects;
        public IBaseRepository<LocationProject> LocationProjects
        {
            get
            {
                return _LocationProjects ??= new BaseRepository<LocationProject>(_context);
            }
        }

        private IBaseRepository<Units> _Units;
        public IBaseRepository<Units> Units
        {
            get
            {
                return _Units ??= new BaseRepository<Units>(_context);
            }
        }
        
        private IBaseRepository<UnitImage> _UnitImage;
        public IBaseRepository<UnitImage> UnitImage
        {
            get
            {
                return _UnitImage ??= new BaseRepository<UnitImage>(_context);
            }
        }
        private IBaseRepository<AdvantageUnit> _AdvantageUnit;
        public IBaseRepository<AdvantageUnit> AdvantageUnit
        {
            get
            {
                return _AdvantageUnit ??= new BaseRepository<AdvantageUnit>(_context);
            }
        }
        private IBaseRepository<ServiceUnit> _ServiceUnit;
        public IBaseRepository<ServiceUnit> ServiceUnit
        {
            get
            {
                return _ServiceUnit ??= new BaseRepository<ServiceUnit>(_context);
            }
        }
        private IBaseRepository<MediaCategory> _MediaCategory;
        public IBaseRepository<MediaCategory> MediaCategory
        {
            get
            {
                return _MediaCategory ??= new BaseRepository<MediaCategory>(_context);
            }
        }
        private IBaseRepository<Media> _Media;
        public IBaseRepository<Media> Media
        {
            get
            {
                return _Media ??= new BaseRepository<Media>(_context);
            }
        }

        private IBaseRepository<MediaImages> _MediaImages;
        public IBaseRepository<MediaImages> MediaImages
        {
            get
            {
                return _MediaImages ??= new BaseRepository<MediaImages>(_context);
            }
        }

        private IBaseRepository<FinishCategory> _FinishCategory;
        public IBaseRepository<FinishCategory> FinishCategory
        {
            get
            {
                return _FinishCategory ??= new BaseRepository<FinishCategory>(_context);
            }
        }
        private IBaseRepository<FinishItem> _FinishItem;
        public IBaseRepository<FinishItem> FinishItem
        {
            get
            {
                return _FinishItem ??= new BaseRepository<FinishItem>(_context);
            }
        }
        private IBaseRepository<FinishImage> _FinishImage;
        public IBaseRepository<FinishImage> FinishImage
        {
            get
            {
                return _FinishImage ??= new BaseRepository<FinishImage>(_context);
            }
        }

        private IBaseRepository<Contact> _Contact;
        public IBaseRepository<Contact> Contact
        {
            get
            {
                return _Contact ??= new BaseRepository<Contact>(_context);
            }
        }
        /// <summary>
        /// Saves all changes made across repositories.
        /// This method commits all pending changes to the database in a single transaction.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {

            _context.Dispose();
        }
    }
}
