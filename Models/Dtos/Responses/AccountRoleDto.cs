namespace Models.DTOs.Responses
{
    public class AccountRoleDto
    {
        public AccountRoleDto(string titleRu, string titleEn)
        {
            TitleRu = titleRu;
            TitleEn = titleEn;
        }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }
    }
}