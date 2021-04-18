namespace Models.DTOs.Responses
{
    public class WorkerRoleDto
    {
        public WorkerRoleDto(string titleRu, string titleEn)
        {
            TitleRu = titleRu;
            TitleEn = titleEn;
        }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }
    }
}