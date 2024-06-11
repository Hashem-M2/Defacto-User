using DTO_s.ViewResult;
using DTOs.ColorAndSize;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ColotAndSize
{
    public interface ICorlorAndSIzeService
    {
        Task<ResultDataList<ColorDTO>> GetAllColor();
        Task<ResultDataList<SizeDTO>> GetAllSize();
        Task<ResultView<ColorDTO>>  addColor(ColorDTO colorDTO);
        Task<ResultView<SizeDTO>>  addSize(SizeDTO sizeDTO);
        Task<ResultView<ColorDTO>>  DeleteColorS(ColorDTO colorDTO);
        Task<ResultView<SizeDTO>>  DeleteSizeS(SizeDTO SizeDTO);
 
    }
}
