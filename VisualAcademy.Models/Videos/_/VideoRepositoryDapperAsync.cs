using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq; 

namespace VideoAppCore.Models
{
    /// <summary>
    /// [4][2][2] 리포지토리 클래스(비동기 방식): Micro ORM인 Dapper를 사용하여 CRUD 구현
    /// </summary>
    public class VideoRepositoryDapperAsync : IVideoRepositoryAsync
    {
        private readonly SqlConnection db;

        public VideoRepositoryDapperAsync(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }

        // 입력
        public async Task<Video> AddVideoAsync(Video model)
        {
            const string query =
                "Insert Into Videos(Title, Url, Name, Company, CreatedBy) Values(@Title, @Url, @Name, @Company, @CreatedBy);" +
                "Select Cast(SCOPE_IDENTITY() As Int);";

            int id = await db.ExecuteScalarAsync<int>(query, model);

            model.Id = id;

            return model;
        }

        // 상세보기
        public async Task<Video> GetVideoByIdAsync(int id)
        {
            const string query = "Select * From Videos Where Id = @Id";

            var video = await db.QueryFirstOrDefaultAsync<Video>(query, new { id }, commandType: CommandType.Text);

            return video;
        }

        // 출력
        public async Task<List<Video>> GetVideosAsync()
        {
            const string query = "Select * From Videos;";

            var videos = await db.QueryAsync<Video>(query);

            return videos.ToList();
        }

        // 삭제
        public async Task RemoveVideoAsync(int id)
        {
            const string query = "Delete Videos Where Id = @Id";

            await db.ExecuteAsync(query, new { id }, commandType: CommandType.Text);
        }

        // 수정
        public async Task<Video> UpdateVideoAsync(Video model)
        {
            const string query = @"
                    Update Videos 
                    Set 
                        Title = @Title, 
                        Url = @Url, 
                        Name = @Name, 
                        Company = @Company, 
                        ModifiedBy = @ModifiedBy 
                    Where Id = @Id";

            await db.ExecuteAsync(query, model);

            return model;
        }
    }
}
