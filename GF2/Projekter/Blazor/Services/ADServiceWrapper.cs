using Enterprice;

namespace Blazor.Services
{
    public class ADServiceWrapper
    {
        public async Task<List<ADGroup>> GetAllGroupsAsync(Action<string> progressCallback)
        {
            return await Task.Run(() => GroupADService.GetAllGroups(progressCallback));
        }

        public async Task<List<ADUser>> GetAllUsersAsync(Action<string> progressCallback)
        {
            var userService = new UserADService();
            return await Task.Run(() => userService.GetAllUsers(progressCallback));
        }
    }
}

