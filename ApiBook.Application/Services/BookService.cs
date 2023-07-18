using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ApiBook.Application.Interfaces;
using ApiBook.Domain.Entities;
using ApiBook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiBook.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IConfiguration _configration;

        public BookService(IBookRepository bookRepository, IConfiguration configuration)
        {
            _bookRepository = bookRepository;
            _configration = configuration;
        }

        public async Task<IEnumerable<Book>> FindAll()
        {
            return await _bookRepository.FindAll();
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<string> Add(Book book)
        {
            string base64File = book.Cover;
            string bucketName = "api-book-bucket";
            string fileName = $"{Guid.NewGuid().ToString()}.jpeg";
            string url = $"{_configration.GetSection("S3:UriBucket").Value}{fileName}";
            book.Cover = url;

            bool hasUploadS3 = await UploadFileToS3(base64File, bucketName, fileName);

            if (hasUploadS3)
            {
                await _bookRepository.Add(book);
                return string.Empty;
            }
            else
            {
                return "Ocorreu um erro ao fazer o upload no S3!";
            }
            
        }

        public async Task<string> Update(Book book)
        {
            if (book.Cover != null)
            {
                string base64File = book.Cover;
                string bucketName = "api-book-bucket";
                string fileName = $"{Guid.NewGuid().ToString()}.jpeg";
                string url = $"{_configration.GetSection("S3:UriBucket").Value}{fileName}";

                bool hasUploadS3 = await UploadFileToS3(base64File, bucketName, fileName);

                if (hasUploadS3)
                {
                    var fromDb = await FindOriginalBook(book);
                    fromDb.Cover = url;
                    await _bookRepository.Update(fromDb);
                    return string.Empty;
                }
                else
                {
                    return "Ocorreu um erro ao fazer o upload no S3!";
                }
            }
            else
            {
                var fromDb = await FindOriginalBook(book);
                await _bookRepository.Update(fromDb);
                return string.Empty;
            }
        }

        public async Task Delete(Guid id)
        {
            await _bookRepository.Remove(id);
        }

        private async Task<bool> UploadFileToS3(string base64File, string bucketName, string fileName)
        {
            byte[] fileBytes = Convert.FromBase64String(base64File);

            using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = new MemoryStream(fileBytes),
                    ContentType = "application/octet-stream",
                    CannedACL = S3CannedACL.PublicRead
                };

                var response = await client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private async Task<Book> FindOriginalBook(Book book)
        {
            var fromDb = await GetById(book.Id);
            fromDb.Author = !string.IsNullOrEmpty(book.Author) ? book.Author : fromDb.Author;
            fromDb.LaunchYear = book.LaunchYear > 0 ? book.LaunchYear : fromDb.LaunchYear;
            fromDb.Title = !string.IsNullOrEmpty(book.Title) ? book.Title : fromDb.Title;

            return fromDb;
        }
    }
}
