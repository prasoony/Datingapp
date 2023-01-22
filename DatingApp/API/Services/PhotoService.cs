using System.Data;
using System.Threading.Tasks;
using API._Helper;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        public  readonly Cloudinary  _cloudinary;

        public PhotoService(IOptions<CloudinarySetting> config)
        {
            var acc = new Account
            (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
            
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
             var uploadResult = new ImageUploadResult();
             if(file.Length>0)
             {
                 using var stream= file.OpenReadStream();
                 var uploadParam = new ImageUploadParams
                 {
                       File = new FileDescription(file.FileName ,stream),
                       Transformation =  new Transformation().Height("130").Width("130").Crop("fill").Gravity("face"),
                       Folder="da-net7"
                       
                 };
                 uploadResult = await _cloudinary.UploadAsync(uploadParam);
             }
             return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string PublicID)
        {
           var deleteParams = new DeletionParams(PublicID);
           return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}