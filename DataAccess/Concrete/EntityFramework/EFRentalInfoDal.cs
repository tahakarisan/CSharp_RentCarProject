using CoreLayer.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFRentalInfoDal : EFEntityRepositoryBase<RentalInfo, RentCarContext>, IRentalDal
    {
        public List<RentalDto> GetRentalInfoDto()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var query = from r in context.Rentals
                            join c in context.Customers
                            on r.CustomerId equals c.Id
                            select new RentalDto
                            {
                                Id = r.Id,
                                CustomerFirstName = c.FirstName,
                                CustomerLastName = c.LastName,
                                RentDate = r.RentDate,
                                ReturnDate = r.ReturnDate

                            };

                return query.ToList(); 
            }
        }
    }
}