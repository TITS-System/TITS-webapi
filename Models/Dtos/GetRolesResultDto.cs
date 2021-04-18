using System.Collections.Generic;
using Models.DTOs.Responses;

namespace Models.DTOs
{
    public class GetRolesResultDto
    {
        public IEnumerable<WorkerRoleDto> Roles { get; set; }

        public GetRolesResultDto(IEnumerable<WorkerRoleDto> roles)
        {
            Roles = roles;
        }
    }
}