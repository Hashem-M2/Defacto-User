using Application.Contract;
using AutoMapper;
using DTO_s.Category;
using DTO_s.ViewResult;
using DTOs.ColorAndSize;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Service.ColotAndSize
{
    public class CorlorAndSIzeService : ICorlorAndSIzeService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly ISizeRepository _sizeRepository;

        public CorlorAndSIzeService(IColorRepository corlorRepository, ISizeRepository sizeRepository, IMapper mapper)
        {
            _colorRepository = corlorRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;


        }


        public async Task<ResultDataList<ColorDTO>> GetAllColor()
        {
            var ListofColors = await _colorRepository.GetAllAsync();

            var  data =  _mapper.Map<List<ColorDTO>>(ListofColors.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList());

            return new ResultDataList<ColorDTO>() { Count = data.Count ,Entities=data };

        }

        public async Task<ResultDataList<SizeDTO>> GetAllSize()
        {
            var ListofSizes= await _sizeRepository.GetAllAsync();

            var data = _mapper.Map<List<SizeDTO>>(ListofSizes.Where(x=>x.IsDeleted==false || x.IsDeleted ==null).ToList());

            return new ResultDataList<SizeDTO>() { Count = data.Count ,Entities = data };
        }

        public async Task<ResultView<ColorDTO>>addColor(ColorDTO colorDTO)
        {
            var Query = (await _colorRepository.GetAllAsync());
            var OldColor = Query.Where(c => c.Name.ToLower() == colorDTO.Name.ToLower() || c.HEX.ToLower() == colorDTO.HEX.ToLower() || c.RGB ==colorDTO.RGB).FirstOrDefault();
            if (OldColor != null)
            {
                return new ResultView<ColorDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var clr =  _mapper.Map<Color>(colorDTO);
                var NewPrd = await _colorRepository.CreateAsync(clr);
                NewPrd.IsDeleted = false;
                await _colorRepository.SaveChangesAsync();

                return new ResultView<ColorDTO> { Entity = colorDTO, IsSuccess = true, Message = "Created Successfully" };
            }
         
        }

        public async Task<ResultView<SizeDTO>> addSize(SizeDTO sizeDTO)
        {
            var Query = (await _sizeRepository.GetAllAsync());
            var OldSize= Query.Where(c => c.Name.ToLower() == sizeDTO.Name.ToLower() ).FirstOrDefault();
            if (OldSize != null)
            {
                return new ResultView<SizeDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var size = _mapper.Map<Size>(sizeDTO);
                var NewPrd = await _sizeRepository.CreateAsync(size);
                NewPrd.IsDeleted = false;
                await _sizeRepository.SaveChangesAsync();

                return new ResultView<SizeDTO> { Entity = sizeDTO, IsSuccess = true, Message = "Created Successfully" };
            }
        }

        public async Task<ResultView<ColorDTO>> DeleteColorS(ColorDTO colorDTO)
        {
            try
            {

                var catObj = await _colorRepository.GetByIdAsync(colorDTO.ID);

                if (catObj == null)
                {
                    return new ResultView<ColorDTO>()
                    {
                        Entity = colorDTO,
                        IsSuccess = false,
                        Message = "Not found"
                    };
                }
                catObj.IsDeleted = true;
                await   _colorRepository.SaveChangesAsync();
                ResultView<ColorDTO> resultView = new ResultView<ColorDTO>()
                {
                    Entity = colorDTO,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
                return resultView;
            }
            catch (Exception ex)
            {

                return new ResultView<ColorDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResultView<SizeDTO>> DeleteSizeS(SizeDTO SizeDTO)
        {
            try
            {

                var catObj = await _sizeRepository.GetByIdAsync(SizeDTO.ID);

                if (catObj == null)
                {
                    return new ResultView<SizeDTO>()
                    {
                        Entity = SizeDTO,
                        IsSuccess = false,
                        Message = "Not found"
                    };
                }
                catObj.IsDeleted = true;
                await _colorRepository.SaveChangesAsync();
                ResultView<SizeDTO> resultView = new ResultView<SizeDTO>()
                {
                    Entity = SizeDTO,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
                return resultView;
            }
            catch (Exception ex)
            {

                return new ResultView<SizeDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
