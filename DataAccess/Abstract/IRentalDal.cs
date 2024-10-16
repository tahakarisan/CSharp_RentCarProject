using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<RentalInfo>
    {
        List<RentalDto> GetRentalInfoDto();
    }
}
