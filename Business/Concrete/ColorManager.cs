using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 8)
            {
                return new ErrorDataResult<List<Color>>(_colorDal.GetAll(), Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorListed);
        }
        [CacheAspect]
        public IDataResult<List<Color>> GetById(int id)
        {
            return new SuccesfulDataResult<List<Color>>(_colorDal.GetAll(c => c.Id == id), Messages.ColorGetById);
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccesfullResult("Renk başarıyla eklendi");
        }
        [ValidationAspect(typeof(UpdateColorValidator))]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccesfullResult("Renk bilgileri başarıyla güncellendi");
        }
        public IResult Delete(int id)
        {
            if (!_colorDal.GetAll(c => c.Id == id).Any())
            {
                return new ErrorResult("Girmiş olduğunuz bilgilerdeki ile uyuşan veri zaten yok");
            }
            _colorDal.Delete(id);
            return new SuccesfullResult("Renk başarıyla silindi");
        }
    }
}
